using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Boss_Script : MonoBehaviour
{
    public int maxHealth = 3;
    public int currentHealth;
    public SpriteRenderer spriteRenderer;
    private Color originalColor;
    public Animator animator;

    public float hitColorDuration = 0.1f;
    


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TakeDamage(int amount)
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
            animator.SetBool("IsHurt", true);
            StartCoroutine(WaitAndDoSomething());
            
        }
        if (other.CompareTag("PurifyArrow"))
        {
            
            Destroy(other.gameObject);
            TakeDamage(1);
            BossScore.instance.DecreaseBossScore(1);
            
            
        }
    }
     
    

    private IEnumerator WaitAndDoSomething()
    {
        // Wait for 1 second
        yield return new WaitForSeconds(.2f);
        animator.SetBool("IsHurt", false);

        // Code to execute after 1 second
        Debug.Log("1 second has passed!");
    }

}
