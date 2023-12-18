using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SceneObject_Unity //Node with attributes
{
    public int ID;
    public string GUID;
    public float Size;
    public float Height;
    public string Category;
    public bool Horizontal;
    public bool Real;
    public Transform Position;
    public Bounds Bounds;
    public bool Float;
    // 添加其他属性
    public SceneObject_Unity(SceneObject_Unity source)
    {
        this.ID = source.ID;
        this.GUID = source.GUID;
        this.Size = source.Size;
        this.Height = source.Height;
        this.Category = source.Category;
        this.Horizontal = source.Horizontal;
        this.Real = source.Real;
        this.Position = source.Position;
        this.Bounds = source.Bounds;
        this.Float = source.Float;
    }
    public SceneObject_Unity(int id, string name, float size, Transform position, bool horizontal, bool real)
    {
        ID = id;
        Category = name;
        Size = size;
        Height = position.position.y;
        Position = position;
        Horizontal = horizontal;
        Real = real;
    }

    public SceneObject_Unity(string ID, string name, float size, Transform position, bool horizontal, bool real)
    {
        GUID = ID;
        Category = name;
        Size = size;
        Height = position.position.y;
        Position = position;
        Horizontal = horizontal;
        Real = real;
    }


}

[Serializable]
public class Relation
{
    public SceneObject_Unity StartNode;
    public SceneObject_Unity EndNode;
    public RelationType Relationship; // 添加关系属性

    public Relation(SceneObject_Unity startNode, SceneObject_Unity endNode, RelationType relationship)
    {
        StartNode = startNode;
        EndNode = endNode;
        Relationship = relationship;
    }
}

public enum RelationType
{
    Above,
    Below,
    NextTo,
    InFrontOf,
    Right,
    Left,
    Behind,
    RightLeft,
    FrontBehind,
    Around,
    None
    // Add more relation types as needed
}