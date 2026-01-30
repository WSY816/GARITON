using UnityEngine;
using UnityEngine.UI;

public class WheelAutoRotate : MonoBehaviour
{
    [Header("旋转设置")]
    public float rotationSpeed = 90f; // 每秒旋转的角度（正数逆时针，负数顺时针）
    private bool isRotating = true;   // 当前是否正在旋转

    [Header("UI引用")]
    public Image faceImage;        // 拖入 FaceDisplay 的 Image 组件
    public Sprite smileSprite;     // 拖入笑脸图片
    public Sprite crySprite;       // 拖入哭脸图片

    void Update()
    {
        // 1. 检测键盘输入 E (按下切换状态)
        if (Input.GetKeyDown(KeyCode.E))
        {
            isRotating = !isRotating; // 切换旋转/停止状态
        }

        // 2. 如果处于旋转状态，则执行旋转
        if (isRotating)
        {
            // Vector3.forward 对应 Z 轴
            // Time.deltaTime 确保在不同帧率下旋转速度一致
            transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
        }

        // 3. 无论是否旋转，都更新表情状态
        UpdateFaceStatus();
    }

    private void UpdateFaceStatus()
    {
        // 获取当前 Z 轴的欧拉角 (Unity 自动处理为 0-360 度)
        float currentZ = transform.localEulerAngles.z;

        // 根据角度判断：0-180度显示笑脸，180-360度显示哭脸
        // 注意：0度是正右方，90度是正上方
        if (currentZ >= 0 && currentZ < 180)
        {
            if (faceImage.sprite != smileSprite)
            {
                faceImage.sprite = smileSprite;
            }
        }
        else
        {
            if (faceImage.sprite != crySprite)
            {
                faceImage.sprite = crySprite;
            }
        }
    }
}