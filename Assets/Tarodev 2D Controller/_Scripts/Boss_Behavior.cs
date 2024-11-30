using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Behavior : MonoBehaviour
{
    // Start is called before the first frame updatepublic int maxHealth = 3;
    public int currentHealth;
    public int BossHealth;
    
    
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = BossHealth;
        
    }

    // Update is called once per frame
    void Update()
    {
        currentHealth = BossHealth;
        BossHealth = BossScore.instance.currentBossScore;
    }
    void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            
            Destroy(gameObject);
            
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Bullet_Player"))
        {
            Destroy(other.gameObject);
            TakeDamage(1);
            BossScore.instance.IncreaseBossScore(1);
            BossHealth = BossScore.instance.currentBossScore;
        }
    }
}
