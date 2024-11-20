using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly : MonoBehaviour
{
    public float radius = 5.0f;    // 圆周运动的半径
    public float speed = 2.0f;     // 旋转速度
    private Vector3 center;        // 中心点
    private float angle;           // 当前角度

    void Start()
    {
        center = transform.position;  // 初始时，将物体当前位置设为旋转的中心点
    }

    void Update()
    {
        RotateAroundPoint();
    }

    void RotateAroundPoint()
    {
        angle += speed * Time.deltaTime; // 根据速度更新角度
        float radian = angle * Mathf.Deg2Rad; // 将角度转换为弧度

        // 计算新的位置
        Vector3 offset = new Vector3(Mathf.Sin(radian), 0, Mathf.Cos(radian)) * radius;
        transform.position = center + offset; // 更新物体的位置

        // 计算切线方向，即当前位置的角度加90度的方向
        Vector3 tangent = new Vector3(Mathf.Cos(radian), 0, -Mathf.Sin(radian));
        transform.rotation = Quaternion.LookRotation(tangent);
    }
}
