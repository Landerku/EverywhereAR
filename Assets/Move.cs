using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class MovementData
{
    public float timeStamp;
    public Vector3 position;
    public Quaternion rotation;
}

public class Move : MonoBehaviour
{
    public IKControl ikControl;  // 引用IK控制脚本
    private List<MovementData> movements = new List<MovementData>();
    private Vector3 velocity = Vector3.zero;
    void Start()
    {
        LoadAndParseLog();
        if (movements.Count > 0)
        {
            StartCoroutine(MoveAgent());
        }
    }

    void LoadAndParseLog()
    {
        string path = Application.dataPath + "/CameraLog/CameraLog_mid.txt";
        if (File.Exists(path))
        {
            string fileContent = File.ReadAllText(path);
            using (StringReader reader = new StringReader(fileContent))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] parts = line.Split(';');
                    MovementData data = new MovementData
                    {
                        timeStamp = float.Parse(parts[0].Split(':')[1].Trim()),
                        position = ConvertPosition(StringToVector3(parts[1].Split(':')[1].Trim())),
                        rotation = ConvertRotation(StringToQuaternion(parts[2].Split(':')[1].Trim()))
                    };
                    movements.Add(data);
                }
            }
        }
        else
        {
            Debug.LogError("File not found: " + path);
        }
    }

    Vector3 StringToVector3(string s)
    {
        s = s.Trim('(', ')');
        string[] values = s.Split(',');
        return new Vector3(float.Parse(values[0]), float.Parse(values[1]), -float.Parse(values[2]));
    }

    Quaternion StringToQuaternion(string s)
    {
        s = s.Trim('(', ')');
        string[] values = s.Split(',');
        return new Quaternion(float.Parse(values[0]), float.Parse(values[1]), -float.Parse(values[2]), -float.Parse(values[3]));
    }

    Vector3 ConvertPosition(Vector3 position)
    {
        return new Vector3(position.x, position.y, -position.z);
    }

    Quaternion ConvertRotation(Quaternion originalRotation)
    {
        return new Quaternion(originalRotation.x, originalRotation.y, -originalRotation.z, -originalRotation.w);
    }

    IEnumerator MoveAgent()
    {
        float moveDuration = 0.5f;
        Vector3 smoothHeadTargetPosition = movements[0].position;  // 初始头部平滑位置

        if (movements.Count > 0)
        {
            transform.position = movements[0].position;  // 初始位置
            ikControl.headTarget.position = movements[0].position;  // 初始头部位置
        }

        for (int i = 0; i < movements.Count; i++)
        {
            Vector3 startPosition = transform.position;
            Quaternion startRotation = transform.rotation;
            Vector3 targetPosition = movements[i].position;
            Quaternion targetRotation = Quaternion.Euler(0, movements[i].rotation.eulerAngles.y, 0);  // 只使用y轴旋转

            float startTime = Time.time;
            float endTime = startTime + moveDuration;

            while (Time.time < endTime)
            {
                float t = (Time.time - startTime) / moveDuration;
                transform.position = Vector3.Lerp(startPosition, targetPosition, t);
                transform.rotation = Quaternion.Slerp(startRotation, targetRotation, t);

                // 平滑更新头部目标位置
                smoothHeadTargetPosition = Vector3.SmoothDamp(smoothHeadTargetPosition, movements[i].position, ref velocity, moveDuration);
                ikControl.headTarget.position = smoothHeadTargetPosition;

                yield return null;
            }
        }
    }
}
