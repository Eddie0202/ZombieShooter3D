using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBoost : MonoBehaviour
{
    [Header("AmmoBoost")]
    public GunScript rifle;
    public GunInventory inventory;
    private int magToGive = 120;
    private float radius = 2.5f;

    [Header("Sounds")]
    public AudioClip HealthBoostSound;
    public AudioSource audioSource;

    [Header("AmmoBox Animator")]
    public Animator animator;

    private void Update()
    {
        GameObject playerObject = FindNearestPlayerObject();
        if (playerObject != null && Vector3.Distance(transform.position, playerObject.transform.position) < radius)
        {
            if (Input.GetKeyDown("f"))
            {
                animator.SetBool("Open", true);
                rifle.bulletsIHave = magToGive;

                Object.Destroy(gameObject, 1.5f);
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

}
