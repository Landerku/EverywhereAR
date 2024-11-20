#if (UNITY_EDITOR) 
using System;
using GraphProcessor;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using System.Reflection;
using UnityEngine.UI;
using System.Linq;
using UnityEditor;

[Serializable]
[NodeMenuItem("Custom/Object")]

public class ObjectNode : BaseNode
{
    public int id;
    public string Name;
    public string Category;
    public float Size;
    public float Height;
    public bool Horizontal;
    public bool Floating;
    public GameObject Prefab;

    
    [Output(name = "OutputRelationship", allowMultiple = true)]
    public int output;

    /*[Output(name = "In front of", allowMultiple = true)]
    public int InFrontOf;

    [Output(name = "Behind", allowMultiple = true)]
    public int Behind;

    [Output(name = "Right", allowMultiple = true)]
    public int Right;

    [Output(name = "Left", allowMultiple = true)]*/
    public int Left;

    [Output(name = "Below", allowMultiple = true)]
    public int Below;

    [Output(name = "Above", allowMultiple = true)]
    public int Above;

    [Output(name = "Front or Back", allowMultiple = true)]
    public int FrontBack;

    [Output(name = "Right or Left", allowMultiple = true)]
    public int RightLeft;

    [Output(name = "Around", allowMultiple = true)]
    public int Around;

    [Input(name = "InputRelationship", allowMultiple = true)]

    public int input;
    public override string name => "Object";

 

}
#endif