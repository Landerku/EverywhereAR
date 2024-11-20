using System;
using System.Collections;
using System.IO;
using UnityEngine;

public class CameraLog : MonoBehaviour
{
    private string filePath;

    void Start()
    {
        filePath = System.IO.Path.Combine(Application.persistentDataPath, "CameraLog.txt");

        // 清空文件以便开始新的记录
        File.WriteAllText(filePath, string.Empty);

        StartCoroutine(LogCameraPositionAndRotation());
    }

    IEnumerator LogCameraPositionAndRotation()
    {
        filePath = System.IO.Path.Combine(Application.persistentDataPath, "CameraLog.txt");

        while (true)
        {
            // 设置文件路径，文件将被保存在Assets/Resources文件夹中
            Vector3 position = transform.position;
            Quaternion rotation = transform.rotation;

            // 将信息格式化为字符串
            string log = $"Time: {Time.time}; Position: {position}; Rotation: {rotation}";

            // 使用StreamWriter追加到文件
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    writer.WriteLine(log);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Failed to write to {filePath}: {ex.Message}");
            }


            // 等待0.5秒
            yield return new WaitForSeconds(0.5f);
        }
    }
}
