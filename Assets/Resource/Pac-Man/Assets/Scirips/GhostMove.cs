using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GhostMove : MonoBehaviour
{
    public GameObject[] paths;
    public float speed = 0.4f;

    [Header("碰撞设置")]
    public LayerMask wallLayer; // 在 Inspector 中设置为 "Wall" 所在的图层
    public float detectRadius = 0.3f; // 检测半径，根据幽灵大小调整

    [SerializeField]
    private List<Vector3> Waypoint = new List<Vector3>();
    private int index = 0;
    private Vector3 x;

    private void getPath(GameObject path)
    {
        Waypoint.Clear();
        foreach (Transform t in path.transform)
        {
            Waypoint.Add(t.position);
        }
    }

    private void Start()
    {
        x = transform.position; // 记录出生点（原代码逻辑中 x 未初始化，这里补齐）
        getPath(paths[GameManager.Instance.usingIndex[GetComponent<SpriteRenderer>().sortingOrder - 1]]);
    }

    private void FixedUpdate()
    {
        if (Waypoint.Count == 0) return;

        // 1. 计算移动方向
        Vector2 targetPos = Waypoint[index];
        Vector2 currentPos = transform.position;
        Vector2 dir = (targetPos - currentPos).normalized;

        // 2. --- 核心修改：墙壁检测 ---
        // 使用 CircleCast 像手电筒一样照一下前方是否有 Wall 图层的东西
        float distToTarget = Vector2.Distance(currentPos, targetPos);
        RaycastHit2D hit = Physics2D.CircleCast(currentPos, detectRadius, dir, speed, wallLayer);

        if (hit.collider != null)
        {
            // 如果撞到了墙，说明这条路不通，直接切换到下一个路径点，防止卡死在墙边
            Debug.Log("前面有墙，切换路径点");
            index++;
            CheckIndex();
            return;
        }

        // 3. 正常移动逻辑
        if (transform.position != Waypoint[index])
        {
            Vector2 temp = Vector2.MoveTowards(transform.position, Waypoint[index], speed);
            GetComponent<Rigidbody2D>().MovePosition(temp);
        }
        else
        {
            index++;
            CheckIndex();
        }

        // 4. 设置动画
        Vector2 animDir = Waypoint[index] - transform.position;
        GetComponent<Animator>().SetFloat("DirX", animDir.x);
        GetComponent<Animator>().SetFloat("DirY", animDir.y);
    }

    // 提取出来的索引检查逻辑
    private void CheckIndex()
    {
        if (index >= Waypoint.Count)
        {
            index = 0;
            getPath(paths[Random.Range(0, paths.Length)]);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Pacman")
        {
            if (GameManager.Instance.isSuperPacman)
            {
                // 回到出生点
                gameObject.transform.position = x;
                index = 0;
                GameManager.Instance.score += 500;
            }
            else
            {
                Debug.Log("Game Over");
                GameManager.Instance.PlayAlive = false;
                collision.gameObject.SetActive(false);
                GameManager.Instance.gamePanel.SetActive(false);
                Instantiate(GameManager.Instance.gameoverPrefab);
                GameManager.Instance.Invoke("ReStart", 2.0f);
                this.enabled = false;
            }
        }
    }

    //private void ReStart()
    //{
    //    SceneManager.LoadScene(0);
    //}
}