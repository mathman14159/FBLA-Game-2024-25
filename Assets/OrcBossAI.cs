using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class OrcBossAI : MonoBehaviour
{
    [SerializeField] private Transform player; // Reference to the player's Transform
    [SerializeField] private float moveSpeed = 5f; // Movement speed of the boss
    [SerializeField] private float stopDistance = 1.5f; // Distance at which the boss stops to attack
    [SerializeField] private float attackCooldown = 2f; // Time between attacks
    [SerializeField] private Animator animator; // Animator for attack animations

    private bool isAttacking = false; // Whether the boss is currently attacking
    private float lastAttackTime = 0f; // Timestamp of the last attack
    [SerializeField] GameObject OverheadAttackHitBox;
    [SerializeField] GameObject RoundhouseSwingHitBox;

    private void Update()
    {
        // If not attacking, move toward the player
        if (!isAttacking)
        {
            FollowPlayer();
        }

        // Check if it's time to attack
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
        // Move toward the player
        Vector2 direction = (player.position - transform.position).normalized;

        // Move horizontally only
        transform.position += new Vector3(direction.x * moveSpeed * Time.deltaTime, 0, 0);

        // Flip the boss to face the player
        if (direction.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1); // Facing right
        }
        else if (direction.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1); // Facing left
        }
    }

    private IEnumerator PerformAttack()
    {
        isAttacking = true;

        // Randomly choose between two attacks
        bool isOverheadAttack = Random.value > 0.5f;

        if (isOverheadAttack)
        {
            animator.SetBool("OverheadAttack", true); // Trigger overhead attack animation
            OverheadAttackHitBox.SetActive(true);
            StartCoroutine(OverheadAttack());

        }
        else
        {
            animator.SetBool("RoundhouseSwing", true); // Trigger roundhouse swing animation
            RoundhouseSwingHitBox.SetActive(true);
            
        }

        // Wait for the attack animation to finish (assumes 1 second duration)
        yield return new WaitForSeconds(1f);

        // End attack and reset cooldown
        lastAttackTime = Time.time;
        isAttacking = false;
    }

    private void OnDrawGizmosSelected()
    {
        // Visualize the stop distance in the editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, stopDistance);
    }

    private IEnumerator OverheadAttack()
    {
        // Wait for 1 second
        yield return new WaitForSeconds(1f);
        animator.SetBool("OverheadAttack", false);
        OverheadAttackHitBox.SetActive(false);

        // Code to execute after 1 second
        Debug.Log("1 second has passed!");
    }
}
