using System.Collections;
using UnityEngine;

public class Hallucination : MonoBehaviour
{
    public static Hallucination instance;

    private void Awake()
    {
        instance = this;
    }

    [Range(0, 10)][SerializeField] private int hallucinationFactor;

    private Animator animator;

    [SerializeField] private AudioSource[] lowInsanitySFX;
    [SerializeField] private AudioSource[] midInsanitySFX;
    [SerializeField] private AudioSource[] highInsanitySFX;

    private void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(HallucinationLoop());
        StartCoroutine(LoopSFX());
    }

    public void ChangeHallucinationStrength(int amount)
    {
        hallucinationFactor += amount;

    }

    public int GetHallucinationFactor() { return hallucinationFactor; }

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
                float r = Random.Range(5, 20); // time between hallucinations
                r /= hallucinationFactor;

                animator.SetFloat("Blend", Random.Range(0, 1f));
                yield return new WaitForSeconds(r);

                animator.SetBool("Hallucinating", true);
                r = Random.Range(0.1f, 0.6f); // time of hallucination limit
                r *= hallucinationFactor;
                r = Mathf.Clamp(r, 0, hallucinationFactor * 2.5f);
                yield return new WaitForSeconds(r);

                animator.SetBool("Hallucinating", false);
            }
        }
    }
    private IEnumerator LoopSFX()
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
                float r = Random.Range(4f, (20 - hallucinationFactor)); // time between sound proc
                yield return new WaitForSeconds(r);
                try
                {
                    if (hallucinationFactor <= 3) // low insanity
                    {
                        int r2 = Random.Range(0, lowInsanitySFX.Length);
                        lowInsanitySFX[r2].Play();
                    }
                    else if (hallucinationFactor <= 6) // medium insanity
                    {
                        int r2 = Random.Range(0, midInsanitySFX.Length);
                        midInsanitySFX[r2].Play();
                    }
                    else // batshit insane
                    {
                        int r2 = Random.Range(0, highInsanitySFX.Length);
                        highInsanitySFX[r2].Play();
                    }
                }
                catch
                {
                    Debug.LogWarning("warning");
                    lowInsanitySFX[0].Play();
                }
            }
        }
    }
}
