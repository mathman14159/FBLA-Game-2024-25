using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WerebearBossAI : MonoBehaviour
{
    
    [SerializeField] private Transform player;
    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private float stopDistance = 2f;
    [SerializeField] private float attackCooldown = 2f;
    [SerializeField] private Animator animator;
    [SerializeField] private float knockbackForce = 5f; // Knockback strength for Ground Pound
    [SerializeField] GameObject RoundhouseSwingHitBox;

    private bool isAttacking = false;
    private float lastAttackTime = 0f;
    private Health playerHealth;
    private Rigidbody2D playerRb;
    public int maxHealth;
    public int currentHealth;
    public Slider healthBar;
    public int endHealth;  // PlayerPref on the end of the Players health
    public int endScore; // PlayerPref on the end of the Players score
    

    private void Start()
    {
        currentHealth = (PlayerPrefs.GetInt("BossScore") * 2) + 30;
        playerHealth = player.GetComponent<Health>();
        playerRb = player.GetComponent<Rigidbody2D>();

        if (playerHealth == null)
        {
            Debug.LogError("PlayerHealth script not found on the player!");
        }
        if (playerRb == null)
        {
            Debug.LogError("Player does not have a Rigidbody2D for knockback!");
        }
    }

    private void Update()
    {
        healthBar.value = currentHealth;
        if (!isAttacking)
        {
            FollowPlayer();
        }

        if (Vector2.Distance(transform.position, player.position) <= stopDistance && !isAttacking)
        {
            if (Time.time - lastAttackTime >= attackCooldown)
            {
                StartCoroutine(PerformAttack());
            }
        }
    }

    private void FollowPlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        float distance = Vector2.Distance(transform.position, player.position);

        if (distance > stopDistance)
        {
            transform.position += new Vector3(direction.x * moveSpeed * Time.deltaTime, 0, 0);
            transform.localScale = new Vector3(direction.x > 0 ? 1 : -1, 1, 1);
        }
    }

    private IEnumerator PerformAttack()
    {
        isAttacking = true;
        int attackType = Random.Range(0, 2);

        switch (attackType)
        {
            case 0:
                animator.SetBool("OverheadScratch", true);
                yield return new WaitForSeconds(0.6f);
                animator.SetBool("OverheadScratch", false);
                DealDamage();
                break;

            case 1:
                animator.SetBool("SideSwipe", true);
                yield return new WaitForSeconds(0.7f);
                animator.SetBool("SideSwipe", false);
                DealDamage();
                break;

            case 2:
                animator.SetBool("GroundPound", true);
                yield return new WaitForSeconds(0.5f);
                animator.SetBool("GroundPound", false);
                DealDamage();
                
                break;
        }

        yield return new WaitForSeconds(0.5f);
        lastAttackTime = Time.time;
        isAttacking = false;
    }

    private void DealDamage()
    {
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(1);
        }
    }
    void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            
            endHealth = Health.instance.currentHealth;
            endScore = ScoreCounter.instance.currentScore;
            PlayerPrefs.SetInt("Health", endHealth);
            PlayerPrefs.SetInt("Score", endScore);
            SceneManager.LoadScene("Fake Win Screen");
            //Dead
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, stopDistance);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet_Player"))
        {
            Destroy(other.gameObject);
            TakeDamage(1);
            BossScore.instance.IncreaseBossScore(1);
            animator.SetBool("IsHurt", true);
            StartCoroutine(WaitAndDoSomething());

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
