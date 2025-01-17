using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class saveData : MonoBehaviour
{
    public int endHealth;
    public int endScore;
    public int endBossScore;
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
            endBossScore = BossScore.instance.currentBossScore;
            PlayerPrefs.SetInt("BossScore", endBossScore);
            PlayerPrefs.SetInt("Health", endHealth);
            PlayerPrefs.SetInt("Score", endScore);
            if (bossScore >= 0 && bossScore < 3)
            {
                SceneManager.LoadScene("Boss_Fight");
            }
            else if (bossScore >= 3 && bossScore <= 15)
            {
                SceneManager.LoadScene("Boss_Fight(V2)");
            }
            else if(bossScore > 15)
            {
                SceneManager.LoadScene("Boss_Fight(V3)");
            }


        }
    }
}
