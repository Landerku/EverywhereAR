#if (UNITY_EDITOR) 

using GraphProcessor;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using Microsoft.MixedReality.SceneUnderstanding;
using System.Linq;
using System.IO;
using System;

[CustomEditor(typeof(GameSceneGraph))]
public class GameSceneEditor : Editor
{

    public static GameSceneGraph graph;
    public List<SceneObject_Unity> GameSceneObjects = new List<SceneObject_Unity>(); //Store sceneObjects output from MyScene
    public List<Relation> GameRelations = new List<Relation>(); //Store relations output from MyScene
    public Dictionary<ObjectNode, SceneObject_Unity> object_unity_Dict = new Dictionary<ObjectNode, SceneObject_Unity>();
    public Dictionary<SceneObject_Unity, ObjectNode> unity_object_Dict = new Dictionary<SceneObject_Unity, ObjectNode>();
    public Dictionary<string, SceneObject_Unity> Node_Dict = new Dictionary<string, SceneObject_Unity>();

    public float MatchingAngle = 0f;
    public float HMatchingRange = 0f;
    public float VMatchingRange = 0f;
    public int SelectedStrategy = 0;
    public float DecisionRange = 0f;
    public float CoverageThreshold  = 0f;
    private string[] arrangementStrategies = new string[] { "Default", "Do not place", "Choose similar place" };
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        MatchingAngle = EditorGUILayout.Slider("Matching Angle", MatchingAngle, 45, 135);  // 参数分别为标签、当前值、最小值和最大值
        HMatchingRange = EditorGUILayout.Slider("Horizontal Matching Range", HMatchingRange, 0, 5);
        VMatchingRange = EditorGUILayout.Slider("Vertical Matching Range", VMatchingRange, 0, 5);

        SelectedStrategy = EditorGUILayout.Popup("Arrangement Strategy", SelectedStrategy, arrangementStrategies);

        DecisionRange = EditorGUILayout.Slider("Decision Range", DecisionRange, 0, 0.5f);
        CoverageThreshold = EditorGUILayout.Slider("Coverage Threshold", CoverageThreshold, 0, 1);

        graph = target as GameSceneGraph;
        if(GUILayout.Button("Output"))
        {
            GameSceneObjects.Clear();
            GameRelations.Clear();
            object_unity_Dict.Clear();
            unity_object_Dict.Clear();
            Node_Dict.Clear();

            Transform myTransform = new GameObject().transform;
            myTransform.position = Vector3.zero;
            myTransform.rotation = Quaternion.identity;

            Dictionary<string, int> categoryIDs = new Dictionary<string, int>();

            List<ObjectNode> objectNodes = graph.GetNodes();
            foreach (ObjectNode objectNode in objectNodes)
            {
                Debug.Log(objectNode.Floating);
                if (!categoryIDs.ContainsKey(objectNode.Category))
                {
                    categoryIDs.Add(objectNode.Category, 0);
                }
                // Create SceneObject_Unity 
                SceneObject_Unity sceneObject = new SceneObject_Unity(
                    objectNode.GUID,
                    objectNode.Category,
                    objectNode.Size,
                    myTransform,
                    objectNode.Horizontal,
                    false
                );
                sceneObject.ID = categoryIDs[objectNode.Category];
                sceneObject.Height = objectNode.Height;
                // Increment the ID for this category
                categoryIDs[objectNode.Category]++;
                // Add to list
                GameSceneObjects.Add(sceneObject);
            }




            foreach (var node in objectNodes)
            {
                foreach (SceneObject_Unity object_unity in GameSceneObjects)
                {
                    if (node.GUID == object_unity.GUID)
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
                    if (node.GUID == object_unity.GUID)
                    {
                        Node_Dict.Add(node.GUID, object_unity);
                    }
                }

            }


            Relation relation;
            GraphGeneration graphGeneration = new GraphGeneration();
            List<SceneObject_Unity> nodesWithoutEdges = new List<SceneObject_Unity>();

            foreach (var node in objectNodes)
            {
                bool hasConnectedEdges = false;
                foreach (var outputPort in node.outputPorts)
                {
                    if (outputPort.GetEdges().Any())
                    {
                        var connectedNode_id = outputPort.GetEdges().FirstOrDefault().inputPort.owner.GUID;

                        // 通过GUID对应起开始节点和结束节点
                        SceneObject_Unity startNode = object_unity_Dict[node];
                        SceneObject_Unity endNode = Node_Dict[connectedNode_id]; // 自定义方法，根据GUID获取对应的SceneObject_Unity

                        switch (outputPort.fieldName)
                        {
                            case "Above":
                                relation = new Relation(startNode, endNode, RelationType.Above);
                                break;

                            case "InFrontOf":
                                relation = new Relation(startNode, endNode, RelationType.InFrontOf);

                                break;

                            case "Behind":
                                relation = new Relation(startNode, endNode, RelationType.Behind);

                                break;

                            case "Right":
                                relation = new Relation(startNode, endNode, RelationType.Right);

                                break;

                            case "Left":
                                relation = new Relation(startNode, endNode, RelationType.Left);

                                break;

                            case "Below":
                                relation = new Relation(startNode, endNode, RelationType.Below);
                                break;

                            case "RightLeft":
                                relation = new Relation(startNode, endNode, RelationType.RightLeft);
                                break;

                            case "FrontBack":
                                relation = new Relation(startNode, endNode, RelationType.FrontBehind);
                                break;

                            case "Around":
                                relation = new Relation(startNode, endNode, RelationType.Around);
                                break;

                            default:
                                relation = new Relation(startNode, endNode, RelationType.None);
                                break;
                        }

                        GameRelations.Add(relation);
                        hasConnectedEdges = true;
                        continue;
                    }


                }





                foreach (var inputPort in node.inputPorts)
                {
                    if (inputPort.GetEdges().Any())
                    {
                        hasConnectedEdges = true;
                        continue;
                    }

                    if (!hasConnectedEdges)
                    {
                        SceneObject_Unity NodeNoEdges = object_unity_Dict[node];
                        nodesWithoutEdges.Add(NodeNoEdges);
                    }

                }

            }


            Debug.Log("Relation number: " + GameRelations.Count);


            /*foreach (Relation relation1 in GameRelations)
            {
                if (relation1.Relationship != RelationType.None)
                {
                    Debug.Log("ObjectA: " + relation1.StartNode.Category + " " + relation1.StartNode.ID + ", Relationship: " + relation1.Relationship + ", ObjectB: " + relation1.EndNode.Category + " " + relation1.EndNode.ID);

                }
            }*/
            string path = System.IO.Path.Combine(Application.dataPath, "Resources/Output1.txt");

            SaveSceneObjectsToFile(path, objectNodes);
            path = System.IO.Path.Combine(Application.dataPath, "Resources/OutputRelations.txt");
            SaveRelationsToFile(path, GameRelations);
            string ParaPath = System.IO.Path.Combine(Application.dataPath, "Resources/GlobalParas.txt");
            SaveParaToFile(ParaPath);
        }
    }

    public void  SaveParaToFile(string filePath)
    {
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            writer.WriteLine("MachingAngle: " + MatchingAngle);
            writer.WriteLine("HorizontalMatchingRange: " + HMatchingRange);
            writer.WriteLine("VerticalMatchingRange: " + VMatchingRange);
            writer.WriteLine("SelectedStrategy: " + arrangementStrategies[SelectedStrategy]); // 显示选中的策略名称
            writer.WriteLine("DecisionRange: " + DecisionRange);
            writer.WriteLine("CoverageThreshold: " + CoverageThreshold);
        }

    }

    public void SaveSceneObjectsToFile(string filePath, List<ObjectNode> objects)
    {
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            foreach (var obj in objects)
            {
                writer.WriteLine("Data");
                writer.WriteLine("ID = " + obj.id);
                writer.WriteLine("GID = " + obj.GUID);
                writer.WriteLine("Name = " + obj.Name);
                writer.WriteLine("Category = " + obj.Category);
                writer.WriteLine("Size = " + obj.Size);
                writer.WriteLine("Height = " + obj.Height);
                writer.WriteLine("Horizontal = " + obj.Horizontal);
                writer.WriteLine("Float = " + obj.Floating);


                if (obj.Prefab == null)
                {
                    writer.WriteLine("Prefab = N/A");
                }
                else
                {
                    // Assuming the prefab is located under a "Resources" directory
                    string prefabPath = AssetDatabase.GetAssetPath(obj.Prefab);

                    if (prefabPath.Contains("/Resources/"))
                    {
                        string relativePath = prefabPath.Substring(prefabPath.IndexOf("/Resources/") + 11);
                        relativePath = relativePath.Substring(0, relativePath.Length - 7); // Remove the '.prefab' extension
                        writer.WriteLine("Prefab = " + relativePath);
                    }
                    else
                    {
                        Debug.LogError("Prefab " + obj.Prefab.name + " is not under a Resources directory!");
                    }
                }
            }
        }
    }

    public void SaveRelationsToFile(string filePath, List<Relation> relations)
    {
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            foreach (var relation in relations)
            {
                writer.WriteLine("GUID = " + relation.StartNode.GUID);
           

                writer.WriteLine("Relation = " + relation.Relationship.ToString());

                writer.WriteLine("GUID = " + relation.EndNode.GUID);
                writer.WriteLine();

            }
        }
    }
}
#endif

