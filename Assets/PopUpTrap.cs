using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpTrap : MonoBehaviour
{
    [SerializeField] private float activeTime = 2f; // Time spikes stay up
    [SerializeField] private float inactiveTime = 2f; // Time spikes stay down
    [SerializeField] private Animator animator; // Animator for animations (optional)
    [SerializeField] GameObject trap;

    private bool isActive = false;

    private void Start()
    {
        StartCoroutine(SpikeCycle());
    }

    private IEnumerator SpikeCycle()
    {
        while (true)
        {
            ActivateSpikes();
            yield return new WaitForSeconds(activeTime);

            DeactivateSpikes();
            yield return new WaitForSeconds(inactiveTime);
        }
    }

    private void ActivateSpikes()
    {
        trap.SetActive(true);
        if (animator != null) animator.SetTrigger("Appear");
    }

    private void DeactivateSpikes()
    {
        trap.SetActive(false);
        if (animator != null) animator.SetTrigger("Disappear");
    }

   
}
