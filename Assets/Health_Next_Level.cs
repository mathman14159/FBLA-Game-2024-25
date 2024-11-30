using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health_Next_Level : MonoBehaviour
{
    // Start is called before the first frame update
   public static Health_Next_Level instance;
    public int maxHealth = 5;
    public int currentHealth;
    public Slider healthBar;
    public int BossPoints;

    // Start is called before the first frame update
    void Awake(){
        instance = this;
    }
   

    void Start()
    {
        currentHealth = PlayerPrefs.GetInt("Health");
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.value = currentHealth;
    }
    void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            //Dead
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Bad_Guy"))
        {
            BossPoints = BossScore.instance.currentBossScore;
            TakeDamage(1);
        }
        if (other.CompareTag("Boss"))
        {
            BossPoints = BossScore.instance.currentBossScore;
            TakeDamage(BossPoints);
        }
        
    }
}
