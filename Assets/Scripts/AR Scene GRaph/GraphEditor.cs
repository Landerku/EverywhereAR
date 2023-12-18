#if (UNITY_EDITOR)
using GraphProcessor;
using UnityEngine;
using UnityEditor;
 

[CustomEditor(typeof(GameSceneGraph))]
public class ExampleGraphEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Process"))
        {
            /*var graph = target as ARSceneGraph;
            var processor = new ExampleGraphProcessor(graph);
            processor.Run();
            Debug.Log(processor.Result);*/
        }
    }
}
#endif
