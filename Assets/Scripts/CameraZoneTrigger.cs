using UnityEngine;
using Cinemachine;

public class CameraZoneTrigger : MonoBehaviour
{
    public CinemachineVirtualCamera targetCamera;

    private int activePriority = 20;
    private int inactivePriority = 10;

    // 注意：这里必须加上 "2D" 字样，且参数是 Collider2D
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("玩家进入区域，切换相机"); // 调试日志
            targetCamera.Priority = activePriority;
        }
    }

    // 注意：这里也必须加上 "2D"
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("玩家离开区域，恢复优先级"); // 调试日志
            targetCamera.Priority = inactivePriority;
        }
    }
}