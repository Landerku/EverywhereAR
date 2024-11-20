<h2 align="center"> <a href="https://arxiv.org/abs/2402.13724">EverywhereAR: A Visual Authoring System for Creating Adaptive AR Game Scenes</a></h2>

This repository is the official implementation of [**EverywhereAR**] under review in IEEE-TVCG.

<p align="center" width="100%">
<a target="_blank"><img src="figs/teaser.png" alt="BYOC" style="width: 60%; min-width: 200px; display: block; margin: auto;"></a>
</p>

## Introduction

- **EverywhereAR** is a holistic solution for creating facial animations of virtual human in VR applications. This solution incorporates two key contributions: 
  - (1) a deep learning model that transforms human facial images into desired blendshape coefficients and replicate the specified facial expression.
  - (2) a Unity-based toolkit that encapsulates the deep learning model, allowing users to utilize the trained model to create facial animation when developing their own VR applications.
- We also conduct a user study, collecting feedback from a wide range of potential users. It would inspire more future works in this direction.


## Setup

### Environment
**Unity Version**
- 为了兼容MRTK2的版本，我们建议安装的版本为

**Dependency**
- MRTK2.8
- NodeGraphProcessor


## Usage (Unity)
1. 首先创建一个场景图app，通过xx右键点击xx创建。
2. 在场景图中通过邮件“新建节点”－“物体”创建节点，并可以通过拖拽port到另一个port进行连线。需要注意的是，在目前的设置中，每个节点可以有多条连进的
3. Start the `BlendshapeToolkit` with Unity and run the project.
4. Click the `Initialize` button to call `image2bs` project to generate animation. For more details about usage of the toolkit, please refer to our paper.


## Acknowledgement
We are grateful for the following awesome projects:
* [Deep3DFaceRecon_pytorch](https://github.com/sicxu/Deep3DFaceRecon_pytorch)
* [pix2pix](https://github.com/phillipi/pix2pix)

