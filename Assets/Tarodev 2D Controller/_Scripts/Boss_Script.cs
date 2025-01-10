using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Script : MonoBehaviour
{
    public int maxHealth = 3;
    public int currentHealth;
    public SpriteRenderer spriteRenderer;
    private Color originalColor;

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
            StartCoroutine(ChangeColorTemporarily());
        }
    }
    private IEnumerator ChangeColorTemporarily()
    {
        spriteRenderer.color = Color.red; // Change color to red
        yield return new WaitForSeconds(hitColorDuration); // Wait for a short period
        spriteRenderer.color = originalColor; // Revert to the original color
    }
}
