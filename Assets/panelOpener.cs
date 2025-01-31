using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class panelOpener : MonoBehaviour
{
    public GameObject panelToOpen;

    public float rangeDistance;


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            panelToOpen.SetActive(true);
        }

    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            panelToOpen.SetActive(false);
        }
    }

}
