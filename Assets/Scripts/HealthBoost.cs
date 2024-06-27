using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBoost : MonoBehaviour
{
    [Header("HealthBoost")]
    public PlayerHealth player;
    private int healToGive = 100;
    private float radius = 2.5f;
    public float cooldownDuration = 30f; // Thời gian hồi 30 giây

    [Header("Sounds")]
    public AudioClip HealthBoostSound;
    public AudioSource audioSource;

    [Header("HealthBox Animator")]
    public Animator animator;

    private bool isOnCooldown = false;
    private float cooldownTimer = 0f;

    private void Update()
    {
        GameObject playerObject = FindNearestPlayerObject();
        if (playerObject != null && Vector3.Distance(transform.position, playerObject.transform.position) < radius)
        {
            if (Input.GetKeyDown("f"))
            {
                if (!isOnCooldown)
                {
                    animator.SetBool("Open", true);
                    player.currentHealth = healToGive;
                    StartCoroutine(Cooldown());
                }
                else
                {
                    Debug.Log("Hộp sơ cứu đang trong thời gian hồi, vui lòng đợi " + Mathf.Ceil(cooldownTimer) + " giây nữa.");
                }
            }
        }

        // Giảm đếm thời gian hồi
        if (isOnCooldown)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer <= 0f)
            {
                isOnCooldown = false;
            }
        }
    }

    private GameObject FindNearestPlayerObject()
    {
        // Tìm tất cả các GameObject có tag "Player"
        GameObject[] playerObjects = GameObject.FindGameObjectsWithTag("Player");

        // Nếu không tìm thấy GameObject nào có tag "Player", trả về null
        if (playerObjects.Length == 0)
            return null;

        // Tìm GameObject "Player" gần nhất
        GameObject nearestPlayerObject = playerObjects[0];
        float nearestDistance = Vector3.Distance(transform.position, nearestPlayerObject.transform.position);

        for (int i = 1; i < playerObjects.Length; i++)
        {
            float distance = Vector3.Distance(transform.position, playerObjects[i].transform.position);
            if (distance < nearestDistance)
            {
                nearestPlayerObject = playerObjects[i];
                nearestDistance = distance;
            }
        }

        return nearestPlayerObject;
    }

    private IEnumerator Cooldown()
    {
        isOnCooldown = true;
        cooldownTimer = cooldownDuration;
        yield return new WaitForSeconds(cooldownDuration);
        isOnCooldown = false;
    }
}