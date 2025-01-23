using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class OrcBossAI : MonoBehaviour
{
    public float moveSpeed = 3f; // Movement speed of the Orc
    public float followRange = 10f; // Distance at which the Orc starts following the player
    public float attackRange = 2f; // Distance at which the Orc can attack
    public float swingAttackDelay = 1f; // Time between attack choices
    public float attackCooldown = 0f; // Cooldown for the next attack

    private Transform player; // Reference to the player
    private Rigidbody2D rb; // Orc's Rigidbody2D for movement
    private bool isAttacking = false; // Flag to check if Orc is currently attacking

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // Find the player by tag
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component
    }

    void Update()
    {
        if (attackCooldown > 0)
            attackCooldown -= Time.deltaTime;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= followRange && !isAttacking)
        {
            FollowPlayer(); // Move towards the player if within follow range
        }

        if (distanceToPlayer <= attackRange && attackCooldown <= 0 && !isAttacking)
        {
            PerformAttack(); // Perform attack if within range and cooldown is over
        }
    }

    // Function to follow the player
    void FollowPlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized; // Get direction towards player
        rb.velocity = new Vector2(direction.x * moveSpeed, rb.velocity.y); // Move towards player horizontally

        // Flip Orc sprite to face player
        if (player.position.x < transform.position.x)
            transform.localScale = new Vector3(-1, 1, 1); // Flip to face left
        else
            transform.localScale = new Vector3(1, 1, 1); // Flip to face right
    }

    // Function to perform an attack
    void PerformAttack()
    {
        isAttacking = true;
        rb.velocity = Vector2.zero; // Stop movement by setting velocity to zero
        attackCooldown = swingAttackDelay; // Set cooldown to prevent immediate re-attacks

        // Randomly choose between a swing attack or overhead attack
        int attackChoice = Random.Range(0, 2); // 0 for swing, 1 for overhead

        if (attackChoice == 0)
        {
            SwingAttack();
        }
        else
        {
            OverheadAttack();
        }
    }

    // Function for the quick swing attack
    void SwingAttack()
    {
        // Here you would implement the logic for the swing attack, like damaging the player
        Debug.Log("Orc performs a quick swing attack!");

        // Add your attack animation logic or damage logic here

        isAttacking = false; // Reset attack flag
    }

    // Function for the slower overhead attack
    void OverheadAttack()
    {
        // Here you would implement the logic for the overhead attack, like damaging the player
        Debug.Log("Orc performs a slower overhead attack!");

        // Add your attack animation logic or damage logic here

        isAttacking = false; // Reset attack flag
    }
}
