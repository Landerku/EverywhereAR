<h2 align="center"> <a href="https://arxiv.org/abs/2402.13724">EverywhereAR: A Visual Authoring System for Creating Adaptive AR Game Scenes</a></h2>

This repository is the official implementation of [**EverywhereAR**] under review in IEEE-TVCG.

<p align="center" width="100%">
<a target="_blank"><img src="figs/teaser.png" alt="EverywhereAR Overview" style="width: 60%; min-width: 200px; display: block; margin: auto;"></a>
</p>

## Introduction

- **EverywhereAR** is a novel system that is capable of flexibly realizing the designer's idea in various real-world scenes. It provides a designer-friendly Game Scene Template development interface, for designers to quickly graphify their inspirations. This system incorporates two key contributions: 
  - (1) An intuitive graph-based interface for designers to swiftly transform their ideas into deployable game scenes.
  - (2) A novel scene graphs integration algorithm for intelligently realizing game designs in various real scenes.
  - We also conduct 2 user studies, collecting feedback from a wide range of potential users. It would inspire more future works in this direction.


## Setup

### Environment
**Unity Version**
- To ensure compatibility with MRTK2, we recommend using Unity 2020 (this system implementation uses Unity 2020.3.47f1).

**Dependency**
- MRTK2.8 (https://learn.microsoft.com/ja-jp/windows/mixed-reality/mrtk-unity/mrtk2/?view=mrtkunity-2022-05)
- NodeGraphProcessor (https://github.com/alelievr/NodeGraphProcessor)
- Visual Studio 2017 or 2019 with Universal Windows Platform components 
- Windows SDK version 10.0.18362.0 or higher

## Setup 

1. Clone or download this sample repository. 
2. Open Unity Hub, select 'Add' and choose the project folder where you extracted the cloned sample.
3. After the project loads, navigate to **Windows > Package Manager** and check that you have the required packages installed:
    * Mixed Reality Scene Understanding
    * Mixed Reality WinRT Projections
4. If they're missing, download them using the [Mixed Reality Feature Tool](https://docs.microsoft.com/en-us/windows/mixed-reality/develop/unity/welcome-to-mr-feature-tool)

## Edit the Scene Graph

1. First, create a scene graph by right-clicking and selecting "Create" -> "AR Scene Graph."
2. Double-click to open the scene graph. Within the scene graph, right-click and select "Create New Node" -> "Custom" -> "Object" to create a node. You can then connect nodes by dragging the `InputRelationship` port to the `OutputRelationship` port of another object. Note that in the current settings, each node can have multiple incoming connections but only 0 or 1 outgoing connection. Additionally, all nodes in the scene graph must be interconnected, meaning no independent subgraphs are allowed.
3. After editing the scene graph, click the `Output` button at the bottom of the Inspector window for the scene graph to generate it.

## Running the sample

### Running on HoloLens 2

To run this sample on a HoloLens 2:

1. Open this scene
2. Select the **SceneUnderstandingManager** game object and make sure that **Query Scene From Device** is selected on the **SceneUnderstandingManager Component** in all Scenes.
3. Go to **File > Build Settings** and select **Build > UWP**. Once the build completes successfully, a log indicating this will show up in the output console.
4. Navigate to the **UWP** folder under root and open 'AppName.sln' in Visual Studio.
5. Right-click on the 'AppName (Universal Windows)' project and click on 'Publish' --\> 'Create App Packages'.
6. Run through the wizard and wait for building and packaging to complete.
7. [Deploy](https://docs.microsoft.com/en-us/hololens/holographic-custom-apps) the package to a HoloLens 2. Ensure you build your application using **ARM64**, see the topic [Unity 2019.3 and HoloLens](https://microsoft.github.io/MixedRealityToolkit-Unity/Documentation/BuildAndDeploy.html#unity-20193-and-hololens) for further details.
8. Launch 'AppName' from the 'All Apps' list on the HoloLens 2!

### Running on PC

To run this sample on a PC:

1. Open the Exp_ Scene
2. Select the **SceneUnderstandingManager** game object and uncheck the **Query Scene From Device** checkbox on the **SceneUnderstandingManager Component**
3. Ensure SU Serialized Scene Paths on the Scene Understanding component is referring to a serialized Scene Understanding scene, examples scenes are provided under the **MRTK_Scan** folder
4. Click **Play** in the Editor
5. Use WASD in the keyboard to control the pespective, move and aim the white dot at **Finish** button in the menu bar, click!

## Important Contents

| File/folder | Description |
|-------------|-------------|
| `Resources` | 3D assets can be used in the AR Scene Graph's Prefab attribute. Be careful of the path of these models in **GraphGeneration.cs** file.|
| `Scripts` | **GraphGeneration.cs** is the main file for editting the system, **ObjectNodeView.cs** is used for editting the attributes of each node in the graph, **ObjectNodeEditor.cs** is for editting the Inspetor of AR Scene Graph. |
| `MRTK_Scan` | Scanned real scene examples using HoloLens, can be directly used in `SU Serialized Scene Path` of **SceneUnderstandingManager** game object.|
| `Scene_Scan` | Scanned real scene models, can be used for visualization. |


## Acknowledgement
We are grateful for the following awesome projects:
* [NodeGraphProcessor](https://github.com/alelievr/NodeGraphProcessor)
* [MixedReality-SceneUnderstanding-Samples](https://github.com/microsoft/MixedReality-SceneUnderstanding-Samples)
