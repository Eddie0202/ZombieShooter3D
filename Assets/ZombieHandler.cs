using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class ZombieHandler : MonoBehaviour
{
    public float health;
    public GameObject deadEffect;
    public NavMeshAgent navMeshAgent;
    public Animator animator;
    private string sceneName;
    private int zombieSpotDistance;
    float attackCoolDownTimer;
    Transform target;

    // Start is called before the first frame update
    void Start()
    {
        // Khởi tạo target ban đầu là null
        target = null;
        // Lấy scene hiện tại
        Scene currentScene = SceneManager.GetActiveScene();

        // Lấy tên của scene hiện tại
        sceneName = currentScene.name;
        if(sceneName == "Map_1")
        {
            zombieSpotDistance = 15;
        }
        else
        {
            zombieSpotDistance = 50;
        }
        // In ra tên scene hiện tại
        Debug.Log("Tên của scene hiện tại là: " + sceneName);
    
}

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            FindNearestPlayer();
        }
        else
        {
            float _distance = Vector3.Distance(transform.position, target.position);
            animator.SetTrigger("Run");
            if (_distance > 2)
            {

                navMeshAgent.isStopped = false;
                navMeshAgent.SetDestination(target.position);
            }
            else
            {
                navMeshAgent.isStopped = true;
                if (attackCoolDownTimer <= 0)
                {
                    attackCoolDownTimer = 3;
                    animator.SetTrigger("Attack");
                    if (target.GetComponent<PlayerHealth>() != null)
                    {
                        target.GetComponent<PlayerHealth>().CallTakeDamage(10);
                    }
                }
                Vector3 dir = (target.position - transform.position).normalized;
                Quaternion lookAngle = Quaternion.LookRotation(new Vector3(dir.x,0,dir.z));
                transform.rotation = Quaternion.Slerp(transform.rotation,lookAngle,Time.deltaTime*10);

            }

            if (attackCoolDownTimer > 0)
            {
                attackCoolDownTimer -= Time.deltaTime;
            }
        }
    }

// Hàm tìm người chơi gần nhất trong phạm vi của zombie và có thể tấn công được
void FindNearestPlayer()
{
    GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
    if (players.Length > 0)
    {
        float shortestDistance = Mathf.Infinity;
        GameObject nearestPlayer = null;
        foreach (GameObject player in players)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            Timer timer = FindObjectOfType<Timer>();
            if (distanceToPlayer < shortestDistance && distanceToPlayer <= zombieSpotDistance) // Thay 10 bằng khoảng cách tối đa mà zombie có thể tấn công
            {
                shortestDistance = distanceToPlayer;
                nearestPlayer = player;
            }
        }
        // Cập nhật target với người chơi gần nhất và có thể tấn công được
        target = nearestPlayer?.transform;
    }
}


    public void TakeDamage()
    {
        health -= 10;
        if (health <= 0)
        {
            GameObject _effect = Instantiate(deadEffect, transform.position + new Vector3(0, 2, 0), Quaternion.identity);
            Destroy(_effect, 3f);
            MenuHandler menuHandler = FindObjectOfType<MenuHandler>();
            if(menuHandler){
                menuHandler.score +=10;
            }
            Destroy(gameObject);
        }
    }
}
