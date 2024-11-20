using Microsoft.MixedReality.WorldLocking.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepLocation : MonoBehaviour
{
    private SpacePin spacePin;

    void Start()
    {
        spacePin = GetComponent<SpacePin>();
    }

    // 调用以开始交互或移动
    public void StartMovement()
    {
        // 交互开始时的逻辑（如果有）
    }

    // 调用以结束交互或移动
    public void StopMovement()
    {
        if (spacePin != null)
        {
            // 交互结束，更新SpacePin的位置
            Pose currentPose = new Pose(transform.position, transform.rotation);
            spacePin.SetFrozenPose(currentPose);
            Debug.Log("Yeah!!!");
        }
    }
}
