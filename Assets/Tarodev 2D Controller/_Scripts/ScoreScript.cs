using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    public int value;
    
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Item"))
        {
            Destroy(other.gameObject);
            ScoreCounter.instance.IncreaseScore(value);
            
            
        }
        
    }
}

