using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hallucination : MonoBehaviour
{
    [Range(0, 20)][SerializeField] private int hallucinationFactor;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(HallucinationLoop());
    }

    public void ChangeHallucinationStrength(int amount)
    {
        hallucinationFactor += amount;

        animator.speed = hallucinationFactor;
    }

    private IEnumerator HallucinationLoop()
    {
        while (true)
        {

            if (hallucinationFactor == 0)
            {
                yield return new WaitForEndOfFrame();
                continue; 
            }
            else
            {
                float r = Random.Range(0, 20);
                r /= hallucinationFactor;
                yield return new WaitForSeconds(r);
            }
        }
    }
}
