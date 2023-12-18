
#if (UNITY_EDITOR) 
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using GraphProcessor;
using System.Xml;
using System.Collections.Generic;
using static GraphProcessor.FloatParameter;
using UnityEngine;
using JetBrains.Annotations;
using UnityEditor;
using System.Collections;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using System.Reflection;
using Microsoft.MixedReality.SceneUnderstanding;
using Microsoft.MixedReality.Toolkit.CameraSystem;
using System;

[Serializable]
[NodeCustomEditor(typeof(ObjectNode))]
public class ObjectNodeView : BaseNodeView
{
    private ObjectNode objectNode;

    public override void Enable()
    {
        objectNode = nodeTarget as ObjectNode;

        List<string> options = new List<string>() { "Horizontal", "Vertical"};
        List<string> options1 = new List<string>() { "Yes", "No" };
        List<string> categories = new List<string>() { "Unknown", "Chair", "Table", "Sofa", "Wall", "Floor", "Character", "TV", "Shelf", "Bed" };
        //GameObject model = new GameObject();


        TextField NameField = new TextField ("Name")
        {
            value = objectNode.Name
        };



        PopupField<string> HorizontalField = new PopupField<string>("Horizontal?", options, objectNode.Horizontal ? 0 : 1);
        PopupField<string> CategoryField = new PopupField<string>("Category", categories, categories.IndexOf(objectNode.Category));


        FloatField SizeField = new FloatField("Size") { value = objectNode.Size };
        FloatField HeightField = new FloatField("Height") { value = objectNode.Height };

        ObjectField ObjectField = new ObjectField("Prefab") { value = objectNode.Prefab };
        ObjectField.visible = true;
        ObjectField.allowSceneObjects = false;
        ObjectField.objectType = typeof(GameObject);

        PopupField<string> FloatField = new PopupField<string>("Float?", options1, objectNode.Floating ? 0 : 1);


        VisualElement previewContainer = new VisualElement();
        ObjectField.Add(previewContainer);
        UpdatePreviewImage(previewContainer, objectNode.Prefab);





        HorizontalField.RegisterValueChangedCallback((evt) =>
        {
            if( evt.newValue == "Horizontal")
            {
                objectNode.Horizontal = true;

            }
            else
            {
                objectNode.Horizontal=false;

            }
        });

        FloatField.RegisterValueChangedCallback((evt) =>
        {
            if (evt.newValue == "Yes")
            {
                objectNode.Floating = true;

            }
            else
            {
                objectNode.Floating = false;

            }
        });

        NameField.RegisterValueChangedCallback((v) => {
            Debug.Log("Updated Object Name");
            objectNode.Name = v.newValue;
        });

        CategoryField.RegisterValueChangedCallback((v) => {
            Debug.Log("Updated Object Category");
            objectNode.Category = v.newValue;
        });


        SizeField.RegisterValueChangedCallback((v) => {
            Debug.Log("Updated Object Size");
            objectNode.Size = v.newValue;
        });

        HeightField.RegisterValueChangedCallback((v) => {
            Debug.Log("Updated Object Height");
            objectNode.Height = v.newValue;
        });

        ObjectField.RegisterValueChangedCallback((v) => {
            Debug.Log("Updated Object Prefab");
            objectNode.Prefab = (GameObject)v.newValue;
            UpdatePreviewImage(previewContainer, objectNode.Prefab);
        });

        controlsContainer.Add(NameField);
        controlsContainer.Add(CategoryField);
        controlsContainer.Add(HorizontalField);
        controlsContainer.Add(FloatField);
        controlsContainer.Add(SizeField);
        controlsContainer.Add(HeightField);
        controlsContainer.Add(ObjectField);


    }

    /*public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
    {
        // 调用父类的构建上下文菜单方法
        base.BuildContextualMenu(evt);

        // 添加 "Add Port" 菜单项
        evt.menu.AppendAction("Add Relationship: Right", (action) => OnAddPort(action, "Right"));
        evt.menu.AppendAction("Add Relationship: Left", (action) => OnAddPort(action, "Left"));
        evt.menu.AppendAction("Add Relationship: In front of", (action) => OnAddPort(action, "InFrontOf"));
        evt.menu.AppendAction("Add Relationship: Behind", (action) => OnAddPort(action, "Behind"));
        evt.menu.AppendAction("Add Relationship: Below", (action) => OnAddPort(action, "Below"));
        evt.menu.AppendAction("Add Relationship: Above", (action) => OnAddPort(action, "Above"));
    }*/

    /*private void OnAddPort(DropdownMenuAction action, string Relationship) //要储存该关系的名字
    {
        PortData portData = new PortData();

        switch (Relationship)
        {
            case "Right":
                portData = new PortData
                {
                    displayName = "Right",
                    displayType = typeof(int), // 设置端口类型
                    acceptMultipleEdges = true, 
                    identifier = Relationship,
                };
                break;

            case "Left":
                portData = new PortData
                {
                    displayName = "Left",
                    displayType = typeof(int), // 设置端口类型
                    acceptMultipleEdges = true,
                    identifier = Relationship,
                };
                break;

            case "InFrontOf":
                portData = new PortData
                {
                    displayName = "In Front Of",
                    displayType = typeof(int), // 设置端口类型
                    acceptMultipleEdges = true,
                    identifier = Relationship,
                };
                break;

            case "Behind":
                portData = new PortData
                {
                    displayName = "Behind",
                    displayType = typeof(int), // 设置端口类型
                    acceptMultipleEdges = true,
                    identifier = Relationship,
                };
                break;

            case "Below":
                portData = new PortData
                {
                    displayName = "Below",
                    displayType = typeof(int), // 设置端口类型
                    acceptMultipleEdges = true,
                    identifier = Relationship,
                };
                break;

            case "Above":
                portData = new PortData
                {
                    displayName = "Above",
                    displayType = typeof(int), // 设置端口类型
                    acceptMultipleEdges = true,
                    identifier = Relationship,
                };
                break;

        }
        // 创建新的端口数据
        

        var listener = owner.connectorListener;
        // 获取节点字段信息
        FieldInfo fieldInfo = typeof(ObjectNode).GetField("output");
        // 添加输入端口
        objectNode.AddPort(false, "output", portData); //添加端口，但无视图
        var outputPort = AddPort(fieldInfo, Direction.Output, listener, portData); //添加视图
        //var port = Port.Create<Edge>(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(int));
        //Debug.Log("portName" + outputPort.portData.identifier);
        outputContainer.Add(outputPort);
        objectNode.UpdateAllPorts();

    }*/
    private IEnumerator WaitAndLoadPreview(VisualElement container, GameObject prefab)
    {
        // Request the preview asynchronously
        Texture2D previewTexture = AssetPreview.GetAssetPreview(prefab);

        // Wait until the preview texture is available
        while (previewTexture == null)
        {
            yield return null;
            previewTexture = AssetPreview.GetAssetPreview(prefab);
        }

        // Create and add the preview image
        Image previewImage = new Image();
        previewImage.image = previewTexture;
        container.Add(previewImage);
    }

    private void UpdatePreviewImage(VisualElement container, GameObject prefab)
    {
        container.Clear();

        if (prefab != null)
        {
            // Start the coroutine to wait for the preview texture
            EditorApplication.delayCall += () =>
            {
                IEnumerator coroutine = WaitAndLoadPreview(container, prefab);
                EditorApplication.update += () =>
                {
                    if (!coroutine.MoveNext())
                    {
                        EditorApplication.update -= coroutine.Current as EditorApplication.CallbackFunction;
                    }
                };
            };
        }
    }
}
#endif