using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.SceneUnderstanding;
using Microsoft.MixedReality.SceneUnderstanding.Samples.Unity;
using NumericsConversion;
using System.Linq;



public class ObjectAutoPlace : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject parentObject;
    public GameObject Character;
    public GameObject Cup;
    public GameObject Photo;
    public GameObject Window;
    public GameObject Lamp;
    public GameObject Menu;
    public GameObject ManOnChair;
    public GameObject Table;

    public List<GameObject> Platforms;
    public List<GameObject> Floors;
    
    string Label = null;    
    
    public List<GameObject> PlatformObjects;


    public Material PlatformMaterial;
    public Material WallMaterial;
    public Material FloorMaterial;
    public Material LabelMaterial;

    Scene myScene;
    public GameObject markerPrefab;
    public GameObject quadPrefab;
    public GameObject placePrefab;

    public Transform floor;
    public float boundaryPadding = 0.1f;

    private Vector3 floorMinBounds;
    private Vector3 floorMaxBounds;
    List<GameObject> FloorObjects = new List<GameObject>();
    Bounds mergedBounds = new Bounds();


    private void Update()
    {
        myScene = GameObject.Find("SceneUnderstandingManager").GetComponent<SceneUnderstandingManager>().GetLatestDeserializedScene();
      
    }

    private void Start()
    {
        StartCoroutine(WaitForMyScene());

    }

    private IEnumerator WaitForMyScene()
    {
        yield return new WaitUntil(() => GameObject.Find("Floor") != null);
        yield return new WaitForSeconds(1f);

        InvokeRepeating("DoPlace", 0, 1.0f);


        GameObject[] objects = FindObjectsOfType<GameObject>();


        foreach (GameObject obj in objects)
        {

            if (obj.name == "FloorMesh")
            {
                FloorObjects.Add(obj);
            }
        }

        Debug.Log("地板的物体数量为：" + FloorObjects.Count);

        mergedBounds = FloorObjects[0].GetComponent<MeshRenderer>().bounds;//初始化避免包括原点

        foreach (GameObject obj in FloorObjects)
        {
            // 获取物体的边界信息
            MeshRenderer renderer = obj.GetComponent<MeshRenderer>();
            Bounds bounds = renderer.bounds;
            Debug.Log("范围大小:" + bounds.max + bounds.min);


            mergedBounds.Encapsulate(bounds);


        }

    }

    public void PlatformPlace ()
    {
        //TODO:一张桌子上出现多个物体，分配位置

        Platforms = PlatformSearch(Platforms);

        Floors = FloorSearch(Floors);


        Debug.Log("平台的数量是：" + Platforms.Count);

        int Need_Platform = 6;

        if(Platforms.Count >= Need_Platform)//Assign platform
        {
            Debug.Log("检测到的平台的数量大于需要的平台数量：");

            //TODO: Create list of gameobject, loop to assign
            for (int j = 0; j < PlatformObjects.Count; j++)
            {
                Instantiate(PlatformObjects[j], new Vector3(Platforms[j].transform.position.x, Platforms[j].transform.position.y + 0.02f, Platforms[j].transform.position.z), PlatformObjects[j].transform.rotation);

            }


            //Platforms[0].transform.Rotate(180.0f, 0.0f, 0.0f);

            //Instantiate(Photo, new Vector3(Platforms[1].transform.position.x, Platforms[1].transform.position.y + 0.14f, Platforms[1].transform.position.z), Photo.transform.rotation);

        }
        //Exist i platform, but n platforms are needed, creat n-i virtual platform
        else if(Platforms.Count > 0 )//Fisrt assign, then Create platform on floor
        {
            Debug.Log("检测到的平台的数量小于需要的平台数量， 且平台存在！");


            for ( int i = 0; i < Platforms.Count; i++)
            {
                Instantiate(PlatformObjects[i], new Vector3(Platforms[i].transform.position.x, Platforms[i].transform.position.y + 0.02f, Platforms[i].transform.position.z), PlatformObjects[i].transform.rotation);
                Debug.Log("检测到平台放置");
            }



            for (int n = Platforms.Count; n < Need_Platform; n++)
            {

                Vector3 goodposition = GoodPosition(Table, Floors[0].transform.position, mergedBounds);

                Instantiate(Table, goodposition, Table.transform.rotation);

                GameObject TableObject = Instantiate(PlatformObjects[n]);

                TableObject.transform.position = goodposition + new Vector3(0.0f, 0.7f, 0.0f);


                Debug.Log("虚拟平台放置");

            }

            // 检测是否与其他物体发生重叠


        }
        else //Create
        {
            Debug.Log("场景中不存在平台！");

            for (int i = 0; i < Need_Platform; i++)
            {
                Floors = FloorSearch(Floors);

                Vector3 goodposition = GoodPosition(PlatformObjects[i], Floors[0].transform.position, mergedBounds);

                Instantiate(PlatformObjects[i], goodposition, PlatformObjects[i].transform.rotation);

            }

        }

    }

    public GameObject GetObjectBelow(Vector3 Position)
    {
        float raycastDistance = 10f; // 射线的长度

        // 发射一条射线从物体下方向下
        RaycastHit hit;
        if (Physics.Raycast(Position, Vector3.down, out hit, raycastDistance))
        {
            // 检测到物体，返回被击中的物体
            return hit.transform.gameObject;
        }

        // 没有检测到物体，返回 null
        return null;
    }

    public void FindSittable()
    {

        List<GameObject> Sittable = new List<GameObject>();


        foreach (var sceneObject in myScene.SceneObjects)
        {

            // Find a wall
            if (sceneObject.Kind == SceneObjectKind.Background)
            {
              
                // Get the quad
                //var marker = GameObject.Instantiate(markerPrefab);
                var marker = new GameObject();

                //marker.transform.position = new Vector3(1, 0, 0);
                //marker.transform.rotation = Quaternion.identity;
                //marker.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
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
                        if(marker.transform.position.y < -0.9f && marker.transform.position.y > -1.3f)
                        { 
                            Hori_upward = true;
                        
                        }
                        /*Hori_upward = true;
                        Debug.Log(marker.transform.position.y);*/


                    }
                }


                var quads = sceneObject.Quads;

                if (quads.Count > 0 && Hori_upward)//quads not empty && horizontal && facing upward 
                {

                    SceneQuad largestQuad = null;
                    float fLargestArea = float.NegativeInfinity;
                    foreach (SceneQuad quad in quads)
                    {
                        var quad1 = GameObject.CreatePrimitive(PrimitiveType.Cube);

                        quad1.transform.SetParent(marker.transform, false);
                        quad1.transform.localPosition = new Vector3(0.0f, 0.0f, -0.03f);

                        quad1.transform.localScale = new Vector3(
                            5, 5, 0.005f);

                        quad1.GetComponent<Renderer>().material = PlatformMaterial;


                        System.Numerics.Vector2 extents = quad.Extents;
                        float area = extents.X * extents.Y;

                        if (area > fLargestArea)
                        {
                            fLargestArea = area;
                            largestQuad = quad;
                        }
                    }
                    // Find a good location for a 1mx1m object  

                    System.Numerics.Vector2 location;


                    if (largestQuad.FindCentermostPlacement(new System.Numerics.Vector2(0.3f, 0.3f), out location))
                    {
                        //GameObject location1 = GameObject.Instantiate(placePrefab);
                        GameObject location1 = new GameObject();
                        location1.transform.parent = marker.transform;
                        location1.transform.localPosition = Vector3.zero;
                        location1.transform.localRotation = Quaternion.identity;

                        //Our pivot gameobject starts in the middle of the platform, that's the local origin in Unity, but we need to move it to the top left 
                        //corner of the object. 'FindCenterMostPlacement' coordinates work in 'Quad Space'
                        //they start at the top left of the Quad, and go down and right correspondingly in X and Y axis

                        //Move to the starting position aka. Top Left Corner
                        location1.transform.Translate(-(largestQuad.Extents.X / 2.0f), largestQuad.Extents.Y / 2.0f, -0.05f, Space.Self);

                        //Move to the CenterMost Place -- Y is negate so that we move right and down, according to the API 'FindCenterMostPlacement'
                        //https://docs.microsoft.com/en-us/windows/mixed-reality/develop/platform-capabilities-and-apis/scene-understanding-SDK#quad
                        location1.transform.Translate(location.X, -location.Y, 0.0f, Space.Self);
                        //location1.transform.Rotate(0.0f, 0.0f, 0.0f);


                        


                        Sittable.Add(location1);


                    }

                }
            }
        }
        Debug.Log(Sittable.Count);

        //Test
        bool testFlag = true;

        //If Sittable == 0
        if (Sittable.Count == 0) //&&  
        {
            FloorPlace();
        
        }
        else
        {
            Sittable = Sittable.OrderBy(p => Vector3.Distance(Vector3.zero, p.transform.position)).ToList();

            /*Sittable[1].transform.Rotate(-90.0f, 0.0f, 0.0f);
            Instantiate(Character, new Vector3(Sittable[1].transform.position.x, Sittable[1].transform.position.y - 0.5f, Sittable[1].transform.position.z), Sittable[1].transform.rotation);*/

            Sittable[0].transform.Rotate(-90.0f, 0.0f, 0.0f);
            Instantiate(Character, new Vector3(Sittable[0].transform.position.x, Sittable[0].transform.position.y - 0.5f, Sittable[0].transform.position.z), Sittable[0].transform.rotation);

        }


        Debug.Log("Sittable search finished!");




    }

    private void CalculateFloorBounds()
    {
        if (floor != null)
        {
            Renderer floorRenderer = floor.GetComponent<Renderer>();
            if (floorRenderer != null)
            {
                floorMinBounds = floorRenderer.bounds.min;
                floorMaxBounds = floorRenderer.bounds.max;
            }
        }
    }

    private bool IsWithinBounds(Vector3 position)
    {
        if (position.x < floorMinBounds.x + boundaryPadding || position.x > floorMaxBounds.x - boundaryPadding ||
            position.z < floorMinBounds.z + boundaryPadding || position.z > floorMaxBounds.z - boundaryPadding)
        {
            return false;
        }

        return true;
    }

    public List<GameObject> PlatformSearch(List<GameObject> Platforms)
    {

        foreach (var sceneObject in myScene.SceneObjects)
        {

            // Find a wall
            if (sceneObject.Kind == SceneObjectKind.Platform)
            {

                // Get the quad
                var marker = new GameObject();

                marker.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                marker.transform.SetParent(parentObject.transform);

                marker.transform.localPosition = sceneObject.Position.ToUnity();
                //marker.transform.localPosition = new Vector3(marker.transform.localPosition.x, marker.transform.localPosition.y, marker.transform.localPosition.z + 0.05f);
                marker.transform.localRotation = sceneObject.Orientation.ToUnity();


                var quads = sceneObject.Quads;

                if (quads.Count > 0)
                {

                    SceneQuad largestQuad = null;
                    float fLargestArea = float.NegativeInfinity;
                    foreach (SceneQuad quad in quads)
                    {
                        System.Numerics.Vector2 extents = quad.Extents;
                        float area = extents.X * extents.Y;

                        if (area > fLargestArea)
                        {
                            fLargestArea = area;
                            largestQuad = quad;
                        }
                    }
                    // Find a good location for a 1mx1m object  

                    System.Numerics.Vector2 location;


                    if (largestQuad.FindCentermostPlacement(new System.Numerics.Vector2(0.1f, 0.1f), out location))
                    {
                        //GameObject location1 = GameObject.Instantiate(placePrefab);
                        GameObject location1 = new GameObject();
                        location1.transform.parent = marker.transform;
                        location1.transform.localPosition = Vector3.zero;
                        location1.transform.localRotation = Quaternion.identity;

                        //Our pivot gameobject starts in the middle of the platform, that's the local origin in Unity, but we need to move it to the top left 
                        //corner of the object. 'FindCenterMostPlacement' coordinates work in 'Quad Space'
                        //they start at the top left of the Quad, and go down and right correspondingly in X and Y axis

                        //Move to the starting position aka. Top Left Corner
                        location1.transform.Translate(-(largestQuad.Extents.X / 2.0f), largestQuad.Extents.Y / 2.0f, -0.05f, Space.Self);

                        //Move to the CenterMost Place -- Y is negate so that we move right and down, according to the API 'FindCenterMostPlacement'
                        //https://docs.microsoft.com/en-us/windows/mixed-reality/develop/platform-capabilities-and-apis/scene-understanding-SDK#quad
                        location1.transform.Translate(location.X, -location.Y, 0.0f, Space.Self);
                        location1.transform.Rotate(0.0f, 0.0f, 0.0f);

                        Platforms.Add(location1);


                    }

                }
            }
        }

        Platforms = Platforms.OrderBy(p => Vector3.Distance(Vector3.zero, p.transform.position)).ToList();

        return Platforms;
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

        Floors = Floors.OrderBy(p => Vector3.Distance(Vector3.zero, p.transform.position)).ToList();

        

        return Floors;
    }

    public Vector3 GoodPosition(GameObject Object, Vector3 Position, Bounds mergedBounds)
    {
        List<Vector3> BadPoint = new List<Vector3>();

        BoxCollider Collider = Object.GetComponent<BoxCollider>();
        float addedHeight = 1.0f; // 想增加的高度

        Vector3 colliderSize = Collider.bounds.size + new Vector3(0, addedHeight, 0);

        // 检测是否与其他物体发生重叠

        float gridSize = 0.2f; // 网格单元的大小
        float halfGridSize = gridSize / 2f; // 网格单元大小的一半

        // 计算中心点坐标
        Vector3 centerPoint = new Vector3((mergedBounds.max.x + mergedBounds.min.x) / 2, Position.y, (mergedBounds.max.z + mergedBounds.min.z) / 2);

        // 生成所有可能的坐标点
        List<Vector3> allPoints = new List<Vector3>();
        for (float x = mergedBounds.min.x + halfGridSize; x < mergedBounds.max.x; x += gridSize)
        {
            for (float z = mergedBounds.min.z + halfGridSize; z < mergedBounds.max.z; z += gridSize)
            {
                // 计算代表性点的位置
                Vector3 representativePoint = new Vector3(x, Position.y, z);

                // 检查代表性点是否在地图内
                if (mergedBounds.Contains(representativePoint))
                {
                    allPoints.Add(representativePoint);
                }
            }
        }

        // 按照与中心点的距离进行排序
        allPoints = allPoints.OrderBy(point => Vector3.Distance(point, centerPoint)).ToList();

        // 遍历所有的代表性点
        foreach (var representativePoint in allPoints)
        {
            Collider[] colliders = Physics.OverlapBox(representativePoint, colliderSize);

            // 检查代表性点是否与其他物体发生碰撞
            if (colliders.Length > 1)
            {
                continue;
            }
            else if (colliders.Length == 1)
            {
                int only_floor = 1;
                foreach (Collider collider in colliders)
                {
                    if (collider.gameObject.name != "FloorMesh")
                    {
                        only_floor = 0;
                    }
                }
                if (only_floor == 0)
                {
                    continue;
                }
            }

            //是否在不规则形状上面
            GameObject UnderObject = GetObjectBelow(representativePoint + new Vector3(0.0f, 1.0f, 0.0f));
            if (UnderObject == null || UnderObject.name != "FloorMesh")
            {
                continue;
            }

            //是否已经出现过
            if (BadPoint.Contains(representativePoint))
            {
                continue;
            }

            BadPoint.Add(representativePoint);

            Collider[] colliders1 = Physics.OverlapBox(representativePoint, colliderSize / 2);
            foreach (Collider collider in colliders)
            {
                Debug.Log("与" + collider.gameObject.name + "重叠！");
            }

            // 返回找到的代表性点
            return representativePoint;
        }

        BadPoint.Add(Position);

        return Position;
    }

    public List<Vector3> GoodPositions(GameObject Object, Vector3 Position, Bounds mergedBounds)
    {
        List<Vector3> BadPoint = new List<Vector3>();
        List<Vector3> result = new List<Vector3>();
        BoxCollider Collider = Object.GetComponent<BoxCollider>();
        float addedHeight = 0.5f; // 想增加的高度

        Vector3 colliderSize = Collider.bounds.size + new Vector3(0, addedHeight, 0);
        Debug.Log("collider: " + colliderSize + Object.name);

        // 检测是否与其他物体发生重叠

        float gridSize = 0.2f; // 网格单元的大小
        float halfGridSize = gridSize / 2f; // 网格单元大小的一半

        // 计算中心点坐标
        Vector3 centerPoint = new Vector3((mergedBounds.max.x + mergedBounds.min.x) / 2, Position.y, (mergedBounds.max.z + mergedBounds.min.z) / 2);

        // 生成所有可能的坐标点
        List<Vector3> allPoints = new List<Vector3>();
        for (float x = mergedBounds.min.x + halfGridSize; x < mergedBounds.max.x; x += gridSize)
        {
            for (float z = mergedBounds.min.z + halfGridSize; z < mergedBounds.max.z; z += gridSize)
            {
                // 计算代表性点的位置
                Vector3 representativePoint = new Vector3(x, Position.y, z);

                // 检查代表性点是否在地图内
                if (mergedBounds.Contains(representativePoint))
                {
                    allPoints.Add(representativePoint);
                }
            }
        }

        // 按照与中心点的距离进行排序
        allPoints = allPoints.OrderBy(point => Vector3.Distance(point, centerPoint)).ToList();

        // 遍历所有的代表性点
        foreach (var representativePoint in allPoints)
        {
            Collider[] colliders = Physics.OverlapBox(representativePoint, colliderSize);

            // 检查代表性点是否与其他物体发生碰撞
            if (colliders.Length > 0)
            {
                int ignoredCollidersCount = 0;
                foreach (Collider collider in colliders)
                {
                    if (collider.gameObject.name == "FloorMesh" || collider.gameObject.name.StartsWith("SpatialMesh"))
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
            GameObject UnderObject = GetObjectBelow(representativePoint + new Vector3(0.0f, 1.0f, 0.0f));
            if (UnderObject == null || UnderObject.name != "FloorMesh")
            {
                continue;
            }

            
            result.Add(representativePoint);
        }
        
        return result;
    }
    public Vector3 PlatformPosition(GameObject Object, Vector3 Position, Bounds bound)
    {
        List<Vector3> BadPoint = new List<Vector3>();
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

                    if (colliders.Length > 1)
                    {
                        //Debug.Log("与其他物体重叠！");

                        continue;
                    }
                    else if (colliders.Length == 1)
                    {
                        int only_floor = 1;
                        foreach (Collider collider in colliders)
                        {
                            if (collider.gameObject.name != "PlatformMesh")
                            {
                                only_floor = 0;
                            }

                        }
                        if (only_floor == 0)
                        {
                            //Debug.Log("与其他物体重叠！");

                            continue;
                        }

                    }

                    //是否在不规则形状上面
                    GameObject UnderObject = GetObjectBelow(representativePoint + new Vector3(0.0f, 0.1f, 0.0f));
                    if (UnderObject == null || UnderObject.name != "PlatformMesh")
                    {
                        //Debug.Log("在不规则形状上：" + UnderObject);


                        continue;

                    }


                    //是否已经出现过
                    if (BadPoint.Contains(representativePoint))
                    {
                        continue;
                    }

                    BadPoint.Add(representativePoint);

                    Collider[] colliders1 = Physics.OverlapBox(representativePoint, colliderSize / 2);
                    foreach (Collider collider in colliders)
                    {
                        //Debug.Log("与" + collider.gameObject.name + "重叠！");


                    }

                    return representativePoint;
                }
            }
        }


        // Search from edge
        /*for (float x = bound.min.x + halfGridSize; x < bound.max.x; x += gridSize)
        {
            for (float z = bound.min.z + halfGridSize; z < bound.max.z; z += gridSize)
            {
                // 计算代表性点的位置
                Vector3 representativePoint = new Vector3(x, Position.y, z);
                Collider[] colliders = Physics.OverlapBox(representativePoint, colliderSize);

                // 检查代表性点是否在地图内
                if (!bound.Contains(representativePoint))
                {
                    Debug.Log("与其他物体重叠！1");

                    continue;
                }

                // 检查代表性点是否与其他物体发生碰撞


                if (colliders.Length > 1)
                {

                    continue;
                }
                else if (colliders.Length == 1)
                {
                    int only_floor = 1;
                    foreach (Collider collider in colliders)
                    {
                        if (collider.gameObject.name != "PlatformMesh")
                        {
                            only_floor = 0;
                        }

                    }
                    if (only_floor == 0)
                    {
                        Debug.Log("与其他物体重叠！2");

                        continue;
                    }

                }

                //是否在不规则形状上面
                GameObject UnderObject = GetObjectBelow(representativePoint + new Vector3(0.0f, 0.1f, 0.0f));
                if (UnderObject == null || UnderObject.name != "PlatformMesh")
                {
                    Debug.Log("与其他物体重叠！3");


                    continue;

                }


                //是否已经出现过
                if (BadPoint.Contains(representativePoint))
                {
                    continue;
                }

                BadPoint.Add(representativePoint);

                Collider[] colliders1 = Physics.OverlapBox(representativePoint, colliderSize / 2);
                foreach (Collider collider in colliders)
                {
                    //Debug.Log("与" + collider.gameObject.name + "重叠！");


                }

                return representativePoint;
            }
        }*/

        BadPoint.Add(Position);

        return Position;


    }

    public void PlatformPlace1()
    {
        //TODO:一张桌子上出现多个物体，分配位置
        int Need_Platform = 6; //6张桌子, 10个物体, 第2个放3个物体，第5张桌子放2个，第6张2个
        Dictionary<int, int> objectCounts = new Dictionary<int, int>();
        objectCounts.Add(1, 1); // 第一张桌子放置1个物体
        objectCounts.Add(2, 3);
        objectCounts.Add(3, 1);
        objectCounts.Add(4, 1);
        objectCounts.Add(5, 2);
        objectCounts.Add(6, 2);


        Platforms = PlatformSearch(Platforms);

        Floors = FloorSearch(Floors);
        Bounds platformBounds = new Bounds();

        Debug.Log("平台的数量是：" + Platforms.Count);

        int currentIndex = 0;

        if (Platforms.Count >= Need_Platform)//Assign platform
        {
            Debug.Log("检测到的平台的数量大于需要的平台数量：");

            //TODO: Create list of gameobject, loop to assign
            for (int j = 0; j < Platforms.Count; j++)
            {
                // 获取当前桌子应该放置的物体数量
                int objectCount = 0;
                objectCounts.TryGetValue(j + 1, out objectCount);

                float raycastDistance = 1f; // 射线的长度

                // 发射一条射线从物体下方向下
                RaycastHit hit;
                if (Physics.Raycast(new Vector3(Platforms[j].transform.position.x, Platforms[j].transform.position.y + 0.1f, Platforms[j].transform.position.z), Vector3.down, out hit, raycastDistance))
                {
                    // 检测到物体，返回被击中的物体
                    platformBounds = hit.transform.gameObject.GetComponent<MeshRenderer>().bounds;
                }


                // 循环生成需要放置的物体数量
                for (int k = 0; k < objectCount; k++)
                {
                    Vector3 position = PlatformPosition(PlatformObjects[currentIndex], Platforms[currentIndex].transform.position, platformBounds);

                    // 在位置上实例化物体
                    Instantiate(PlatformObjects[currentIndex], position + new Vector3(0f, 0.02f, 0f), PlatformObjects[currentIndex].transform.rotation);
                    currentIndex++;
                }
            }

        }
        //Exist i platform, but n platforms are needed, creat n-i virtual platform
        else if (Platforms.Count > 0)//Fisrt assign, then Create platform on floor
        {
            Debug.Log("检测到的平台的数量小于需要的平台数量， 且平台存在！");


            for (int i = 0; i < Platforms.Count; i++)
            {
                Instantiate(PlatformObjects[i], new Vector3(Platforms[i].transform.position.x, Platforms[i].transform.position.y + 0.02f, Platforms[i].transform.position.z), PlatformObjects[i].transform.rotation);
                Debug.Log("检测到平台放置");
            }



            for (int n = Platforms.Count; n < Need_Platform; n++)
            {

                Vector3 goodposition = GoodPosition(Table, Floors[0].transform.position, mergedBounds);

                Instantiate(Table, goodposition, Table.transform.rotation);

                GameObject TableObject = Instantiate(PlatformObjects[n]);

                TableObject.transform.position = goodposition + new Vector3(0.0f, 0.7f, 0.0f);


                Debug.Log("虚拟平台放置");

            }

            // 检测是否与其他物体发生重叠


        }
        else //Create
        {
            Debug.Log("场景中不存在平台！");

            for (int i = 0; i < Need_Platform; i++)
            {
                Floors = FloorSearch(Floors);

                Vector3 goodposition = GoodPosition(PlatformObjects[i], Floors[0].transform.position, mergedBounds);

                Instantiate(PlatformObjects[i], goodposition, PlatformObjects[i].transform.rotation);

            }

        }

    }

    public void FloorPlace()
    {
        Floors = FloorSearch(Floors); //MyScene is updating, ensure everytime we get the newest scene

        if(Floors == null)
        {
            Debug.Log("Floor detection failed");
        }
        else
        {
            
            Vector3 goodposition = GoodPosition(ManOnChair, Floors[0].transform.position, mergedBounds);

            Debug.Log("地板检测数量为" + goodposition);

            Instantiate(ManOnChair, goodposition, ManOnChair.transform.rotation);

        }
        
        

    }

    private Vector2Int[] GetGridOffsets()
    {
        return new Vector2Int[]
        {
            new Vector2Int(-1, -1), new Vector2Int(0, -1), new Vector2Int(1, -1),
            new Vector2Int(-1, 0), new Vector2Int(0, 0), new Vector2Int(1, 0),
            new Vector2Int(-1, 1), new Vector2Int(0, 1), new Vector2Int(1, 1)
        };
    }

    public void WallPlace()
    {
        List<GameObject> Walls = new List<GameObject>();


        foreach (var sceneObject in myScene.SceneObjects)
        {

            // Find a wall
            if (sceneObject.Kind == SceneObjectKind.Wall)
            {

                // Get the quad
                //var marker = GameObject.Instantiate(markerPrefab);
                var marker = new GameObject();

                //marker.transform.position = new Vector3(1, 0, 0);
                //marker.transform.rotation = Quaternion.identity;
                //marker.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                marker.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                marker.transform.SetParent(parentObject.transform);

                marker.transform.localPosition = sceneObject.Position.ToUnity();
                //marker.transform.localPosition = new Vector3(marker.transform.localPosition.x, marker.transform.localPosition.y, marker.transform.localPosition.z + 0.05f);
                marker.transform.localRotation = sceneObject.Orientation.ToUnity();


                var quads = sceneObject.Quads;
                if (quads.Count > 0)
                {

                    SceneQuad largestQuad = null;
                    float fLargestArea = float.NegativeInfinity;
                    foreach (SceneQuad quad in quads)
                    {
                        System.Numerics.Vector2 extents = quad.Extents;
                        float area = extents.X * extents.Y;

                        if (area > fLargestArea)
                        {
                            fLargestArea = area;
                            largestQuad = quad;
                        }
                    }
                    // Find a good location for a 1mx1m object  

                    System.Numerics.Vector2 location;
                    if (largestQuad.FindCentermostPlacement(new System.Numerics.Vector2(0.3f, 0.3f), out location))
                    {
                        //GameObject location1 = GameObject.Instantiate(Window);
                        GameObject location1 = new GameObject();
                        location1.transform.parent = marker.transform;
                        location1.transform.localPosition = Vector3.zero;
                        location1.transform.localRotation = Quaternion.identity;

                        //Our pivot gameobject starts in the middle of the platform, that's the local origin in Unity, but we need to move it to the top left 
                        //corner of the object. 'FindCenterMostPlacement' coordinates work in 'Quad Space'
                        //they start at the top left of the Quad, and go down and right correspondingly in X and Y axis

                        //Move to the starting position aka. Top Left Corner
                        location1.transform.Translate(-(largestQuad.Extents.X / 2.0f), largestQuad.Extents.Y / 2.0f, -0.05f, Space.Self);

                        //Move to the CenterMost Place -- Y is negate so that we move right and down, according to the API 'FindCenterMostPlacement'
                        //https://docs.microsoft.com/en-us/windows/mixed-reality/develop/platform-capabilities-and-apis/scene-understanding-SDK#quad
                        location1.transform.Translate(location.X, -location.Y, 0.0f, Space.Self);
                        location1.transform.Rotate(0.0f, 0.0f, 0.0f);

                        Walls.Add(location1);
                       


                        Debug.Log("Wall Placement finished!");
                    }


                }
            }
        }
        Walls = Walls.OrderBy(p => Vector3.Distance(Vector3.zero, p.transform.position)).ToList();

        Instantiate(Window, Walls[0].transform.position, Walls[0].transform.rotation);
        //Platforms[0].transform.Rotate(180.0f, 0.0f, 0.0f);

        Instantiate(Lamp, Walls[1].transform.position, Walls[1].transform.rotation);

        
    }

    
    public void MaterialPlace()
    {
        foreach (var sceneObject in myScene.SceneObjects)
        {

            // Find a platform
            if (sceneObject.Kind == SceneObjectKind.Platform)
            {

                // Get the quad
                //var marker = GameObject.Instantiate(markerPrefab);
                var marker = new GameObject();
                marker.transform.SetParent(parentObject.transform);

                marker.transform.localPosition = sceneObject.Position.ToUnity();
                //marker.transform.localPosition = new Vector3(marker.transform.localPosition.x, marker.transform.localPosition.y, marker.transform.localPosition.z + 0.05f);
                marker.transform.localRotation = sceneObject.Orientation.ToUnity();
                

                foreach (var sceneQuad in sceneObject.Quads)
                {
                    var quad = GameObject.CreatePrimitive(PrimitiveType.Cube);

                    quad.transform.SetParent(marker.transform, false);
                    quad.transform.localPosition = new Vector3(0.0f, 0.0f, -0.03f);

                    quad.transform.localScale = new Vector3(
                        sceneQuad.Extents.X, sceneQuad.Extents.Y, 0.005f);

                    quad.GetComponent<Renderer>().material = PlatformMaterial;
                    
                }


            }

            //Find a wall
            if (sceneObject.Kind == SceneObjectKind.Wall)
            {

                // Get the quad
                //var marker = GameObject.Instantiate(markerPrefab);
                var marker = new GameObject();
                marker.transform.SetParent(parentObject.transform);

                marker.transform.localPosition = sceneObject.Position.ToUnity();
                //marker.transform.localPosition = new Vector3(marker.transform.localPosition.x, marker.transform.localPosition.y, marker.transform.localPosition.z + 0.05f);
                marker.transform.localRotation = sceneObject.Orientation.ToUnity();

                foreach (var sceneQuad in sceneObject.Quads)
                {
                    var quad = GameObject.CreatePrimitive(PrimitiveType.Cube);

                    quad.transform.SetParent(marker.transform, false);

                    quad.transform.localScale = new Vector3(
                        sceneQuad.Extents.X, sceneQuad.Extents.Y, 0.005f);

                    quad.GetComponent<Renderer>().material = WallMaterial;
                }


            }

            //Find a floor
            if (sceneObject.Kind == SceneObjectKind.Floor)
            {

                // Get the quad
                //var marker = GameObject.Instantiate(markerPrefab);
                var marker = new GameObject();
                marker.transform.SetParent(parentObject.transform);

                marker.transform.localPosition = sceneObject.Position.ToUnity();
                //marker.transform.localPosition = new Vector3(marker.transform.localPosition.x, marker.transform.localPosition.y, marker.transform.localPosition.z + 0.05f);
                marker.transform.localRotation = sceneObject.Orientation.ToUnity();
                

                foreach (var sceneQuad in sceneObject.Quads)
                {
                    var quad = GameObject.CreatePrimitive(PrimitiveType.Cube);

                    quad.transform.SetParent(marker.transform, false);

                    quad.transform.localScale = new Vector3(
                        sceneQuad.Extents.X, sceneQuad.Extents.Y, 0.005f);

                    quad.GetComponent<Renderer>().material = FloorMaterial;
                }


            }


        }
    }

    public void DoPlace()
    {

        if (Label == "Finish")//Continue add materials and objects
        {
            FindSittable();
            PlatformPlace();
            //PlatformPlace1();
            //WallPlace();
            //ManPlace();
            //MaterialPlace();
            Label = null;
        }
    }
   

    public void ClickButton1()
    {
        Debug.Log("Button1");
        Label = "Chair";

    }

    public void ClickButton2()
    {
        Debug.Log("Button2");
        Label = "Sofa";

    }

    public void ClickButton3()
    {
        Debug.Log("Button3");
        Label = "Finish";

    }
}
