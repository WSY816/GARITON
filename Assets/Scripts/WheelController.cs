using UnityEngine;

public class PointerRotate : MonoBehaviour
{
    [Header("旋转设置")]
    // 拖入你的指针物体（Pointer）
    public Transform pointerTransform;
    public float rotationSpeed = 90f;
    private bool isRotating = true;

    [Header("玩家引用")]
    // 这里改成了 SpriteRenderer，用来控制场景中玩家的图片
    public SpriteRenderer playerSpriteRenderer;
    public Sprite smileSprite;
    public Sprite crySprite;

    void Update()
    {
        // 1. 按 E 切换状态
        if (Input.GetKeyDown(KeyCode.E))
        {
            isRotating = !isRotating;
        }

        // 2. 如果正在旋转，旋转指针
        if (isRotating && pointerTransform != null)
        {
            pointerTransform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
        }

        // 3. 根据指针的角度更新玩家的 Sprite
        UpdatePlayerSprite();
    }

    private void UpdatePlayerSprite()
    {
        // 确保引用都存在
        if (pointerTransform == null || playerSpriteRenderer == null) return;

        // 获取指针当前的 Z 轴角度
        float currentZ = pointerTransform.localEulerAngles.z;

        // 根据角度判断：0-180度显示笑脸，180-360度显示哭脸
        if (currentZ >= 0 && currentZ < 180)
        {
            // 只有当图片不一样时才更换，节省性能
            if (playerSpriteRenderer.sprite != smileSprite)
            {
                playerSpriteRenderer.sprite = smileSprite;
            }
        }
        else
        {
            if (playerSpriteRenderer.sprite != crySprite)
            {
                playerSpriteRenderer.sprite = crySprite;
            }
        }
    }
}