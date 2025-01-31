using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] GameObject ShopMenu;
    public int arrows;
    public int coins;
    public int cost;
    // Start is called before the first frame update
    void Start()
    {
        arrows = PlayerPrefs.GetInt("Arrows");
        coins = PlayerPrefs.GetInt("Score");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void BuyArrows()
    {
        if (coins > 0)
        {
            ArrowKepper.instance.IncreaseArrows(1);
            
            coins -= 1;
            PlayerPrefs.SetInt("Arrows", arrows);
            PlayerPrefs.SetInt("Score",coins);
        }
    }
}
