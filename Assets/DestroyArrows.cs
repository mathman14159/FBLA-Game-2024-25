using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyArrows : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet_Player"))
        {
            Destroy(other.gameObject);

        }
    }
}
