using UnityEngine;
using UnityEngine.UI;

public class PointerRotate : MonoBehaviour
{
    [Header("旋转设置")]
    // 拖入你的指针物体（Pointer）
    public Transform pointerTransform;
    public float rotationSpeed = 90f;
    private bool isRotating = true;

    [Header("UI引用")]
    public Image faceImage;
    public Sprite smileSprite;
    public Sprite crySprite;

    void Update()
    {
        // 1. 按 E 切换状态
        if (Input.GetKeyDown(KeyCode.E))
        {
            isRotating = !isRotating;
        }

        // 2. 如果正在旋转，旋转指针而不是表盘
        if (isRotating && pointerTransform != null)
        {
            // 旋转指针的 Z 轴
            pointerTransform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
        }

        // 3. 根据指针的角度更新表情
        UpdateFaceStatus();
    }

    private void UpdateFaceStatus()
    {
        if (pointerTransform == null) return;

        // 获取指针当前的 Z 轴角度
        float currentZ = pointerTransform.localEulerAngles.z;

        // 逻辑保持不变：0-180度笑脸，180-360度哭脸
        if (currentZ >= 0 && currentZ < 180)
        {
            if (faceImage.sprite != smileSprite)
                faceImage.sprite = smileSprite;
        }
        else
        {
            if (faceImage.sprite != crySprite)
                faceImage.sprite = crySprite;
        }
    }
}