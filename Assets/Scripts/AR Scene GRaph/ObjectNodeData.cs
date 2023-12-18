using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectNodeData
{
    public int ID;
    public string GID;
    public string name;
    public string category;
    public float size;
    public float height;
    public bool horizontal;
    public bool Float;
    public GameObject Prefab;

    public ObjectNodeData()
    {
    }

    public ObjectNodeData(int id, string name, string category, float size, float height, bool horizontal, GameObject prefab)
    {
        this.ID = id;
        this.name = name;
        this.category = category;
        this.size = size;
        this.height = height;
        this.horizontal = horizontal;
        this.Prefab = prefab;
    }
}
