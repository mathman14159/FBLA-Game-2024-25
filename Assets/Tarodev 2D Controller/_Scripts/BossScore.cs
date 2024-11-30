using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScore : MonoBehaviour
{
    // Start is called before the first frame update
   public static BossScore instance;
   
    public int currentBossScore = 0;
    
    void Awake()
    {
        instance = this;
    }

    
    void Start()
    {
        
    }

    public void IncreaseBossScore(int v)
    {
        currentBossScore += v;
        
    }
    
}
