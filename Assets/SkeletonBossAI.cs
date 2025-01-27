using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SkeletonBossAI : MonoBehaviour
{
   public Transform player; // Reference to the player
    public float moveSpeed = 2f; // Speed at which the boss moves
    public float stopDistance = 3f; // Distance at which the boss stops following
    public float minStopTime = 1f; // Minimum time the boss will stop to throw bones
    public float maxStopTime = 3f; // Maximum time the boss will stop to throw bones
    public float followDuration = 5f; // Time the boss will follow the player before stopping
    public GameObject bonePrefab; // Prefab for the thrown bone
    public Transform throwPoint; // Point from which the bone is thrown
    public LayerMask groundLayer; // LayerMask to identify ground
    public float groundCheckDistance = 0.5f; // Distance to check for ground
    public int maxHealth;
    public int currentHealth;
    public Slider healthBar;

    private Rigidbody2D rb; // Rigidbody2D for movement
    private bool isFacingRight = true; // Tracks the direction the boss is facing
    private float actionTimer = 0f; // Timer to track state transitions
    private bool isFollowing = true; // Whether the boss is currently following the player
    public int endHealth;
    public int endScore;
    public Animator animator;


    void Awake()
    {
        
    }


    void Start()
    {
        currentHealth = (PlayerPrefs.GetInt("BossScore") * 2) + 20;
        rb = GetComponent<Rigidbody2D>();
        StartFollowing();
    }

    void Update()
    {
        actionTimer -= Time.deltaTime;
        healthBar.value = currentHealth;

        if (isFollowing)
        {
            FollowPlayer();
            if (actionTimer <= 0)
            {
                StartThrowingBones();
            }
        }
    }

    private void FollowPlayer()
    {
        float distanceToPlayer = player.position.x - transform.position.x;

        // Only follow if the player is outside stop distance
        if (Mathf.Abs(distanceToPlayer) > stopDistance)
        {
            int moveDirection = distanceToPlayer > 0 ? 1 : -1;

            // Flip to face the player
            if (moveDirection > 0 && !isFacingRight)
                Flip();
            else if (moveDirection < 0 && isFacingRight)
                Flip();

            // Check for ground and move
            Vector2 groundCheckPosition = new Vector2(transform.position.x + moveDirection * groundCheckDistance, transform.position.y);
            bool isGroundAhead = Physics2D.Raycast(groundCheckPosition, Vector2.down, 1f, groundLayer);

            if (isGroundAhead)
            {
                rb.velocity = new Vector2(moveDirection * moveSpeed, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
        }
        else
        {
            // Stop moving if within stop distance
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }

    private void StartThrowingBones()
    {
        isFollowing = false;
        rb.velocity = Vector2.zero; // Stop moving

        float stopTime = Random.Range(minStopTime, maxStopTime);
        actionTimer = stopTime;

        StartCoroutine(ThrowBonesCoroutine(stopTime));
    }

    private System.Collections.IEnumerator ThrowBonesCoroutine(float stopTime)
    {
        float elapsedTime = 0f;

        while (elapsedTime < stopTime)
        {
            elapsedTime += 1f;

            // Throw a bone at the player
            ThrowBone();

            // Wait 1 second between throws
            yield return new WaitForSeconds(1f);
        }

        StartFollowing(); // Resume following after throwing
    }

    private void ThrowBone()
    {
        // Instantiate a bone and aim it at the player
        Vector2 direction = (player.position - throwPoint.position).normalized;
        GameObject bone = Instantiate(bonePrefab, throwPoint.position, Quaternion.identity);
        Rigidbody2D rbBone = bone.GetComponent<Rigidbody2D>();
        if (rbBone != null)
        {
            rbBone.velocity = direction * 5f; // Adjust bone speed
        }

        Debug.Log("Skeleton Boss threw a bone!");
        TakeDamage(1);
    }

    private void StartFollowing()
    {
        isFollowing = true;
        actionTimer = followDuration;
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
    void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            SceneManager.LoadScene("Hub World-Heaven");
            endHealth = Health.instance.currentHealth;
            endScore = ScoreCounter.instance.currentScore;
            PlayerPrefs.SetInt("Health", endHealth);
            PlayerPrefs.SetInt("Score", endScore);
            //Dead
        }
    }

    private void OnDrawGizmos()
    {
        // Debugging ground check ray
        Gizmos.color = Color.green;
        Vector2 groundCheckPosition = new Vector2(transform.position.x + (isFacingRight ? groundCheckDistance : -groundCheckDistance), transform.position.y);
        Gizmos.DrawLine(groundCheckPosition, groundCheckPosition + Vector2.down * 1f);
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
