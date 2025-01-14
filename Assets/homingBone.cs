using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class homingBone : MonoBehaviour
{
    public float speed = 5;
    // Start is called before the first frame update public float speed = 5f; // Speed of the bone
    public float rotationSpeed = 200f; // Speed at which the bone rotates to face the player
    public LayerMask groundLayer; // LayerMask to identify ground
    public float lifeTime = 5f; // How long the bone will exist before disappearing

    private Transform player; // Reference to the player's transform
    private Rigidbody2D rb; // Rigidbody2D for movement
    private bool isHoming = true; // Whether the bone is actively homing in on the player

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform; // Assume player has the "Player" tag
        Destroy(gameObject, lifeTime); // Destroy the bone after a set lifetime
    }

    void FixedUpdate()
    {
        if (isHoming && player != null)
        {
            // Calculate direction to the player
            Vector2 direction = (player.position - transform.position).normalized;

            // Rotate the bone toward the player
            float rotateAmount = Vector3.Cross(direction, transform.right).z;
            rb.angularVelocity = -rotateAmount * rotationSpeed;

            // Move the bone forward
            rb.velocity = transform.right * speed;
        }
    }

    

    

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player"))
        {
            Debug.Log("Bone hit the player!");
            Destroy(gameObject); // Destroy the bone
            
        }
        
    }
}
