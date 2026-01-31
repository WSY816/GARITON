using UnityEngine;

public class PacmanMove : MonoBehaviour
{
    public float speed = 0.35f;
    private Vector2 des = Vector2.zero;

    [Header("设置")]
    public SpriteRenderer playerSpriteRenderer;
    // 建议在 Inspector 面板里把墙壁设置为一个特定的 Layer（比如叫 "Walls"），然后在这里选择它
    public LayerMask obstacleLayer;

    private void Start()
    {
        des = transform.position;

        if (playerSpriteRenderer == null)
        {
            playerSpriteRenderer = GetComponent<SpriteRenderer>();
        }

    }

    private void FixedUpdate()
    {
        // 1. 处理移动
        Vector2 temp = Vector2.MoveTowards(transform.position, des, speed);
        GetComponent<Rigidbody2D>().MovePosition(temp);

        // 2. 只有到达格子中心点时，才允许转向/输入
        // 使用 Distance 避免浮点数精度问题导致的“无法到达目的地”
        if (Vector2.Distance(transform.position, des) < 0.001f)
        {
            bool canMove = true;
            if (playerSpriteRenderer != null && playerSpriteRenderer.sprite != null)
            {
                if (playerSpriteRenderer.sprite.name == "Smile")
                {
                    canMove = false;
                }
            }

            if (canMove)
            {
                // 检测按键并检查前方是否有障碍物
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

            // --- 动画机已关闭，以下代码如果报错可以注释掉 ---
            /*
            Animator anim = GetComponent<Animator>();
            if (anim != null && anim.enabled) 
            {
                Vector2 dir = des - (Vector2)transform.position;
                anim.SetFloat("DirX", dir.x);
                anim.SetFloat("DirY", dir.y);
            }
            */
        }
    }

    private bool valid(Vector2 dir)
    {
        // 获取当前位置
        Vector2 pos = transform.position;

        // 【核心修改】：从当前位置向目标方向发出一根短射线
        // 我们检测目标点 (pos + dir) 是否有障碍物
        // 如果你使用了 LayerMask，射线会变得非常精准
        RaycastHit2D hit = Physics2D.Linecast(pos + dir, pos, obstacleLayer);

        // 如果没有碰撞到任何东西，说明是空的，可以走
        return (hit.collider == null);

        // 如果你没有设置 LayerMask，请使用原来的逻辑，但要确保障碍物有 BoxCollider2D：
        // return (hit.collider == GetComponent<Collider2D>());
    }


}