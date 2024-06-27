using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject zombiePrefab; // Prefab của zombie
    public Transform[] spawnPoints; // Danh sách các vị trí spawn có thể

    public float spawnInterval = 5f; // Khoảng thời gian giữa các lần spawn
    private float nextSpawnTime; // Thời gian kế tiếp để spawn

    void Start()
    {
        // Đặt thời gian spawn lần đầu tiên
        nextSpawnTime = Time.time + spawnInterval;
    }

    void Update()
    {
        // Kiểm tra nếu đã đến thời gian để spawn
        if (Time.time >= nextSpawnTime)
        {
            // Chọn ngẫu nhiên một vị trí spawn từ danh sách
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

            // Tạo một đối tượng zombie tại vị trí spawn đã chọn
            GameObject newZombie = Instantiate(zombiePrefab, spawnPoint.position, Quaternion.identity);

            // Kiểm tra nếu có NavMeshAgent trên đối tượng zombie
            NavMeshAgent agent = newZombie.GetComponent<NavMeshAgent>();
            if (agent != null)
            {
                // Nếu có, kích hoạt NavMeshAgent
                agent.enabled = true;
                // Đặt mục tiêu của NavMeshAgent thành một vị trí ngẫu nhiên trên NavMesh
                agent.SetDestination(GetRandomPointOnNavMesh());
            }

            // Cập nhật thời gian kế tiếp để spawn
            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    // Hàm để lấy một vị trí ngẫu nhiên trên NavMesh
    Vector3 GetRandomPointOnNavMesh()
    {
        // Khởi tạo một biến Vector3 cho vị trí ngẫu nhiên
        Vector3 randomPosition = Vector3.zero;
        // Thử lặp cho đến khi tìm được vị trí trên NavMesh
        while (true)
        {
            // Tạo một vị trí ngẫu nhiên trong phạm vi (-50, 50) trên mặt phẳng x và z
            randomPosition = new Vector3(Random.Range(-50f, 50f), 0f, Random.Range(-50f, 50f));
            // Kiểm tra xem vị trí này có nằm trên NavMesh không
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPosition, out hit, 1.0f, NavMesh.AllAreas))
            {
                // Nếu có, trả về vị trí này
                return hit.position;
            }
        }
    }
}
