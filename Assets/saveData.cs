using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class saveData : MonoBehaviour
{
    public int endHealth;
    public int endScore;
    public int bossScore;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bossScore = BossScore.instance.currentBossScore;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player"))
        {
            endHealth = Health.instance.currentHealth;
            endScore = ScoreCounter.instance.currentScore;
            PlayerPrefs.SetInt("Health", endHealth);
            PlayerPrefs.SetInt("Score", endScore);
            if (bossScore == 0)
            {
                SceneManager.LoadScene("Boss_Fight1");
            }
            
            
        }
    }
}
