using UnityEngine;

public class LevelExit : MonoBehaviour
{
    [Header("设置要跳转的场景名")]
    public string nextSceneName;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 1. 检查进入区域的是不是玩家
        // 确保你的 Pacman 物体 Tag 设为了 "Player"
        if (other.CompareTag("Player"))
        {
            // 2. 调用 GameManager 的单例引用，并传入场景名
            if (GameManager.Instance != null)
            {
                GameManager.Instance.Win(nextSceneName);
            }
            else
            {
                Debug.LogError("场景中找不到 GameManager 实例！");
            }
        }
    }
}