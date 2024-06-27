using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public GameObject hurtEffect;
    public MenuHandler menuHandler;
    public AudioSource hurtSound;
    public int maxHealth;
    [HideInInspector] public int currentHealth;
        public HealthBar healthBar;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

        if (healthBar != null)
            healthBar.UpdateHealth(currentHealth, maxHealth);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CallTakeDamage(int damage){
        if(currentHealth <=10){
            menuHandler.GameOver();
        }else{
            if (!hurtSound.isPlaying) { // Kiểm tra xem âm thanh đang không được phát
                hurtSound.Play();
            }
            StartCoroutine(TakeDamage(damage));
        }
        // If player then update health bar
        if (healthBar != null){
            healthBar.UpdateHealth(currentHealth, maxHealth);
        }   
    }
    IEnumerator TakeDamage(int damage){
        currentHealth -= damage;
        hurtEffect.SetActive(true);
      
        yield return new WaitForSeconds(1);
        hurtEffect.SetActive(false);
    }
}
