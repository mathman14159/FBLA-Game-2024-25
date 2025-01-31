using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ArrowKepper : MonoBehaviour
{
   public static ArrowKepper instance;
    public TMP_Text arrowText;
    public int currentArrows;
    
    void Awake()
    {
        instance = this;
    }
    private void Update()
    {
        PlayerPrefs.SetInt("Arrows", currentArrows);
    }

    void Start()
    {
        currentArrows = PlayerPrefs.GetInt("Arrows");
        arrowText.text = "" + currentArrows.ToString();
    }

    public void IncreaseArrows(int v)
    {
        currentArrows += v;
        arrowText.text = "" + currentArrows.ToString();
    }
    public void DecreaseArrows(int v)
    {
        currentArrows -= v;
        arrowText.text = "" + currentArrows.ToString();
    }
    
}
