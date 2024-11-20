using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.SceneUnderstanding;
using Microsoft.MixedReality.SceneUnderstanding.Samples.Unity;
using NumericsConversion;
using System;
using System.IO;
using System.Linq;
using TMPro;
using Microsoft.MixedReality.Toolkit.SceneSystem;
using System.Xml.Linq;
using System.Security.AccessControl;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.UIElements;
using Microsoft.MixedReality.Toolkit;
using System.Security.Cryptography;
using System.Security.Claims;
using UnityEditor;
using Microsoft.MixedReality.Toolkit.UI.BoundsControl;
using Microsoft.MixedReality.WorldLocking.Core;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

public class GraphGeneration : MonoBehaviour
{
    public List<SceneObject_Unity> SceneObjects = new List<SceneObject_Unity>(); //Store sceneObjects output from MyScene
    public List<Relation> Relations = new List<Relation>(); //Store relations output from MyScene
    public List<SceneObject_Unity> GameSceneObjects = new List<SceneObject_Unity>(); //Store sceneObjects output from MyScene
    public List<Relation> GameRelations = new List<Relation>(); //Store relations output from MyScene
    public Dictionary<ObjectNodeData, SceneObject_Unity> object_unity_Dict = new Dictionary<ObjectNodeData, SceneObject_Unity>();
    public Dictionary<SceneObject_Unity, ObjectNodeData> unity_object_Dict = new Dictionary<SceneObject_Unity, ObjectNodeData>();
    public Dictionary<string, SceneObject_Unity> Node_Dict = new Dictionary<string, SceneObject_Unity>();
    ObjectAutoPlace objectAutoPlace = new ObjectAutoPlace();
    bool isFirstCall = true;

    Scene myScene;

    public GameObject parentObject;
    public List<GameObject> ObjectsToPlace; //Assume the virtual objects that need to be placed
    int platformCount = 0;
    int floorCount = 0;
    int wallCount = 0;
    int backgroundCount = 0;

    List<GameObject> FloorObjects = new List<GameObject>();
    Bounds mergedBounds = new Bounds();
    private List<Vector3> BadPoint = new List<Vector3>();

    public float MatchingAngle;
    public float HMatchingRange;
    public float VMatchingRange;
    public string SelectedStrategy;
    public float DecisionRange;
    public float CoverageThreshold;
    public TMP_Text titleText;
    public Material newMaterial;
    public Material FloorMaterial;
    public Material PlaneMaterial;

    Vector3 LabelPosition = new Vector3(-99, -99, -99);
    Vector3 LabelPosition1 = new Vector3(-99, -99, -99);
    Vector3 LabelPosition2 = new Vector3(-99, -99, -99);


    private void Update()
    {
        myScene = GameObject.Find("SceneUnderstandingManager").GetComponent<SceneUnderstandingManager>().GetLatestDeserializedScene();

    }

    public void Start()
    {
        StartCoroutine(WaitForMyScene());

        //test
        //GameObject slateBlank = GameObject.Find("SlateBlank");
        //titleText = slateBlank.transform.Find("TitleBar/Title").GetComponent<TMP_Text>();

    }
    public IEnumerator WaitForMyScene()
    {

        yield return new WaitUntil(() => GameObject.Find("Floor") != null);
        yield return new WaitForSeconds(1f);

        //DoPlace();
    }

    void FloorSize()
    {
        GameObject[] objects = FindObjectsOfType<GameObject>();


        foreach (GameObject obj in objects)
        {

            if (obj.name == "FloorMesh" || obj.name == "FloorQuad")
            {
                FloorObjects.Add(obj);

            }

            /*if(obj.name == "PlatformMesh")
            {
                GameObject clonedObj = GameObject.Instantiate(obj);
                clonedObj.transform.position = obj.transform.position;
                clonedObj.transform.rotation = obj.transform.rotation;
                clonedObj.transform.localScale = obj.transform.localScale;
                MeshRenderer renderer = clonedObj.GetComponent<MeshRenderer>();
                renderer.material = new Material(renderer.material);

                if (renderer != null)
                {

                    renderer.material = PlaneMaterial;
                    renderer.material.shader = Shader.Find("Custom/PlaneTextureShader");

                    Vector3 currentPosition = clonedObj.transform.position;

                    // 微调位置向上移动(避免被spatial mesh遮挡)
                    clonedObj.transform.position = new Vector3(currentPosition.x, currentPosition.y + 0.01f, currentPosition.z);
                }

            }*/

           if (obj.name == "BackgroundMesh") //添加材质
            {
                Vector3 Faxian = obj.transform.forward;
                float dotProductWithUp = Vector3.Dot(Faxian, Vector3.up);
                Debug.DrawRay(new Vector3(obj.transform.position.x, obj.transform.position.y + 0.1f, obj.transform.position.z), obj.transform.forward, Color.yellow, 500.0f);


                // 判断该值是否接近1或-1，这里允许一定误差范围，例如0.01
                /*if (Mathf.Abs(dotProductWithUp) >= 0.8)
                {
                    GameObject clonedObj = GameObject.Instantiate(obj);
                    clonedObj.transform.position = obj.transform.position;
                    clonedObj.transform.rotation = obj.transform.rotation;
                    clonedObj.transform.localScale = obj.transform.localScale;
                    MeshRenderer renderer = clonedObj.GetComponent<MeshRenderer>();
                    renderer.material = new Material(renderer.material);

                    if (renderer != null)
                    {

                        renderer.material = PlaneMaterial;
                        renderer.material.shader = Shader.Find("Custom/PlaneTextureShader");

                        Vector3 currentPosition = clonedObj.transform.position;

                        // 微调位置向上移动(避免被spatial mesh遮挡)
                        clonedObj.transform.position = new Vector3(currentPosition.x, currentPosition.y + 0.01f, currentPosition.z);
                    }
                }*/
                /*else
                {
                    GameObject clonedObj = GameObject.Instantiate(obj);
                    clonedObj.transform.position = obj.transform.position;
                    clonedObj.transform.rotation = obj.transform.rotation;
                    clonedObj.transform.localScale = obj.transform.localScale;

                    //float dotProductWithUp = Vector3.Dot(Faxian, Vector3.up);
                    //Debug.DrawRay(new Vector3(obj.transform.position.x, obj.transform.position.y, obj.transform.position.z), obj.transform.forward, Color.yellow, 500.0f);
                    Material newMaterial = Resources.Load<Material>("Leaves");
                    MeshRenderer renderer = clonedObj.GetComponent<MeshRenderer>();
                    renderer.material = new Material(newMaterial);

                    renderer.material.shader = Shader.Find("Custom/VerticalPlaneTextureShader");

                    // 微调位置向上移动(避免被spatial mesh遮挡)
                    clonedObj.transform.position += -clonedObj.transform.forward * 0.01f;
                }*/
               
            }

            /*if(obj.name == "WallMesh")
            {
                GameObject clonedObj = GameObject.Instantiate(obj);
                clonedObj.transform.position = obj.transform.position;
                clonedObj.transform.rotation = obj.transform.rotation;
                clonedObj.transform.localScale = obj.transform.localScale;
                //float dotProductWithUp = Vector3.Dot(Faxian, Vector3.up);
                //Debug.DrawRay(new Vector3(obj.transform.position.x, obj.transform.position.y, obj.transform.position.z), obj.transform.forward, Color.yellow, 500.0f);
                Material newMaterial = Resources.Load<Material>("Leaves");
                MeshRenderer renderer = clonedObj.GetComponent<MeshRenderer>();
                renderer.material = new Material(newMaterial);

                renderer.material.shader = Shader.Find("Custom/VerticalPlaneTextureShader");

                // 微调位置向上移动(避免被spatial mesh遮挡)
                clonedObj.transform.position += -clonedObj.transform.forward * 0.01f ;
            }*/
        }

        Debug.Log("地板的物体数量为：" + FloorObjects.Count);

        mergedBounds = FloorObjects[0].GetComponent<MeshRenderer>().bounds;//初始化避免包括原点

        /*foreach (GameObject obj in FloorObjects)
        {
            // 获取物体的边界信息
            MeshRenderer renderer = obj.GetComponent<MeshRenderer>();
            Bounds bounds = renderer.bounds;
            Debug.Log("范围大小:" + bounds.max + bounds.min);

            
            if (renderer != null)
            {
                GameObject clonedObj = GameObject.Instantiate(obj);
                clonedObj.transform.position = obj.transform.position;
                clonedObj.transform.rotation = obj.transform.rotation;
                clonedObj.transform.localScale = obj.transform.localScale;
                renderer = clonedObj.GetComponent<MeshRenderer>();
                renderer.material = FloorMaterial;
                renderer.material.shader = Shader.Find("Custom/PlaneTextureShader");
                Vector3 currentPosition = clonedObj.transform.position;

                // 微调位置向上移动(避免被spatial mesh遮挡)
                clonedObj.transform.position = new Vector3(currentPosition.x, currentPosition.y + 0.01f, currentPosition.z);
            }
         


            mergedBounds.Encapsulate(bounds);


        }*/
    }

    

    void FindSittable(SceneObject sceneObject, float x, float y, out Transform location11, out float area, out Bounds bounds) //Return suitable position, could be null if nothing suitable
    {
        float raycastDistance = 0.5f; // 射线的长度
        Bounds Meshbounds = new Bounds();
        bounds = new Bounds();
        var marker = new GameObject();
        marker.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        marker.transform.SetParent(parentObject.transform);

        marker.transform.localPosition = sceneObject.Position.ToUnity();
        //marker.transform.localPosition = new Vector3(marker.transform.localPosition.x, marker.transform.localPosition.y, marker.transform.localPosition.z + 0.05f);
        marker.transform.localRotation = sceneObject.Orientation.ToUnity();

        Vector3 up = marker.transform.up;
        float dot = Vector3.Dot(up, Vector3.up);
        float dot1 = Vector3.Dot(marker.transform.forward, Vector3.up);

        bool Hori_upward = false;
        if (Mathf.Abs(dot) < 0.1f)
        {
            if (dot1 < 0)
            {
                /*if (marker.transform.position.y < -0.9f && marker.transform.position.y > -1.3f) //Height filter
                {
                    Hori_upward = true;

                }*/
                Hori_upward = true;



            }
        }


        var quads = sceneObject.Quads;
        location11 = null;

        area = 0.0f;

        if (quads.Count > 0 && Hori_upward)//quads not empty && horizontal && facing upward 
        {

            SceneQuad largestQuad = null;
            float fLargestArea = float.NegativeInfinity;
            foreach (SceneQuad quad in quads)
            {
                /*var quad1 = GameObject.CreatePrimitive(PrimitiveType.Cube);

                quad1.transform.SetParent(marker.transform, false);
                quad1.transform.localPosition = new Vector3(0.0f, 0.0f, -0.03f);

                quad1.transform.localScale = new Vector3(
                    5, 5, 0.005f);

                quad1.GetComponent<Renderer>().material = PlatformMaterial;*/


                System.Numerics.Vector2 extents = quad.Extents;
                area = extents.X * extents.Y;

                if (area > fLargestArea)
                {
                    fLargestArea = area;
                    largestQuad = quad;
                }
            }
            // Find a good location for a 1mx1m object  

            System.Numerics.Vector2 location;


            if (largestQuad.FindCentermostPlacement(new System.Numerics.Vector2(x, y), out location))
            {
                //GameObject location1 = GameObject.Instantiate(placePrefab);
                GameObject location1 = new GameObject();
                location1.transform.parent = marker.transform;
                location1.transform.localPosition = Vector3.zero;
                location1.transform.localRotation = Quaternion.identity;

                location1.transform.Translate(-(largestQuad.Extents.X / 2.0f), largestQuad.Extents.Y / 2.0f, -0.05f, Space.Self);

                //Move to the CenterMost Place -- Y is negate so that we move right and down, according to the API 'FindCenterMostPlacement'
                //https://docs.microsoft.com/en-us/windows/mixed-reality/develop/platform-capabilities-and-apis/scene-understanding-SDK#quad
                location1.transform.Translate(location.X, -location.Y, 0.0f, Space.Self);
                //location1.transform.Rotate(0.0f, 0.0f, 0.0f);


                location11 = location1.transform;
            }

            RaycastHit[] hits;
            hits = Physics.RaycastAll(new Vector3(location11.position.x, location11.position.y + 0.1f, location11.position.z), Vector3.down, raycastDistance);

            float nearestDistance = float.MaxValue; // 初始化为最大浮点值
            RaycastHit nearestHit = new RaycastHit(); // 用来存储最近的Hit

            foreach (RaycastHit hit in hits)
            {
                if ((hit.transform.gameObject.name == "PlatformMesh" || hit.transform.gameObject.name == "BackgroundMesh" || hit.transform.gameObject.name == "WallMesh") && hit.distance < nearestDistance)
                {
                    nearestHit = hit;
                    nearestDistance = hit.distance;
                }
            }

            if (nearestHit.transform != null)
            {
                bounds = nearestHit.transform.gameObject.GetComponent<MeshRenderer>().bounds;
                DrawOnGameViewRuntime(nearestHit.transform.gameObject.GetComponent<MeshRenderer>(), Color.blue, 1f);
            }

        }


    }

    private void AddSceneObjects() //Add all SceneObjects from SU scene
    {

        SceneObject_Unity scene_object;

        foreach (var sceneObject in myScene.SceneObjects)
        {
            float size;
            string category;
            Transform transform;
            bool horizontal;
            Bounds bounds = new Bounds();

            switch (sceneObject.Kind)
            {
                case SceneObjectKind.Platform:
                    FindCenterPlace(sceneObject, 0.1f, 0.1f, out transform, out size, out bounds);
                    if (transform != null)
                    {
                        category = "Platform";
                        platformCount++;
                        horizontal = true;
                        scene_object = new SceneObject_Unity(platformCount, category, size, transform, horizontal, true);
                        Debug.Log("IIIIIIMMMMMMMMMMDDDDDDDDD");
                    }
                    else
                    {
                        continue;
                    }
                    break;

                case SceneObjectKind.Floor:
                    FindCenterPlace(sceneObject, 0.5f, 0.5f, out transform, out size, out bounds);
                    if (transform != null)
                    {
                        category = "Floor";
                        floorCount++;
                        horizontal = true;

                    }
                    else
                    {
                        continue;
                    }
                    scene_object = new SceneObject_Unity(floorCount, category, size, transform, horizontal, true);

                    break;

                case SceneObjectKind.Background:
                    FindSittable(sceneObject, 0.1f, 0.1f, out transform, out size, out bounds);// We filter vertical & big size background here
                    if (transform != null)
                    {
                        category = "Background";
                        backgroundCount++;
                        horizontal = true;
                        //Debug.Log(size);
                    }
                    else
                    {
                        continue;
                    }
                    scene_object = new SceneObject_Unity(backgroundCount, category, size, transform, horizontal, true);

                    break;

                case SceneObjectKind.Wall:
                    FindCenterPlace(sceneObject, 0.1f, 0.1f, out transform, out size, out bounds);
                    if (transform != null)
                    {
                        category = "Wall";
                        wallCount++;
                        horizontal = false;

                    }
                    else
                    {
                        continue;
                    }
                    scene_object = new SceneObject_Unity(wallCount, category, size, transform, horizontal, true);

                    break;

                default:
                    continue;
            }

            scene_object.Bounds = bounds;
            SceneObjects.Add(scene_object);





        }

    }

    void FindCenterPlace(SceneObject sceneObject, float x, float y, out Transform location11, out float area, out Bounds bounds)
    {
        float raycastDistance = 0.5f; // 射线的长度
        // Get the quad position
        var marker = new GameObject();

        marker.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        marker.transform.SetParent(parentObject.transform);

        marker.transform.localPosition = sceneObject.Position.ToUnity();
        //marker.transform.localPosition = new Vector3(marker.transform.localPosition.x, marker.transform.localPosition.y, marker.transform.localPosition.z + 0.05f);
        marker.transform.localRotation = sceneObject.Orientation.ToUnity();

        var quads = sceneObject.Quads;
        location11 = null;

        area = 0.0f;
        Bounds Meshbounds = new Bounds();
        bounds = new Bounds();

        if (quads.Count > 0)
        {
            SceneQuad largestQuad = null;
            float fLargestArea = float.NegativeInfinity;
            foreach (SceneQuad quad in quads)
            {
                System.Numerics.Vector2 extents = quad.Extents;
                area = extents.X * extents.Y;

                if (area > fLargestArea)
                {
                    fLargestArea = area;
                    largestQuad = quad;
                }
            }

            System.Numerics.Vector2 location;

            if (largestQuad.FindCentermostPlacement(new System.Numerics.Vector2(x, y), out location))
            {
                GameObject location1 = new GameObject();
                location1.transform.parent = marker.transform;
                location1.transform.localPosition = Vector3.zero;
                location1.transform.localRotation = Quaternion.identity;

                location1.transform.Translate(-(largestQuad.Extents.X / 2.0f), largestQuad.Extents.Y / 2.0f, -0.05f, Space.Self);

                location1.transform.Translate(location.X, -location.Y, 0.0f, Space.Self);
                location1.transform.Rotate(0.0f, 0.0f, 0.0f);

                location11 = location1.transform;
            }

            if (location11 == null)
            {
                Debug.LogError("location11 is not set.");
                return;
            }

            RaycastHit[] hits;
            hits = Physics.RaycastAll(new Vector3(location11.position.x, location11.position.y + 0.1f, location11.position.z), Vector3.down, raycastDistance);

            float nearestDistance = float.MaxValue; // 初始化为最大浮点值
            RaycastHit nearestHit = new RaycastHit(); // 用来存储最近的Hit

            foreach (RaycastHit hit in hits)
            {
                if ((hit.transform.gameObject.name == "PlatformMesh" || hit.transform.gameObject.name == "BackgroundMesh" || hit.transform.gameObject.name == "WallMesh") && hit.distance < nearestDistance)
                {
                    nearestHit = hit;
                    nearestDistance = hit.distance;
                }
            }

            if (nearestHit.transform != null)
            {
                bounds = nearestHit.transform.gameObject.GetComponent<MeshRenderer>().bounds;
                //DrawOnGameViewRuntime(nearestHit.transform.gameObject.GetComponent<MeshRenderer>(), Color.blue, 1f);
            }
        }

    } //Find transform and size of target SceneObject

    public static void DrawOnGameViewRuntime(MeshRenderer renderer, Color color = default(Color), float offsetSize = 1f)
    {
        float width = 0.1f;
        Transform objTransform = renderer.transform;
        Bounds bounds = renderer.bounds;

        Vector3 rightDir = objTransform.right.normalized;
        Vector3 forwardDir = objTransform.up.normalized;
        Vector3 upDir = objTransform.forward.normalized;
        Vector3 center = bounds.center;
        Vector3 size = bounds.size * offsetSize;

        // 直接使用对象的lossyScale来调整大小
        size = Vector3.Scale(size, objTransform.lossyScale);

        Vector3 a = center + upDir * size.y / 2f + rightDir * size.x / 2f + forwardDir * size.z / 2f;
        Vector3 b = center + upDir * size.y / 2f - rightDir * size.x / 2f + forwardDir * size.z / 2f;
        Vector3 c = center - upDir * size.y / 2f + rightDir * size.x / 2f + forwardDir * size.z / 2f;
        Vector3 d = center - upDir * size.y / 2f - rightDir * size.x / 2f + forwardDir * size.z / 2f;
        Vector3 e = center + upDir * size.y / 2f + rightDir * size.x / 2f - forwardDir * size.z / 2f;
        Vector3 f = center + upDir * size.y / 2f - rightDir * size.x / 2f - forwardDir * size.z / 2f;
        Vector3 g = center - upDir * size.y / 2f + rightDir * size.x / 2f - forwardDir * size.z / 2f;
        Vector3 h = center - upDir * size.y / 2f - rightDir * size.x / 2f - forwardDir * size.z / 2f;

        // 绘制12条线
        Debug.DrawLine(a, b, color, 300f);
        Debug.DrawLine(a, c, color, 300f);
        Debug.DrawLine(a, e, color, 300f);
        Debug.DrawLine(b, d, color, 300f);
        Debug.DrawLine(b, f, color, 300f);
        Debug.DrawLine(c, d, color, 300f);
        Debug.DrawLine(c, g, color, 300f);
        Debug.DrawLine(d, h, color, 300f);
        Debug.DrawLine(e, f, color, 300f);
        Debug.DrawLine(e, g, color, 300f);
        Debug.DrawLine(f, h, color, 300f);
        Debug.DrawLine(g, h, color, 300f);
    }

    private void AddRelations() //Add all Relations from SU scene
    {
        TextAsset textAsset2 = Resources.Load<TextAsset>("GlobalParas");
        string fileContent2 = textAsset2.text;
        LoadParametersFromFile(fileContent2);
        Relation relation;

        for (int i = 0; i < SceneObjects.Count; i++)
        {
            for (int j = 0; j < SceneObjects.Count; j++) //Avoid repeated comparisons and self comparisons

            {
                if (i == j) continue;

                if (SceneObjects[i].Category == "Floor" || SceneObjects[j].Category == "Floor")
                {
                    continue;
                }

                if (SceneObjects[i].Category == "Floor" && SceneObjects[j].Category == "Wall")
                {
                    continue;
                }

                if (SceneObjects[i].Category == "Wall" && SceneObjects[j].Category == "Floor")
                {
                    continue;
                }

                if (SceneObjects[i].Category == "Wall" && SceneObjects[j].Category == "Wall")
                {
                    continue;
                }

                //switch (RelationJudge(SceneObjects[j].Position.position, SceneObjects[i].Position.position))
                switch (Relationpanduan(SceneObjects[j], SceneObjects[i].Position.position))
                {
                    case RelationType.Above:
                        relation = new Relation(SceneObjects[i], SceneObjects[j], RelationType.Above);

                        break;

                    case RelationType.InFrontOf:
                        relation = new Relation(SceneObjects[i], SceneObjects[j], RelationType.InFrontOf);

                        break;

                    case RelationType.Behind:
                        relation = new Relation(SceneObjects[i], SceneObjects[j], RelationType.Behind);

                        break;

                    case RelationType.Right:
                        relation = new Relation(SceneObjects[i], SceneObjects[j], RelationType.Right);

                        break;

                    case RelationType.Left:
                        relation = new Relation(SceneObjects[i], SceneObjects[j], RelationType.Left);

                        break;

                    case RelationType.Below:
                        relation = new Relation(SceneObjects[i], SceneObjects[j], RelationType.Below);
                        break;

                    default:
                        relation = new Relation(SceneObjects[i], SceneObjects[j], RelationType.None);
                        break;
                }


                if (relation.Relationship != RelationType.None)
                {
                    Relations.Add(relation);
                    Relation relation1 = RelationReverse(relation);
                    Relations.Add(relation1);

                }
            }

        }

       

        // Apply reverse relationship a -> b add b reverse relation -> a

    }
    bool IsAbove(Vector3 posA, Vector3 posB)
    {
        Collider[] colliders = Physics.OverlapSphere(posB, 0.2f); // 设置合适的半径值，确保覆盖到目标周围的所有可能对象
        Bounds bounds = new Bounds();
        GameObject closestObject = null;
        float closestDistance = Mathf.Infinity;

        foreach (Collider collider in colliders)
        {
            float distance = Vector3.Distance(posB, collider.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestObject = collider.gameObject;
            }
        }

        if (closestObject != null)
        {
            bounds = closestObject.GetComponent<MeshRenderer>().bounds;
        }

        /*RaycastHit hit;
        Bounds bounds = new Bounds();
        if (Physics.Raycast(new Vector3(posB.x, posB.y + 0.1f, posB.z), Vector3.down, out hit, 0.2f))
        {
            // 检测到物体，返回被击中的物体
            bounds = hit.transform.gameObject.GetComponent<MeshRenderer>().bounds;
        } //Calculate objectB's bounding box range*/

        // 获取物体B包围盒在XZ平面上的坐标范围
        Vector2 boundsBMinXZ = new Vector2(bounds.min.x, bounds.min.z);
        Vector2 boundsBMaxXZ = new Vector2(bounds.max.x, bounds.max.z);

        // 判断物体A在XZ平面上的坐标是否在物体B的包围盒在XZ平面上的坐标范围内
        if (posA.x >= boundsBMinXZ.x && posA.x <= boundsBMaxXZ.x &&
            posA.z >= boundsBMinXZ.y && posA.z <= boundsBMaxXZ.y)
        {
            if (posA.y >= posB.y)
            {
                return true; // 物体A在物体B的上方（忽略高度）
            }

        }

        return false; // 物体A不在物体B的上方（忽略高度）
    }

    bool IsBelow(Vector3 posA, Vector3 posB)
    {

        Collider[] colliders = Physics.OverlapSphere(posB, 0.2f); // 设置合适的半径值，确保覆盖到目标周围的所有可能对象
        Bounds bounds = new Bounds();
        GameObject closestObject = null;
        float closestDistance = Mathf.Infinity;

        foreach (Collider collider in colliders)
        {
            float distance = Vector3.Distance(posB, collider.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestObject = collider.gameObject;
            }
        }

        if (closestObject != null)
        {
            bounds = closestObject.GetComponent<MeshRenderer>().bounds;
        }

        // 获取物体B包围盒在XZ平面上的坐标范围
        Vector2 boundsBMinXZ = new Vector2(bounds.min.x, bounds.min.z);
        Vector2 boundsBMaxXZ = new Vector2(bounds.max.x, bounds.max.z);

        // 判断物体A在XZ平面上的坐标是否在物体B的包围盒在XZ平面上的坐标范围内
        if (posA.x >= boundsBMinXZ.x && posA.x <= boundsBMaxXZ.x &&
            posA.z >= boundsBMinXZ.y && posA.z <= boundsBMaxXZ.y)
        {
            if (posA.y < posB.y)
            {
                return true; // 物体A在物体B的下方（忽略高度）
            }

        }

        return false; // 
    }

    public static void DrawBoundBoxLine(Bounds bounds, Color color = default(Color), float offsetSize = 1f, float duration = 0.1f)
    {
        //先计算出包围盒8个点
        Vector3[] points = new Vector3[8];
        var width_x = bounds.size.x * offsetSize;
        var hight_y = bounds.size.y * offsetSize;
        var length_z = bounds.size.z * offsetSize;

        var LeftBottomPoint = bounds.min;
        var rightUpPoint = bounds.max;
        var centerPoint = bounds.center;
        var topPoint = new Vector3(centerPoint.x, centerPoint.y + hight_y / 2, centerPoint.z);
        var bottomPoint = new Vector3(centerPoint.x, centerPoint.y - hight_y * 0.5f, centerPoint.z);

        points[0] = LeftBottomPoint + Vector3.right * width_x;
        points[1] = LeftBottomPoint + Vector3.up * hight_y;
        points[2] = LeftBottomPoint + Vector3.forward * length_z;

        points[3] = rightUpPoint - Vector3.right * width_x;
        points[4] = rightUpPoint - Vector3.up * hight_y;
        points[5] = rightUpPoint - Vector3.forward * length_z;

        points[6] = LeftBottomPoint;
        points[7] = rightUpPoint;

        Debug.DrawLine(LeftBottomPoint, points[0], color, duration);
        Debug.DrawLine(LeftBottomPoint, points[1], color, duration);
        Debug.DrawLine(LeftBottomPoint, points[2], color, duration);

        Debug.DrawLine(rightUpPoint, points[3], color, duration);
        Debug.DrawLine(rightUpPoint, points[4], color, duration);
        Debug.DrawLine(rightUpPoint, points[5], color, duration);

        Debug.DrawLine(points[1], points[3], color, duration);
        Debug.DrawLine(points[2], points[4], color, duration);
        Debug.DrawLine(points[0], points[5], color, duration);

        Debug.DrawLine(points[2], points[3], color, duration);
        Debug.DrawLine(points[0], points[4], color, duration);
        Debug.DrawLine(points[1], points[5], color, duration);
    }

    /*public RelationType Relationpanduan_angle(SceneObject_Unity objectA, Vector3 objectB) // x: right, z: FORWARD 
    {
        Vector3 sizeA = objectA.Bounds.size;
        Vector3 centerAtoB = objectB - objectA.Position.position;

        Vector3 longSideDirection = Vector3.zero;
        Vector3 shortSideDirection = Vector3.zero;

        float depth = HMatchingRange;  // 预设的深度范围

        // 确定长边和短边方向
        if (sizeA.x > sizeA.y && sizeA.x > sizeA.z)
        {
            longSideDirection = objectA.Position.right;
            shortSideDirection = (sizeA.y > sizeA.z) ? objectA.Position.forward : objectA.Position.up;
        }
        else if (sizeA.y > sizeA.x && sizeA.y > sizeA.z)
        {
            longSideDirection = objectA.Position.up;
            shortSideDirection = (sizeA.x > sizeA.z) ? objectA.Position.forward : objectA.Position.right;
        }
        else
        {
            longSideDirection = objectA.Position.forward;
            shortSideDirection = (sizeA.x > sizeA.y) ? objectA.Position.up : objectA.Position.right;
        }

        float distanceInLongDirection = Vector3.Dot(longSideDirection.normalized, centerAtoB);
        float distanceInShortDirection = Vector3.Dot(shortSideDirection.normalized, centerAtoB);

        float angleWithLongSide = Vector3.Angle(centerAtoB, longSideDirection.normalized);
        float angleWithShortSide = Vector3.Angle(centerAtoB, shortSideDirection.normalized);

        float overlapRate = CalculateOverlapRate(objectA, objectB);
        

        if (Vector3.Distance(objectA.Position.position, objectB) <= 2.0f && Mathf.Abs(centerAtoB.y) >= VMatchingRange)
        {
            if (overlapRate >= 70) 
            {
                if (centerAtoB.y < 0)
                return RelationType.Below;
            else
                return RelationType.Above;
            }

            
        }

        // 判断前后关系
        if (Mathf.Abs(distanceInShortDirection) <= depth && Mathf.Abs(distanceInShortDirection) <= sizeA.z * 0.8 &&
            Mathf.Abs(angleWithLongSide) <= MatchingAngle)
        {
            if (overlapRate > 70) 
            {
                return RelationType.None;  // 重合率过高，无法确定具体的空间关系
            }

            if (distanceInShortDirection > 0)
            {
                return RelationType.InFrontOf;
            }
            else
            {
                return RelationType.Behind;
            }
        }

        // 判断左右关系
        if (Mathf.Abs(distanceInLongDirection) <= depth && Mathf.Abs(distanceInLongDirection) <= sizeA.x * 0.8 &&
            Mathf.Abs(angleWithShortSide) <= MatchingAngle)
        {
            if (overlapRate > 70) 
            {
                return RelationType.None;  // 重合率过高，无法确定具体的空间关系
            }

            if (distanceInLongDirection > 0)
            {
                return RelationType.Right;
            }
            else
            {
                return RelationType.Left;
            }
        }

        return RelationType.None;
    }*/

    public float CalculateOverlapRate(SceneObject_Unity objectA, SceneObject_Unity objectB)
    {
        Bounds boundsA = objectA.Bounds;
        Bounds boundsB = objectB.Bounds;

        // 计算交集区域的最小和最大点
        float minX = Mathf.Max(boundsA.min.x, boundsB.min.x);
        float maxX = Mathf.Min(boundsA.max.x, boundsB.max.x);
        float minY = Mathf.Max(boundsA.min.y, boundsB.min.y);
        float maxY = Mathf.Min(boundsA.max.y, boundsB.max.y);

        // 计算重叠区域的宽度和高度
        float width = maxX - minX;
        float height = maxY - minY;

        // 如果宽度或高度小于0，则没有重叠
        if (width < 0 || height < 0)
        {
            return 0.0f; // 无重叠
        }

        // 计算重叠面积
        float overlapArea = width * height;

        // 计算两个矩形的面积
        float areaA = boundsA.size.x * boundsA.size.y;
        float areaB = boundsB.size.x * boundsB.size.y;

        // 计算重合率
        float minArea = Mathf.Min(areaA, areaB);
        float overlapRate = (overlapArea / minArea) * 100;  
        return overlapRate;
    }


    public RelationType Relationpanduan(SceneObject_Unity objectA, Vector3 objectB) ////x: right z : FORWARD 
    {
        Vector3 sizeA = objectA.Bounds.size;

        Vector3 centerAtoB = objectB - objectA.Position.position;

        Vector3 longSideDirection = new Vector3();
        Vector3 shortSideDirection = new Vector3();

        DrawBoundBoxLine(objectA.Bounds, Color.red, 1f, 300f);


        float depth = HMatchingRange;

        if (sizeA.x > sizeA.y && sizeA.x > sizeA.z)
        {
            longSideDirection = objectA.Position.right;
            shortSideDirection = (sizeA.y > sizeA.z) ? objectA.Position.forward : objectA.Position.up;
        }
        else if (sizeA.y > sizeA.x && sizeA.y > sizeA.z)
        {
            longSideDirection = objectA.Position.up;
            shortSideDirection = (sizeA.x > sizeA.z) ? objectA.Position.forward : objectA.Position.right;
        }
        else
        {
            longSideDirection = objectA.Position.forward;
            shortSideDirection = (sizeA.x > sizeA.y) ? objectA.Position.up : objectA.Position.right;
        }

        float distanceInLongDirection = Vector3.Dot(longSideDirection.normalized, centerAtoB);
        float distanceInShortDirection = Vector3.Dot(shortSideDirection.normalized, centerAtoB);

        float angleWithLongSide = Vector3.Angle(centerAtoB, longSideDirection.normalized);
        float angleWithShortSide = Vector3.Angle(centerAtoB, shortSideDirection.normalized);

        if (Vector3.Distance(objectA.Position.position, objectB) <= 1.0f && Mathf.Abs(centerAtoB.y) >= VMatchingRange)
        {
            if (centerAtoB.y < 0)
                return RelationType.Below;
            else return RelationType.Above;
        }


        if (Mathf.Abs(distanceInShortDirection) <= depth && Mathf.Abs(distanceInLongDirection) <= sizeA.x * 0.8)
        {
            if (distanceInShortDirection > 0)
            {
                return RelationType.InFrontOf;

            }

            else
            {
                /*Vector3 halfLong = longSideDirection.normalized * sizeA.x * 0.5f;
                Vector3 halfShort = shortSideDirection.normalized * depth;

                // 矩形的四个角点
                Vector3 corner1 = objectA.Bounds.center + halfLong - halfShort;
                Vector3 corner2 = objectA.Bounds.center + halfLong;
                Vector3 corner3 = objectA.Bounds.center - halfLong - halfShort;
                Vector3 corner4 = objectA.Bounds.center - halfLong;


                Debug.DrawLine(corner1, corner2, Color.red, 300f);
                Debug.DrawLine(corner2, corner4, Color.yellow, 300f);
                Debug.DrawLine(corner4, corner3, Color.green, 300f);
                Debug.DrawLine(corner3, corner1, Color.black, 300f); */
                return RelationType.Behind;

            }
        }

        if (Mathf.Abs(distanceInLongDirection) <= depth && Mathf.Abs(distanceInShortDirection) <= sizeA.z * 0.8)
        {
            if (distanceInLongDirection > 0)
            {
                return RelationType.Right;

            }
            else
                return RelationType.Left;
        }

        return RelationType.None;
    }
    private RelationType RelationJudge(Relation relation, Vector3 objectB)
    {
        SceneObject_Unity objectA = relation.EndNode; //判断载体的方向
        Vector3 centerAtoB = objectB - objectA.Position.position;

        Vector3 sizeA = objectA.Bounds.size;

        Vector3 longSideDirection = new Vector3();
        Vector3 shortSideDirection = new Vector3();

        DrawBoundBoxLine(objectA.Bounds, Color.red, 1f, 300f);


        float mindepth = 1.0f; //test= 1.0
        float maxdepth = HMatchingRange; //defualt 3.0
        //maxdepth = SpecialRelations(relation);
        if (maxdepth <= 2.0f)
            mindepth = 0.0f;

        if (sizeA.x > sizeA.y && sizeA.x > sizeA.z)
        {
            longSideDirection = objectA.Position.right;
            shortSideDirection = (sizeA.y > sizeA.z) ? objectA.Position.forward : objectA.Position.up;
        }
        else if (sizeA.y > sizeA.x && sizeA.y > sizeA.z)
        {
            longSideDirection = objectA.Position.up;
            shortSideDirection = (sizeA.x > sizeA.z) ? objectA.Position.forward : objectA.Position.right;


        }
        else
        {
            longSideDirection = objectA.Position.forward;
            shortSideDirection = (sizeA.x > sizeA.y) ? objectA.Position.up : objectA.Position.right;
        }


        float distanceInLongDirection = Vector3.Dot(longSideDirection.normalized, centerAtoB);
        float distanceInShortDirection = Vector3.Dot(shortSideDirection.normalized, centerAtoB);

        float angleWithLongSide = Vector3.Angle(centerAtoB, longSideDirection.normalized);
        float angleWithShortSide = Vector3.Angle(centerAtoB, shortSideDirection.normalized);

        float dotResult = Vector3.Dot(centerAtoB.normalized, objectA.Position.up);
        if (Vector3.Distance(objectA.Position.position, objectB) <= 1.0f && Mathf.Abs(centerAtoB.y) <= VMatchingRange && dotResult <= -0.7f) // 使用-0.95而不是-1，以留出一点容错空间
        {
            return RelationType.Below;
        }



        if (Mathf.Abs(distanceInShortDirection) >= mindepth && Mathf.Abs(distanceInShortDirection) <= maxdepth && Mathf.Abs(distanceInLongDirection) <= sizeA.x * 0.8)
        {
            if (distanceInShortDirection > 0)
            {
                return RelationType.InFrontOf;

            }

            else
            {
                return RelationType.Behind;

            }
        }

        if (Mathf.Abs(distanceInLongDirection) >= mindepth && Mathf.Abs(distanceInLongDirection) <= maxdepth && Mathf.Abs(distanceInShortDirection) <= sizeA.z * 0.8)
        {
            if (distanceInLongDirection > 0)
            {
                return RelationType.Right;

            }
            else
                return RelationType.Left;
        }

        return RelationType.None;




    }

    public void AddGameSceneObjects()
    {
        GameSceneObjects.Clear();

        Dictionary<string, int> categoryIDs = new Dictionary<string, int>();

        List<ObjectNodeData> objectNodes = null;
        try
        {
            TextAsset textAsset = Resources.Load<TextAsset>("Output1");


            string fileContent = textAsset.text;
            objectNodes = LoadSceneObjectsFromFile1(fileContent);
        }
        catch (Exception e)
        {
            Debug.Log(1);
        }
        //List<ObjectNodeData> objectNodes = LoadSceneObjectsFromFile("U:\\Users\\imdna\\Documents\\Output1.txt");
        //List<ObjectNodeData> objectNodes = LoadSceneObjectsFromFile("Output1.txt");


        foreach (ObjectNodeData objectNode in objectNodes)
        {
            Transform myTransform = new GameObject().transform;
            myTransform.position = Vector3.zero;
            myTransform.rotation = Quaternion.identity;

            if (!categoryIDs.ContainsKey(objectNode.category))
            {
                categoryIDs.Add(objectNode.category, 0);
            }
            // Create SceneObject_Unity 
            SceneObject_Unity sceneObject = new SceneObject_Unity(
                objectNode.GID,
                objectNode.category,
                objectNode.size,
                myTransform,
                objectNode.horizontal,
                false
            );
            sceneObject.ID = categoryIDs[objectNode.category];
            sceneObject.Height = objectNode.height;
            sceneObject.Float = objectNode.Float;
            sceneObject.Bounds = objectNode.Prefab.GetComponent<BoxCollider>().bounds; //cannot get
            // Increment the ID for this category
            categoryIDs[objectNode.category]++;
            // Add to list
            GameSceneObjects.Add(sceneObject);
        }

        TextAsset textAsset1 = Resources.Load<TextAsset>("OutputRelations");
        //TextAsset textAsset2 = Resources.Load<TextAsset>("GlobalParas");
        //

        //
        string fileContent1 = textAsset1.text;
        //string fileContent2 = textAsset2.text;
        GameRelations = LoadRelationsFromFile1(fileContent1);
        //LoadParametersFromFile(fileContent2);


        //GameRelations = LoadRelationsFromFile("OutputRelations.txt");
        if (GameRelations == null)
        {
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            cube.transform.position = Vector3.zero + new Vector3(0, 1.0f, 0);
        }


        foreach (Relation relation in GameRelations)
        {
            if (relation.Relationship != RelationType.None)
            {
                Debug.Log(relation.StartNode.Category + " " + relation.StartNode.ID +
                             " -> " + relation.EndNode.Category + " " + relation.EndNode.ID + " [label = \"" + relation.Relationship + "\"]\n");

            }
        }


        foreach (var node in objectNodes)
        {
            foreach (SceneObject_Unity object_unity in GameSceneObjects)
            {
                if (node.GID == object_unity.GUID)
                {
                    object_unity_Dict.Add(node, object_unity);
                    unity_object_Dict.Add(object_unity, node);
                }
            }

        }


        foreach (var node in objectNodes)
        {
            foreach (SceneObject_Unity object_unity in GameSceneObjects)
            {
                if (node.GID == object_unity.GUID)
                {
                    Node_Dict.Add(node.GID, object_unity);
                }
            }

        }

        /*
        Debug.Log("Relation number: " + GameRelations.Count);


        foreach (Relation relation1 in GameRelations)
        {
            if (relation1.Relationship != RelationType.None)
            {
                Debug.Log("ObjectA: " + relation1.StartNode.Category + " " + relation1.StartNode.ID + ", Relationship: " + relation1.Relationship + ", ObjectB: " + relation1.EndNode.Category + " " + relation1.EndNode.ID);

            }
        }*/

    }

    private RaycastHit GetNearestPlatformHit(RaycastHit[] hits)
    {
        float nearestDistance = float.MaxValue; // 初始化为最大浮点值
        RaycastHit nearestHit = new RaycastHit(); // 用来存储最近的Hit

        foreach (RaycastHit hit in hits)
        {
            if ((hit.transform.gameObject.name == "PlatformMesh" || hit.transform.gameObject.name == "PlatformQuad" || hit.transform.gameObject.name == "BackgroundMesh") && hit.distance < nearestDistance)
            {
                nearestHit = hit;
                nearestDistance = hit.distance;
            }
        }

        return nearestHit;
    }


    public float SpecialRelations(Relation relation) //!(relation.Relationship == RelationType.Above || relation.Relationship == RelationType.Below)
    {
        //常见的大物体： 桌子，椅子，电视，沙发，书架，柜子 逻辑：椅子最好挨着桌子，电视可以远一些。。。
        //小东西： 桌上的/沙发上的小东西 逻辑：Above，紧贴着附属物体
        float depth = 2.0f;
        if (relation.StartNode.Category == "Chair")
        {
            switch (relation.EndNode.Category)
            {
                case "Table":
                case "Platform":
                case "Chair":
                case "Sofa":
                case "Character":
                case "Bed":
                case "Wall":
                    depth = 2.0f; break;

                case "TV":
                    depth = 8.0f; break;

                case "Shelf":
                    depth = 8.0f; break;

            }
        }

        if (relation.StartNode.Category == "Table")
        {
            switch (relation.EndNode.Category)
            {
                case "Table":
                case "Platform":
                case "Shelf":
                    depth = 10.0f; break;


                case "TV":
                    depth = 5.0f; break;

                case "Chair":
                case "Character":
                case "Wall":
                    depth = 2.0f; break;

                case "Sofa":
                case "Bed":
                    depth = 6.0f; break;

            }
        }

        if (relation.StartNode.Category == "Sofa")
        {
            switch (relation.EndNode.Category)
            {
                case "Table":
                    depth = 6.0f; break;

                case "Platform":
                    depth = 6.0f; break;

                case "Chair":
                    depth = 6.0f; break;

                case "Sofa":
                    depth = 6.0f; break;

                case "Character":
                    depth = 2.0f; break;

                case "TV":
                    depth = 8.0f; break;

                case "Shelf":
                    depth = 8.0f; break;

                case "Bed":
                    depth = 6.0f; break;

                case "Wall":
                    depth = 2.0f; break;
            }
        }

        if (relation.StartNode.Category == "Character")
        {
            switch (relation.EndNode.Category)
            {
                case "Table":
                    depth = 2.0f; break;

                case "Platform":
                    depth = 2.0f; break;

                case "Chair":
                    depth = 2.0f; break;

                case "Sofa":
                    depth = 2.0f; break;

                case "Character":
                    depth = 2.0f; break;

                case "TV":
                    depth = 4.0f; break;

                case "Shelf":
                    depth = 2.0f; break;

                case "Bed":
                    depth = 2.0f; break;

                case "Wall":
                    depth = 2.0f; break;
            }
        }

        if (relation.StartNode.Category == "TV")
        {
            switch (relation.EndNode.Category)
            {
                case "Table":
                    depth = 5.0f; break;

                case "Platform":
                    depth = 5.0f; break;

                case "Chair":
                    depth = 4.0f; break;

                case "Sofa":
                    depth = 4.0f; break;

                case "Character":
                    depth = 2.0f; break;

                case "TV":
                    depth = 5.0f; break;

                case "Shelf":
                    depth = 8.0f; break;

                case "Bed":
                    depth = 4.0f; break;

                case "Wall":
                    depth = 2.0f; break;
            }
        }

        if (relation.StartNode.Category == "Shelf")
        {
            switch (relation.EndNode.Category)
            {
                case "Table":
                    depth = 8.0f; break;

                case "Platform":
                    depth = 8.0f; break;

                case "Chair":
                    depth = 8.0f; break;

                case "Sofa":
                    depth = 8.0f; break;

                case "Character":
                    depth = 2.0f; break;

                case "TV":
                    depth = 5.0f; break;

                case "Shelf":
                    depth = 8.0f; break;

                case "Bed":
                    depth = 4.0f; break;

                case "Wall":
                    depth = 2.0f; break;
            }
        }

        if (relation.StartNode.Category == "Bed")
        {
            switch (relation.EndNode.Category)
            {
                case "Table":
                    depth = 3.0f; break;

                case "Platform":
                    depth = 3.0f; break;

                case "Chair":
                    depth = 2.0f; break;

                case "Sofa":
                    depth = 4.0f; break;

                case "Character":
                    depth = 2.0f; break;

                case "TV":
                    depth = 4.0f; break;

                case "Shelf":
                    depth = 5.0f; break;

                case "Bed":
                    depth = 5.0f; break;

                case "Wall":
                    depth = 2.0f; break;
            }
        }

        return depth;
    }

    public void GameObjectCreate(List<Relation> ARRelations, List<SceneObject_Unity> ARSceneObjects)
    {
        List<SceneObject_Unity> set = new List<SceneObject_Unity>();
        List<SceneObject_Unity> failset = new List<SceneObject_Unity>();

        Vector3? myPosition = new Vector3();
        SceneObject_Unity currentNode;
        bool relationReverse;

        set.AddRange(ARSceneObjects.FindAll(node => node.Real == true));
        int virtualNum = ARSceneObjects.FindAll(node => node.Real != true).Count();

        Debug.Log("set number:" + set.Count);

        while (set.Count != ARSceneObjects.Count) //TODO: Check to ensure the graph is a connetced graph
        {
            if (virtualNum == GameSceneObjects.Count)
            {
                Debug.Log("ALL not match!");
                break;
            }

            foreach (Relation relation in ARRelations) // null = virtual but cannot place
            {
                relationReverse = false;
                GameObject myObject = new GameObject();

                if (set.Contains(relation.StartNode) && !set.Contains(relation.EndNode) || !set.Contains(relation.StartNode) && set.Contains(relation.EndNode))
                {

                    //Debug.Log(relation.StartNode.Category + relation.Relationship + relation.EndNode.Category);
                    if (set.Contains(relation.StartNode)) // StartNode <- EndNode
                    {
                        currentNode = relation.EndNode;
                        myObject = GameObject.Instantiate(unity_object_Dict[currentNode].Prefab);
                        currentNode.Bounds = myObject.GetComponent<BoxCollider>().bounds;
                        relationReverse = true;
                        myPosition = VirtualPlaceOnReal(myObject, relation, relationReverse);

                    }
                    else // StartNode -> EndNode
                    {
                        currentNode = relation.StartNode;
                        myObject = GameObject.Instantiate(unity_object_Dict[currentNode].Prefab);
                        currentNode.Bounds = myObject.GetComponent<BoxCollider>().bounds;
                        myPosition = VirtualPlaceOnReal(myObject, relation, relationReverse);
                    }

                    set.Add(currentNode);
                    if (!myPosition.HasValue)
                    {
                        failset.Add(currentNode);
                        Destroy(myObject);
                        Debug.LogError("No suitalbe place to assign for this object!!!");
                    }
                    else
                    {
                        //GameObject myObject = GameObject.Instantiate(unity_object_Dict[currentNode].Prefab);
                        myObject.transform.position = myPosition.Value;
                        SceneObject_Unity otherNode = currentNode == relation.StartNode ? relation.EndNode : relation.StartNode;
                        if (relation.Relationship != RelationType.Above)
                        {
                            if(relation.Relationship == RelationType.InFrontOf && otherNode.Category == "Wall")
                            {
                                Vector3 wallNormal = otherNode.Position.transform.forward; // 对于Unity中的GameObject，forward表示对象的前方向，也就是法线方向

                                // 旋转物体以面向墙的内侧
                                myObject.transform.forward = -wallNormal;
                            }
                            else
                            {
                                Vector3 targetPostition = new Vector3(otherNode.Position.position.x, myObject.transform.position.y, otherNode.Position.position.z);
                                myObject.transform.LookAt(targetPostition);
                            }
                            
                        }
                        else
                        {
                            Vector3 eulerRot = otherNode.Position.rotation.eulerAngles;

                            eulerRot.x = 0;
                            eulerRot.z = 0;
                            

                            // 应用新的旋转
                            if (currentNode.Category == "Character")
                            {
                                //eulerRot.y += 90.0f; ;
                                myObject.transform.rotation = Quaternion.Euler(eulerRot);
                                //myObject.transform.rotation = SitOppositeBackOfChair(myObject.transform.position, 15.0f, Quaternion.Euler(eulerRot));
                            }

                            else { myObject.transform.rotation = Quaternion.Euler(eulerRot); }

                            if(currentNode.Category == "Terrain")//Scale to same size
                            {
                                

                                if (otherNode.Category == "Floor")
                                {
                                    
                                  
                                }
                                else
                                {
                                    Bounds boundsB = CalculateCompositeBounds(myObject); //虚拟物体的bounds
                                    Bounds boundsA = otherNode.Bounds;
                                    bool xLongerInOther = boundsA.size.x > boundsA.size.z;
                                    bool xLongerInBoundsB = boundsB.size.x > boundsB.size.z;

                                    float scaleX = 1;
                                    float scaleZ = 1;

                                    if ((xLongerInOther && xLongerInBoundsB) || (!xLongerInOther && !xLongerInBoundsB))
                                    {
                                        // 使用第二组公式
                                        scaleX = boundsA.size.x / boundsB.size.x;
                                        scaleZ = boundsA.size.z / boundsB.size.z;
                                    }
                                    else
                                    {
                                        // 使用第一组公式
                                        scaleX = boundsA.size.x / boundsB.size.z;
                                        scaleZ = boundsA.size.z / boundsB.size.x;
                                    }
                                    // 计算缩放比例
                                    /*float scaleX = otherNode.Bounds.size.x / boundsB.size.z;
                                    float scaleZ = otherNode.Bounds.size.z / boundsB.size.x;

                                    //float scaleX = otherNode.Bounds.size.x / boundsB.size.x; //根据情况判断，跟原点到物体的连线有关系，bbx界定x和z轴
                                    //float scaleZ = otherNode.Bounds.size.z / boundsB.size.z;*/

                                    float balancedScale = Mathf.Min(scaleX, scaleZ); // 可选：选择一个更加平衡的缩放比例

                                    myObject.transform.localScale = new Vector3(scaleX, balancedScale, scaleZ);

                                    bool isALongerInX = boundsA.size.x > boundsA.size.z;
                                    bool isBLongerInX = boundsB.size.x > boundsB.size.z;

                                    // 计算需要的旋转
                                    // 如果两个物体的长边方向不一致，则需要旋转物体B
                                    if (isALongerInX != isBLongerInX)
                                    {
                                        // 以Y轴为中心旋转90度，使得长短边匹配
                                        myObject.transform.Rotate(0, 90, 0, Space.World);
                                    }

                                }

                                
                            }
                        }

                        currentNode.Position.position = new Vector3(myPosition.Value.x, myPosition.Value.y + myObject.GetComponent<BoxCollider>().bounds.size.y, myPosition.Value.z);
                        currentNode.Position.rotation = myObject.transform.rotation;
                        ObjectsToPlace.Add(myObject);

                    }
                    break;

                } // set.Contains(relation.StartNode) && set.Contains(relation.EndNode) || !set.Contains(relation.StartNode) && !set.Contains(relation.EndNode) => continue


            }
        }


    }

    Bounds CalculateCompositeBounds(GameObject compositeObject)
    {
        Renderer[] childRenderers = compositeObject.GetComponentsInChildren<Renderer>();
        if (childRenderers.Length == 0) return new Bounds(compositeObject.transform.position, Vector3.zero);

        Bounds compositeBounds = childRenderers[0].bounds;
        foreach (Renderer renderer in childRenderers)
        {
            compositeBounds.Encapsulate(renderer.bounds);
        }
        return compositeBounds;
    }

    public Vector3? VirtualPlaceOnReal(GameObject Prefab, Relation relation, bool isReverse) // One of the node is real
    {
        Relation relation1 = new Relation(relation.StartNode, relation.EndNode, relation.Relationship);


        if (isReverse)
        {
            relation1 = RelationReverse(relation);
            Debug.Log("is reverse!");
        }
        List<GameObject> Floors = new List<GameObject>();

        if (Prefab != null) //null的情况要用系统自带的预制件，再讨论
        {

            if (relation1.Relationship == RelationType.Above || relation1.Relationship == RelationType.Below) //Put the virtual object on the real object
            {

                if (relation1.Relationship == RelationType.Above)
                {
                    if (!relation1.EndNode.Real)
                    {
                        if (relation1.StartNode.Float)
                        {
                            return relation1.EndNode.Position.position + new Vector3(0, 0.6f, 0);
                        }
                        return relation1.EndNode.Position.position; //ToDO: 写一些避免重合的代码，参考底下的
                    }
                    else
                    {

                        Vector3 platformposition = relation1.EndNode.Position.position;
                        Bounds platformBounds = new Bounds();
                        float raycastDistance = 0.5f; // 射线的长度

                        // 发射一条射线从物体下方向下
                        RaycastHit[] hits;
                        hits = Physics.RaycastAll(new Vector3(platformposition.x, platformposition.y + 0.1f, platformposition.z), Vector3.down, raycastDistance);
                        Debug.DrawRay(new Vector3(platformposition.x, platformposition.y + 0.1f, platformposition.z), Vector3.down, Color.yellow, 500.0f);


                        RaycastHit nearestHit = GetNearestPlatformHit(hits);


                        if (nearestHit.transform != null) // 如果找到了最近的Platform
                        {
                            platformBounds = nearestHit.transform.gameObject.GetComponent<MeshRenderer>().bounds;

                            if (relation1.StartNode.Float)
                            {
                                return PlatformPosition(Prefab, platformposition, platformBounds, nearestHit.transform.gameObject.name) + new Vector3(0, 0.6f, 0);
                            }
                            return PlatformPosition(Prefab, platformposition, platformBounds, nearestHit.transform.gameObject.name);

                        }else

                        {
                            Debug.Log("Raycast is null!");
                            return new Vector3(0, 0, 0);
                        }

                       


                    }


                }
                else
                {
                    return new Vector3(relation1.EndNode.Position.position.x, relation1.EndNode.Position.position.y - 0.35f, relation1.EndNode.Position.position.z);
                }

            }
            else
            {
                if (relation1.EndNode.Category == "Wall")
                {
                    Vector3 center = relation1.EndNode.Position.position;
                    if (relation1.Relationship != RelationType.InFrontOf)
                    {
                        Debug.Log("Invalid relationship for a wall. The object can only be in front of the wall.");
                        return null; // 结束函数，因为我们不处理墙的其他关系类型
                    }

                    float step = 0.1f;
                    List<Vector3> searchDirections = new List<Vector3> { Vector3.forward }; // 墙只考虑前方

                    for (float distance = step; distance <= 2; distance += step)
                    {
                        foreach (var direction in searchDirections)
                        {
                            Vector3 potentialPosition = center + direction * distance;

                            // 由于物体只能在墙的前面，我们不需要调整 y 值
                            potentialPosition.y = center.y;

                            if (IsRelationMatched(relation1.Relationship, RelationJudgeForFloat(relation1, potentialPosition)))
                            {
                                return potentialPosition;
                            }
                        }
                    }

                    Debug.Log("No valid position found in front of the wall.");
                    return null;
                }

                if (!relation1.StartNode.Float) //放在地板上
                {
                    List<Vector3> Positions = new List<Vector3>();
                    Floors = FloorSearch(Floors);

                    var putPosition = Vector3.zero;

                    foreach (var floor in Floors)
                    {
                        var positions = objectAutoPlace.GoodPositions(Prefab, floor.transform.position, mergedBounds);

                        Positions.AddRange(positions);
                    }

                    float closestDistance = float.MaxValue;
                    Vector3 closestPosition = Vector3.zero;

                    /////////////////////////////////////////// Should add matching differ from different objects pair //////////////////////////////////
                    Vector3 endNodePosition = new Vector3(relation1.EndNode.Position.position.x, 0, relation1.EndNode.Position.position.z);
                    foreach (var position in Positions)
                    {
                        Vector3 targetPosition = new Vector3(position.x, 0, position.z);
                        RelationType currentRelation = RelationJudge(relation1, position);

                        if (IsRelationMatched(relation1.Relationship, currentRelation))
                        {

                            float distance = Vector3.Distance(endNodePosition, targetPosition);
                            if (distance < closestDistance)
                            {
                                closestDistance = distance;
                                closestPosition = position;
                            }
                        }
                    }

                    if (closestPosition != Vector3.zero)
                    {
                        putPosition = closestPosition;
                        return putPosition;
                    }

                    Debug.LogError("ERROR! Due to phsical space, no suitable place to put " + Prefab.name);
                    return null;



                }
                else //放在参考物同一水平面
                {
                    Vector3 center = relation1.EndNode.Position.position;
                    float step = 0.4f;

                    List<Vector3> searchDirections = new List<Vector3>();

                    switch (relation1.Relationship)
                    {
                        case RelationType.InFrontOf:
                            searchDirections.Add(Vector3.forward);
                            break;
                        case RelationType.Behind:
                            searchDirections.Add(Vector3.back);
                            break;
                        case RelationType.Left:
                            searchDirections.Add(Vector3.left);
                            break;
                        case RelationType.Right:
                            searchDirections.Add(Vector3.right);
                            break;
                        case RelationType.Above:
                            searchDirections.Add(Vector3.up);
                            break;
                        case RelationType.Below:
                            searchDirections.Add(Vector3.down);
                            break;
                        case RelationType.RightLeft:
                            searchDirections.AddRange(new List<Vector3> { Vector3.left, Vector3.right });
                            break;
                        case RelationType.FrontBehind:
                            searchDirections.AddRange(new List<Vector3> { Vector3.forward, Vector3.back });
                            break;
                        case RelationType.Around:
                            searchDirections.AddRange(new List<Vector3> { Vector3.forward, Vector3.back, Vector3.left, Vector3.right });
                            break;
                    }

                    for (float distance = step; distance <= 2; distance += step)
                    {
                        foreach (var direction in searchDirections)
                        {
                            Vector3 potentialPosition = center + direction * distance;

                            // 如果关系不是上或下，保持y值不变
                            if (relation1.Relationship != RelationType.Above && relation1.Relationship != RelationType.Below)
                            {
                                potentialPosition.y = center.y;
                            }

                            if (IsRelationMatched(relation1.Relationship, RelationJudgeForFloat(relation1, potentialPosition)))
                            {
                                return potentialPosition;
                            }
                        }
                    }

                    return null;
                }



            }
        }

        else
        {
            Debug.LogError("Object Prefab is null!");
            return null;
        }
    }

    public Quaternion SitOppositeBackOfChair(Vector3 position, float tolerance, Quaternion ori)
    {
        Quaternion orientation = ori;

        // 在座位位置周围创建一个球形碰撞检测区域
        Collider[] hitColliders = Physics.OverlapSphere(position, 0.3f);

        foreach (var hitCollider in hitColliders)
        {
            Debug.Log("How many hits" + hitCollider.name + Mathf.Abs(Vector3.Angle(hitCollider.gameObject.transform.up, Vector3.up)));
            if (hitCollider.gameObject.name == "BackgroundMesh" && Mathf.Abs(Vector3.Angle(hitCollider.gameObject .transform.up, Vector3.up)) < tolerance)
            {
                Vector3 toBack = ((hitCollider.gameObject.transform.position - position).normalized);

                // Step 2: 获取椅背的法线
                Vector3 backNormal = (hitCollider.gameObject.transform.forward);
                Debug.DrawRay(hitCollider.gameObject.transform.position, hitCollider.gameObject.transform.forward, Color.red, 50.0f);
                Debug.DrawRay(hitCollider.gameObject.transform.position, toBack, Color.blue, 50.0f);


                // Step 3: 判断法线的朝向是否与座位相对
                // Step 4: 确定正确的朝向
                if (Vector3.Dot(toBack, backNormal) > 0)
                {
                    // 如果法线朝向座位，表示椅背的正面朝向座位，人物应该面向椅背的背面（即法线的反方向）
                    orientation = Quaternion.LookRotation(-backNormal);
                }
                else
                {
                    // 如果法线朝向座位的反方向，表示椅背的背面朝向座位，人物应该面向椅背的正面（即法线的方向）
                    orientation = Quaternion.LookRotation(backNormal);
                }
            }
        }

        return orientation;
    }
    private RelationType RelationJudgeForFloat(Relation relation, Vector3 objectB)
    {
        SceneObject_Unity objectA = relation.EndNode;
        Vector3 centerAtoB = objectB - objectA.Position.position;

        // 垂直方向判断
        if (Mathf.Abs(centerAtoB.y) >= 0.3f) // 这个阈值可以根据需要进行调整
        {
            if (centerAtoB.y < 0)
                return RelationType.Below;
            else return RelationType.Above;
        }

        // 水平方向判断
        if (Mathf.Abs(centerAtoB.z) > 0.1f)
        {
            if (centerAtoB.z > 0)
                return RelationType.InFrontOf;
            else return RelationType.Behind;
        }

        if (Mathf.Abs(centerAtoB.x) > 0.1f)
        {
            if (centerAtoB.x > 0)
                return RelationType.Right;
            else return RelationType.Left;
        }

        // 如果仍然没有匹配的关系，返回None
        return RelationType.None;
    }
    private bool IsRelationMatched(RelationType targetRelation, RelationType currentRelation)
    {
        switch (targetRelation)
        {
            case RelationType.RightLeft:
                return currentRelation == RelationType.Right || currentRelation == RelationType.Left;
            case RelationType.FrontBehind:
                return currentRelation == RelationType.InFrontOf || currentRelation == RelationType.Behind;
            case RelationType.Around:
                return currentRelation == RelationType.InFrontOf || currentRelation == RelationType.Behind || currentRelation == RelationType.Right || currentRelation == RelationType.Left || currentRelation == RelationType.Above || currentRelation == RelationType.Below;
            default:
                return targetRelation == currentRelation;
        }
    }
    public Relation RelationReverse(Relation relation)
    {

        SceneObject_Unity newStartNode = new SceneObject_Unity(relation.EndNode);
        SceneObject_Unity newEndNode = new SceneObject_Unity(relation.StartNode);

        RelationType newRelationship = relation.Relationship;

        switch (relation.Relationship)
        {
            case RelationType.Above:
                newRelationship = RelationType.Below;
                break;

            case RelationType.Below:
                newRelationship = RelationType.Above;
                break;

            case RelationType.Left:
                newRelationship = RelationType.Right;
                break;

            case RelationType.Right:
                newRelationship = RelationType.Left;
                break;

            case RelationType.InFrontOf:
                newRelationship = RelationType.Behind;
                break;

            case RelationType.Behind:
                newRelationship = RelationType.InFrontOf;
                break;
        }

        return new Relation(newStartNode, newEndNode, newRelationship);
    }
    public Vector3 PlatformPosition(GameObject Object, Vector3 Position, Bounds bound, string colliderObject)
    {
        //TMP.text += Object.name;
        List<Vector3> goodPoints = new List<Vector3>();
        BoxCollider Collider = Object.GetComponent<BoxCollider>();
        Vector3 colliderSize = Collider.bounds.size;
        // 检测是否与其他物体发生重叠

        float gridSize = 0.2f; // 网格单元的大小
        float halfGridSize = gridSize / 2f; // 网格单元大小的一半

        Vector3 center = bound.center;
        Vector3 extent = bound.extents;

        // 计算在x和z轴上的最大偏移量
        float maxXOffset = extent.x;
        float maxZOffset = extent.z;

        Collider[] collidersAtCenter = Physics.OverlapBox(center, colliderSize);
        bool isCenterOccupied = false;
        foreach (var collider in collidersAtCenter)
        {
            // 假设 colliderObject 是一个 GameObject 的引用，你想检查 collider 不是这个 GameObject 的一部分
            if (collider.gameObject.name != colliderObject && !collider.gameObject.name.StartsWith("SpatialMesh") && !collider.gameObject.name.StartsWith("PlatformMesh") && !collider.gameObject.name.StartsWith("BackgroundMesh"))
            {
                isCenterOccupied = true;
                break; // 找到了一个非忽略的碰撞器，可以直接跳出循环
            }
        }

        if (!isCenterOccupied)
        {
            // 如果中心位置未被占用，直接返回中心位置
            return center;
        }

        // 从中心开始向四周进行搜索
        for (float xOffset = 0; xOffset <= maxXOffset; xOffset += gridSize)
        {
            for (float zOffset = 0; zOffset <= maxZOffset; zOffset += gridSize)
            {
                // 在x和z轴的正负两个方向上同时增加偏移量
                foreach (var direction in new[] { new Vector3(1, 0, 1), new Vector3(-1, 0, -1), new Vector3(1, 0, -1), new Vector3(-1, 0, 1) })
                {
                    Vector3 representativePoint = center + Vector3.Scale(direction, new Vector3(xOffset, 0, zOffset));
                    Collider[] colliders = Physics.OverlapBox(representativePoint, colliderSize);
                    

                    // 检查代表性点是否在边界内
                    if (!bound.Contains(representativePoint))
                    {
                        continue;
                    }


                    if (colliders.Length > 0)
                    {
                        int ignoredCollidersCount = 0;
                        foreach (Collider collider in colliders)
                        {
                            if (collider.gameObject.name == colliderObject || collider.gameObject.name.StartsWith("SpatialMesh") || collider.gameObject.name.StartsWith("PlatformMesh") || collider.gameObject.name.StartsWith("BackgroundMesh"))
                            {
                                ignoredCollidersCount++;
                            }

                        }

                        // 如果存在任何一个collider不是被忽略的，则跳过此次循环
                        if (ignoredCollidersCount != colliders.Length)
                        {
                            continue;
                        }

                    }

                    //是否在不规则形状上面
                    GameObject UnderObject = objectAutoPlace.GetObjectBelow(representativePoint + new Vector3(0.0f, 0.1f, 0.0f));
                    if (UnderObject == null || (UnderObject.name != colliderObject && !UnderObject.name.StartsWith("SpatialMesh")))
                    {
                        continue;
                    }

                    goodPoints.Add(representativePoint);              

                    //return representativePoint;
                }
            }
        }

        //return Position;
        if (goodPoints.Count > 0)
        {
            // 将点分为几个区域，这里以距离中心点的四分位数为例
            var distances = goodPoints.Select(point => (point - center).sqrMagnitude).ToList();
            distances.Sort(); // 对距离进行排序
            float thresholdDistance = distances[Mathf.Min(distances.Count / 4, distances.Count - 1)]; // 使用四分位距离作为阈值

            // 从离中心点最近的四分之一点中随机选择
            var nearestPoints = goodPoints.Where(point => (point - center).sqrMagnitude <= thresholdDistance).ToList();

            // 从这些最近的点中随机选择一个点
            return nearestPoints[UnityEngine.Random.Range(0, nearestPoints.Count)];
        }
        else
        {
            return Position; // 如果没有找到合适的点，返回原始位置
        }

    }
    public List<GameObject> FloorSearch(List<GameObject> Floors)
    {

        foreach (var sceneObject in myScene.SceneObjects)
        {

            // Find a wall
            if (sceneObject.Kind == SceneObjectKind.Floor)
            {

                // Get the quad
                //var marker = GameObject.Instantiate(markerPrefab);
                var marker = new GameObject();

                marker.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                marker.transform.SetParent(parentObject.transform);

                marker.transform.localPosition = sceneObject.Position.ToUnity();
                //marker.transform.localPosition = new Vector3(marker.transform.localPosition.x, marker.transform.localPosition.y, marker.transform.localPosition.z + 0.05f);
                marker.transform.localRotation = sceneObject.Orientation.ToUnity();

                Floors.Add(marker);

                //TODO: 判断地板大小，不要出界


            }
        }

        List<GameObject> Floors1 = new List<GameObject>();
        Floors1 = Floors.OrderBy(p => Vector3.Distance(Vector3.zero, p.transform.position)).ToList();

        return Floors1;
    }

    public List<ObjectNodeData> LoadSceneObjectsFromFile(string filePath)
    {
        List<ObjectNodeData> objects = new List<ObjectNodeData>();

        using (StreamReader reader = new StreamReader(filePath))
        {
            ObjectNodeData currentObj = null;
            string line;

            while ((line = reader.ReadLine()) != null)
            {
                if (line.StartsWith("Data"))
                {
                    currentObj = new ObjectNodeData();
                    objects.Add(currentObj);
                }
                else if (currentObj != null)
                {
                    string[] parts = line.Split(new string[] { " = " }, StringSplitOptions.None);
                    if (parts.Length == 2)
                    {
                        string key = parts[0].Trim();
                        string value = parts[1].Trim();

                        switch (key)
                        {
                            case "ID":
                                currentObj.ID = int.Parse(value);
                                break;

                            case "GID":
                                currentObj.GID = value;
                                break;

                            case "Name":
                                currentObj.name = value;
                                break;

                            case "Category":
                                currentObj.category = value;
                                break;

                            case "Size":
                                currentObj.size = float.Parse(value);
                                break;

                            case "Height":
                                currentObj.height = float.Parse(value);
                                break;

                            case "Horizontal":
                                currentObj.horizontal = bool.Parse(value);
                                break;

                            case "Prefab":
                                if (value != "N/A")
                                {
                                    GameObject prefab = Resources.Load(value) as GameObject;
                                    if (prefab == null)
                                    {
                                        Debug.LogError("Could not load prefab from path: " + value);
                                    }
                                    else
                                    {
                                        currentObj.Prefab = prefab;
                                    }
                                }
                                break;
                        }
                    }
                }
            }
        }

        return objects;
    }

    public List<ObjectNodeData> LoadSceneObjectsFromFile1(string file)
    {
        List<ObjectNodeData> objects = new List<ObjectNodeData>();

        using (StringReader reader = new StringReader(file))
        {
            ObjectNodeData currentObj = null;
            string line;

            while ((line = reader.ReadLine()) != null)
            {
                if (line.StartsWith("Data"))
                {
                    currentObj = new ObjectNodeData();
                    objects.Add(currentObj);
                }
                else if (currentObj != null)
                {
                    string[] parts = line.Split(new string[] { " = " }, StringSplitOptions.None);
                    if (parts.Length == 2)
                    {
                        string key = parts[0].Trim();
                        string value = parts[1].Trim();

                        switch (key)
                        {
                            case "ID":
                                currentObj.ID = int.Parse(value);
                                break;

                            case "GID":
                                currentObj.GID = value;
                                break;

                            case "Name":
                                currentObj.name = value;
                                break;

                            case "Category":
                                currentObj.category = value;
                                break;

                            case "Size":
                                currentObj.size = float.Parse(value);
                                break;

                            case "Height":
                                currentObj.height = float.Parse(value);
                                break;

                            case "Horizontal":
                                currentObj.horizontal = bool.Parse(value);
                                break;

                            case "Float":
                                currentObj.Float = bool.Parse(value);
                                break;

                            case "Prefab":
                                if (value != "N/A")
                                {
                                    GameObject prefab = Resources.Load(value) as GameObject;
                                    if (prefab == null)
                                    {
                                        Debug.LogError("Could not load prefab from path: " + value);
                                    }
                                    else
                                    {
                                        currentObj.Prefab = prefab;
                                    }
                                }
                                break;
                        }
                    }
                }
            }
        }

        return objects;
    }

    public List<Relation> LoadRelationsFromFile(string filePath)
    {
        List<Relation> relations = new List<Relation>();
        SceneObject_Unity startMatch = null;
        SceneObject_Unity endMatch = null;
        using (StreamReader reader = new StreamReader(filePath))
        {
            string line;
            string startNodeGUID = null;
            string endNodeGUID = null;
            RelationType relationType = RelationType.None;
            int line_num = 0;
            int type_line;

            while ((line = reader.ReadLine()) != null)
            {
                type_line = line_num % 4;
                switch (type_line)
                {
                    case 0:
                        startNodeGUID = line.Substring(7).Trim();
                        startMatch = GameSceneObjects.Find(n => n.GUID == startNodeGUID);
                        break;
                    case 1:
                        string relationTypeString = line.Substring(11).Trim(); // Read relation type
                        Enum.TryParse(relationTypeString, out relationType);
                        break;
                    case 2:
                        endNodeGUID = line.Substring(7).Trim();
                        endMatch = GameSceneObjects.Find(n => n.GUID == endNodeGUID);

                        break;
                    case 3:
                        Relation relation = new Relation(startMatch, endMatch, relationType);
                        relations.Add(relation);
                        // Find start node and end node based on their GUIDs
                        break;


                }
                line_num++;
            }
        }

        return relations;
    }

    public void LoadParametersFromFile(string fileContent)
    {
        using (StringReader reader = new StringReader(fileContent))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {

                string[] parts = line.Split(':');
                if (parts.Length == 2)
                {
                    string key = parts[0].Trim();
                    string value = parts[1].Trim();
                    switch (key)
                    {
                        case "MachingAngle":
                            MatchingAngle = float.Parse(value);
                            break;
                        case "HorizontalMatchingRange":
                            HMatchingRange = float.Parse(value);
                            break;
                        case "VerticalMatchingRange":
                            VMatchingRange = float.Parse(value);
                            break;
                        case "SelectedStrategy":
                            SelectedStrategy = value;
                            break;
                        case "DecisionRange":
                            DecisionRange = float.Parse(value);
                            break;
                        case "CoverageThreshold":
                            CoverageThreshold = float.Parse(value);
                            break;
                    }
                }
            }
        }

        Debug.Log("Matching Angle: " + MatchingAngle);
        Debug.Log("Horizontal Matching Range: " + HMatchingRange);
        Debug.Log("Vertical Matching Range: " + VMatchingRange);
        Debug.Log("Selected Strategy: " + SelectedStrategy); // 显示选中的策略名称
        Debug.Log("Decision Range: " + DecisionRange);
        Debug.Log("Coverage Threshold: " + CoverageThreshold);
    }

    public List<Relation> LoadRelationsFromFile1(string file)
    {
        List<Relation> relations = new List<Relation>();
        SceneObject_Unity startMatch = null;
        SceneObject_Unity endMatch = null;
        using (StringReader reader = new StringReader(file))
        {
            string line;
            string startNodeGUID = null;
            string endNodeGUID = null;
            RelationType relationType = RelationType.None;
            int line_num = 0;
            int type_line;

            while ((line = reader.ReadLine()) != null)
            {
                type_line = line_num % 4;
                switch (type_line)
                {
                    case 0:
                        startNodeGUID = line.Substring(7).Trim();
                        startMatch = GameSceneObjects.Find(n => n.GUID == startNodeGUID);
                        break;
                    case 1:
                        string relationTypeString = line.Substring(11).Trim(); // Read relation type
                        Enum.TryParse(relationTypeString, out relationType);
                        break;
                    case 2:
                        endNodeGUID = line.Substring(7).Trim();
                        endMatch = GameSceneObjects.Find(n => n.GUID == endNodeGUID);

                        break;
                    case 3:
                        Relation relation = new Relation(startMatch, endMatch, relationType);
                        relations.Add(relation);
                        // Find start node and end node based on their GUIDs
                        break;


                }
                line_num++;
            }
        }

        return relations;
    }

    public void DoPlace()
    {
        FloorSize();
        Stopwatch stopwatch = new Stopwatch();

        Stopwatch stopwatch1 = new Stopwatch();
        Stopwatch stopwatch2 = new Stopwatch();
        Stopwatch stopwatch3 = new Stopwatch();


        // 开始计时
        stopwatch1.Start();
        stopwatch.Start();

        AddSceneObjects();
        ///从场景中标记物体获取对应的scene_object
        for (int i = 0; i < SceneObjects.Count; i++)
        {
            // 获取当前对象的Position.position
            Vector3 objPosition = SceneObjects[i].Position.position;
            if (Mathf.Abs(objPosition.x - LabelPosition.x) <= 0.1f &&
                Mathf.Abs(objPosition.y - LabelPosition.y) <= 0.1f &&
                Mathf.Abs(objPosition.z - LabelPosition.z) <= 0.1f)
            {

                Debug.Log("Platform"+ SceneObjects[i].Size + SceneObjects[i].Height);
                SceneObjects[i].Category = "Platform";

            }

            // 检查坐标是否在误差范围内
            if (Mathf.Abs(objPosition.x - LabelPosition1.x) <= 0.1f &&
                Mathf.Abs(objPosition.y - LabelPosition1.y) <= 0.1f &&
                Mathf.Abs(objPosition.z - LabelPosition1.z) <= 0.1f)
            {
                // 如果是，修改该元素的Category为"Unknown"

                Debug.Log("Unknown");
                SceneObjects[i].Category = "Unknown";
            }

            if (Mathf.Abs(objPosition.x - LabelPosition2.x) <= 0.1f &&
                Mathf.Abs(objPosition.y - LabelPosition2.y) <= 0.1f &&
                Mathf.Abs(objPosition.z - LabelPosition2.z) <= 0.1f)
            {
                // 如果是，修改该元素的Category为"Unknown"

                Debug.Log("Chair");
                SceneObjects[i].Category = "Chair";
            }
        }
        
        AddRelations();
        stopwatch1.Stop();

        // 打印代码执行时间 (以毫秒为单位)
        //titleText.text = " Real Scene Graph Creation " + stopwatch1.ElapsedMilliseconds + " ms";


        AddGameSceneObjects();
        stopwatch2.Start();


        var (ARSceneObjects, ARRelations) = SceneGraphMatching.CompareNodes(SceneObjects, GameSceneObjects, Relations, GameRelations);

        stopwatch2.Stop();

        // 打印代码执行时间 (以毫秒为单位)
        //titleText.text += " Matching" + stopwatch2.ElapsedMilliseconds + " ms";


        stopwatch3.Start();


        GameObjectCreate(ARRelations, ARSceneObjects);

        stopwatch3.Stop();
        stopwatch.Stop();

        // 打印代码执行时间 (以毫秒为单位)
        //titleText.text += " Content Placement" + stopwatch3.ElapsedMilliseconds + " ms";
        //titleText.text += " Sum" + stopwatch.ElapsedMilliseconds + " ms";


        Debug.Log("Real relations number: " + Relations.Count + " Game relations number: " + GameRelations.Count + " AR relations number: " + ARRelations.Count);

        using (StreamWriter writer = new StreamWriter("D://RealSceneGraph_Puzzle.txt"))
        {
            foreach (Relation relation in Relations)
            {
                if (relation.Relationship != RelationType.None)
                {
                    string log = relation.StartNode.Category + relation.StartNode.ID + relation.StartNode.Size + 
                                 " -> " + relation.EndNode.Category + relation.EndNode.ID + relation.EndNode.Size + " [label = \"" + relation.Relationship + "\"]\n";

                    writer.WriteLine(log);
                }
            }
        }

        using (StreamWriter writer = new StreamWriter("D://GameSceneGraph_Puzzle.txt"))
        {
            foreach (Relation relation in GameRelations)
            {
                if (relation.Relationship != RelationType.None)
                {
                    string log = relation.StartNode.Category + relation.StartNode.ID +
                                 " -> " + relation.EndNode.Category + relation.EndNode.ID + " [label = \"" + relation.Relationship + "\"]\n";

                    writer.WriteLine(log);
                }
            }
        }

        using (StreamWriter writer = new StreamWriter("D://ARSceneGraph_Puzzle.txt"))
        {
            foreach (Relation relation in ARRelations)
            {
                if (relation.Relationship != RelationType.None)
                {
                    string log = relation.StartNode.Category + relation.StartNode.ID +
                                 " -> " + relation.EndNode.Category + relation.EndNode.ID + " [label = \"" + relation.Relationship + "\"]\n";

                    writer.WriteLine(log);
                }

            }
        }



    }

    public void SaveSceneObjectsPositionsToFile(string filePath, List<GameObject> gameobjects)
    {
        
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            foreach (var obj in gameobjects)
            {
                //titleText.text += obj.name;

                string prefabName = obj.name.Split('(')[0]; // 移除"(Clone)"
                Vector3 pos = obj.transform.position;
                Quaternion rot = obj.transform.rotation;
                Vector3 scale = obj.transform.localScale;

                writer.WriteLine($"{prefabName};{pos.x},{pos.y},{pos.z};{rot.x},{rot.y},{rot.z},{rot.w};{scale.x},{scale.y},{scale.z}");
            }
        }
        //titleText.text += "Save current gameobjects!";


    }

    public void LoadSceneObjects(string filePath)
    {
        if (!File.Exists(filePath)) return;

        string[] lines = File.ReadAllLines(filePath);
        foreach (string line in lines)
        {
            string[] parts = line.Split(';');
            string prefabName = parts[0];
            Vector3 position = new Vector3(
                float.Parse(parts[1].Split(',')[0]),
                float.Parse(parts[1].Split(',')[1]),
                float.Parse(parts[1].Split(',')[2]));
            Quaternion rotation = new Quaternion(
                float.Parse(parts[2].Split(',')[0]),
                float.Parse(parts[2].Split(',')[1]),
                float.Parse(parts[2].Split(',')[2]),
                float.Parse(parts[2].Split(',')[3]));
            Vector3 scale = new Vector3(
                float.Parse(parts[3].Split(',')[0]),
                float.Parse(parts[3].Split(',')[1]),
                float.Parse(parts[3].Split(',')[2]));

            GameObject prefab = Resources.Load<GameObject>(prefabName);
            if (prefab)
            {
                GameObject obj = Instantiate(prefab, position, rotation);
                obj.transform.localScale = scale;
                Debug.Log("Load Gameobject: " +  obj.name);
                //titleText.text = string.Join("\n", lines);
            }
            else
            {
                Debug.LogError("Prefab not found: " + prefabName);
            }
        }
    }

    public void WorldLockingLoad()
    {
        WorldLockingManager.GetInstance().Load();
    }

    public void ClickButton1()//Save current gameobjects' positions
    {
        Debug.Log("Save current gameobjects!");
        string path = System.IO.Path.Combine(Application.persistentDataPath, "CurrentObjects.txt");
        SaveSceneObjectsPositionsToFile(path, ObjectsToPlace);
    }

    public void ClickButton2()//Load last positions
    {
        Debug.Log("Load last gameobjects!");
        string path = System.IO.Path.Combine(Application.persistentDataPath, "CurrentObjects.txt");
        LoadSceneObjects(path);
    }

    public void ClickButton3()
    {
        Debug.Log("Button3");
    }

    public void LabelPlatform()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit;

        // 执行射线检测
        if (Physics.Raycast(ray, out hit))
        {
            // 射线与物体相交，获取该物体的GameObject
            GameObject hitObject = hit.collider.gameObject;

            // 在此处添加你的逻辑，例如处理选中的物体
            Debug.Log("Hit Object: " + hitObject.name);

            if(!hitObject.name.StartsWith("SpatialMesh"))
            {
                LabelPosition = hitObject.transform.position;
                hitObject.GetComponent<Renderer>().material = newMaterial;
            }
            
            

        }
    }

    public void LabelUnknown()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit;

        // 执行射线检测
        if (Physics.Raycast(ray, out hit))
        {
            // 射线与物体相交，获取该物体的GameObject
            GameObject hitObject = hit.collider.gameObject;

            Debug.Log("Hit Object: " + hitObject.name);

            LabelPosition1 = hitObject.transform.position;

        }
    }

    public void LabelChair()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit;

        // 执行射线检测
        if (Physics.Raycast(ray, out hit))
        {
            // 射线与物体相交，获取该物体的GameObject
            GameObject hitObject = hit.collider.gameObject;

            Debug.Log("Hit Object: " + hitObject.name);

            LabelPosition2 = hitObject.transform.position;

        }
    }



}