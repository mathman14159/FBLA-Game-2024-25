using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using JetBrains.Annotations;

public class Health : MonoBehaviour
{
    public static Health instance;
    public int maxHealth = 5;
    public int currentHealth;
    public Slider healthBar;
    public int BossPoints;
    public int arrowsOnStart;

    public int sceneBuildIndex;
    [SerializeField] GameObject OverheadAttackHitBox;
    [SerializeField] GameObject RoundhouseSwingHitBox;
    [SerializeField] private Animator animator; // Animator for attack animations

    // Start is called before the first frame update
    void Awake(){
        instance = this;
    }
   

    void Start()
    {
        arrowsOnStart = 60;
        PlayerPrefs.SetInt("Arrows", arrowsOnStart);
        currentHealth = 5;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.value = currentHealth;
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            //Dead
        }
        BossPoints = BossScore.instance.currentBossScore;
    }
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            //Dead
            SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Single);
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
        if (other.CompareTag("Spike"))
        {
            currentHealth = 0;
            SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Single);
        }
        if (other.CompareTag("Bone"))
        {
            TakeDamage(1);
        }
        if (other.CompareTag("Swing"))
        {
            TakeDamage(1);
            animator.SetBool("RoundhouseSwing", false);
            RoundhouseSwingHitBox.SetActive(false);
        }
        if (other.CompareTag("Overhead"))
        {
            TakeDamage(1);
        }
    }
}
