using UnityEngine;
using UnityEngine.UI; // 【必须添加】用于识别 Image 组件

public class PacmanMove : MonoBehaviour
{
    public float speed = 0.35f;
    private Vector2 des = Vector2.zero;

    [Header("UI引用")]
    public Image faceImage;        // 拖入 FaceDisplay 的 Image 组件

    private void Start()
    {
        des = transform.position;
    }

    private void FixedUpdate()
    {
        // 1. 处理移动（无论是否笑脸，先完成当前的插值移动到格子点）
        Vector2 temp = Vector2.MoveTowards(transform.position, des, speed);
        GetComponent<Rigidbody2D>().MovePosition(temp);

        // 2. 当到达一个格子的中心点时，尝试获取下一个指令
        if ((Vector2)transform.position == des)
        {
            // --- 【核心修改开始】 ---

            // 首先判断是否可以移动
            bool canMove = true;
            if (faceImage != null && faceImage.sprite != null)
            {
                // 如果当前图片的名字是笑脸（请把 "SmileFaceName" 替换为你图片的真实名字）
                if (faceImage.sprite.name == "Smile")
                {
                    canMove = false;
                }
            }

            // 只有在不是笑脸的情况下，才接受按键输入
            if (canMove)
            {
                if (Input.GetKey(KeyCode.W) && valid(Vector2.up))
                {
                    des = (Vector2)transform.position + Vector2.up;
                }
                else if (Input.GetKey(KeyCode.S) && valid(Vector2.down))
                {
                    des = (Vector2)transform.position + Vector2.down;
                }
                else if (Input.GetKey(KeyCode.D) && valid(Vector2.right))
                {
                    des = (Vector2)transform.position + Vector2.right;
                }
                else if (Input.GetKey(KeyCode.A) && valid(Vector2.left))
                {
                    des = (Vector2)transform.position + Vector2.left;
                }
            }

            // --- 【核心修改结束】 ---

            // 获取移动方向并设置动画
            Vector2 dir = des - (Vector2)transform.position;
            GetComponent<Animator>().SetFloat("DirX", dir.x);
            GetComponent<Animator>().SetFloat("DirY", dir.y);
        }
    }

    private bool valid(Vector2 dir)
    {
        Vector2 pos = transform.position;
        RaycastHit2D hit = Physics2D.Linecast(pos + dir, pos);
        return (hit.collider == GetComponent<Collider2D>());
    }
}