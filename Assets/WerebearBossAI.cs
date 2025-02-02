using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void Start()
    {
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


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, stopDistance);
    }
}
