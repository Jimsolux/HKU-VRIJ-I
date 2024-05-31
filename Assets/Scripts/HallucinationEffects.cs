using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HallucinationEffects : MonoBehaviour
{
    public static HallucinationEffects instance;
    private void Awake()
    {
        instance = this;
    }

    private Hallucination hallucination;
    void Start()
    {
        hallucination = Hallucination.instance;
        StartCoroutine(LoopVFX());
    }

    [SerializeField] private GameObject[] floatyHumans;
    [SerializeField] private GameObject[] monitorChange;
    [SerializeField] private GameObject extraMug;
    [SerializeField] private GameObject personStaringDown;
    [SerializeField] private GameObject[] crowdStaring;

    private IEnumerator LoopVFX()
    {
        while (true)
        {
            int hallucinationFactor = hallucination.GetHallucinationFactor();
            if (hallucinationFactor == 0)
            {
                yield return new WaitForEndOfFrame();
                continue;
            }
            else
            {
                float r = Random.Range(0f, (16 - hallucinationFactor));

                yield return new WaitForSeconds(r);
                r = Mathf.RoundToInt(Random.Range(0, 4)); // event type
                switch (r)
                {
                    case 0:
                        HandleFloatyHumans(4, hallucinationFactor);
                        break;
                    case 1:

                        if (CameraMovement.instance.Direction() == CameraMovement.CameraDirection.Right)
                        {
                            StartCoroutine(HandleOBJ(monitorChange[Random.Range(0, monitorChange.Length)], hallucinationFactor, 3));
                        }
                        else
                        {
                            if (!switchedMonitor)
                            {
                                switchedMonitor = true;
                                monitorChange[Random.Range(0, monitorChange.Length)].SetActive(true);
                            }
                            else HandleFloatyHumans(floatyHumans.Length, hallucinationFactor);
                        }
                        break;
                    case 2:
                        StartCoroutine(HandleOBJ(extraMug, hallucinationFactor, 3));
                        break;
                    case 3:
                        if (hallucinationFactor < 3)
                        {
                            personStaringDown.GetComponent<HumanRandomizer>().Randomize();
                            StartCoroutine(HandleOBJ(personStaringDown, hallucinationFactor, 5));
                        }
                        else
                        {
                            int crowdSize = Random.Range(1, hallucinationFactor);
                            crowdSize = Mathf.Clamp(crowdSize, 1, crowdStaring.Length);
                            for (int i = 0; i < crowdSize; i++)
                            {
                                GameObject person = crowdStaring[i];

                                person.GetComponent<HumanRandomizer>().Randomize();
                                StartCoroutine(HandleOBJ(person, hallucinationFactor, 2));

                            }
                        }
                        break;
                }
            }
        }
    }

    private void HandleFloatyHumans(int maxHumans, int hallucinationFactor)
    {
        int humanCount = Random.Range(1, hallucinationFactor);
        humanCount = Mathf.Clamp(humanCount, 1, maxHumans);

        List<GameObject> humansEnabled = new List<GameObject>();

        while (humansEnabled.Count < humanCount)
        {
            GameObject human = floatyHumans[Random.Range(0, floatyHumans.Length)];

            if (!humansEnabled.Contains(human))
            {
                humansEnabled.Add(human);
                human.GetComponent<HumanRandomizer>().Randomize();
                StartCoroutine(HandleOBJ(human, hallucinationFactor, 5));
            }
        }
    }

    private bool switchedMonitor = false;
    public bool SwitchedMonitor() { return switchedMonitor; }
    public IEnumerator ClearMonitorEffect()
    {
        switchedMonitor = false;
        yield return new WaitForSeconds(Random.Range(0, 3));
        foreach (GameObject g in monitorChange)
        {
            g.SetActive(false);
        }
    }

    private IEnumerator HandleOBJ(GameObject obj, int insanity, int timeDivider)
    {
        obj.SetActive(true);
        yield return new WaitForSeconds(Random.Range(0, insanity / timeDivider));
        obj.SetActive(false);
    }

}