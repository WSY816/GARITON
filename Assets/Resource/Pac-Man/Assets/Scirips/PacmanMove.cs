using UnityEngine;

public class PacmanMove : MonoBehaviour
{
    public float speed = 0.35f;
    private Vector2 des = Vector2.zero;

    [Header("玩家图片引用")]
    // 改为 SpriteRenderer，拖入带有 SpriteRenderer 的物体（通常就是玩家自己）
    public SpriteRenderer playerSpriteRenderer;

    private void Start()
    {
        des = transform.position;

        // 自动初始化：如果没手动拖入，尝试获取自己身上的 SpriteRenderer
        if (playerSpriteRenderer == null)
        {
            playerSpriteRenderer = GetComponent<SpriteRenderer>();
        }
    }

    private void FixedUpdate()
    {
        // 1. 处理移动（插值移动到格子点）
        Vector2 temp = Vector2.MoveTowards(transform.position, des, speed);
        GetComponent<Rigidbody2D>().MovePosition(temp);

        // 2. 当到达一个格子的中心点时，尝试获取下一个指令
        if ((Vector2)transform.position == des)
        {
            // --- 【核心逻辑】 ---

            bool canMove = true;
            if (playerSpriteRenderer != null && playerSpriteRenderer.sprite != null)
            {
                // 如果当前图片的名字是 Smile，则不能移动
                // 请确保你在 PointerRotate 脚本里设置的图片文件名确实叫 "Smile"
                if (playerSpriteRenderer.sprite.name == "Smile")
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

            // 获取移动方向并设置动画
            Vector2 dir = des - (Vector2)transform.position;
            GetComponent<Animator>().SetFloat("DirX", dir.x);
            GetComponent<Animator>().SetFloat("DirY", dir.y);
        }
    }

    private bool valid(Vector2 dir)
    {
        Vector2 pos = transform.position;
        // 注意：Linecast 检测路径上是否有障碍物
        RaycastHit2D hit = Physics2D.Linecast(pos + dir, pos);
        return (hit.collider == GetComponent<Collider2D>());
    }
}