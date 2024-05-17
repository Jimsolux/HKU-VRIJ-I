using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    [SerializeField] LogbookManager lm;
    [SerializeField] MonitorText mt;

    [SerializeField] private List<Character> characters = new List<Character>();
    [SerializeField] private List<Character> charactersLeft;

    private GameObject currentCharacterObj;
    private GameObject currentTubeObj;
    private Character currentCharacterInfo;

    [SerializeField] private Transform characterTransform;

    [SerializeField] private GameObject tubePrefab;
    [SerializeField] private Transform tubeTransform;

    public Animator buisAnimator;

    //Animaties
    // !!! choiceManager.SetCanChoose(); <-- Deze functie bepaalt of je kunt kiezen. Aanroepen vanuit ANIMATOR als een character in de buis is aangekomen, of er uit weggaat.


    private void Start()
    {
        InitializeCharacterManager();

        //FirstCharacter();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            NextCharacter();
            LogCharacter();
        }
    }

    private void InitializeCharacterManager()
    {
        charactersLeft = new List<Character>(characters);
    }

    public Character GetRandomCharacterIndex(List<Character> charactersLeft)
    {
        if (charactersLeft.Count > 0)
        {
            int randomIndex = Mathf.RoundToInt(Random.Range(0, charactersLeft.Count));
            Character characterInfo = charactersLeft[randomIndex];
            charactersLeft.RemoveAt(randomIndex);

            return characterInfo;
        }

        return null;
    }

    // location to instantiate, etc?
    public GameObject InstantiateRandomCharacter(Character randomCharacterInfo)
    {
        GameObject newTube = Instantiate<GameObject>(tubePrefab, tubeTransform.position, tubeTransform.rotation);
        GameObject randomCharacterObj = Instantiate(randomCharacterInfo.obj, characterTransform.position, Quaternion.identity, newTube.transform);
        randomCharacterObj.transform.localScale = new Vector3(0.002f, 0.002f, 0.002f);
        currentCharacterObj = randomCharacterInfo.obj;
        currentTubeObj = newTube;
        return randomCharacterObj;
    }

    public void NextCharacter()
    {
        Character potentialNextCharacterInfo = GetRandomCharacterIndex(charactersLeft);

        if (potentialNextCharacterInfo != null)
        {
            currentCharacterInfo = potentialNextCharacterInfo;
            currentCharacterObj = InstantiateRandomCharacter(currentCharacterInfo);
            mt.SetText(mt.BioToString(currentCharacterInfo));
            //Reset the animator.
            buisAnimator = currentTubeObj.GetComponent<Animator>();
        }
    }

    public void FirstCharacter()
    {
        currentCharacterInfo = GetRandomCharacterIndex(charactersLeft);
        currentCharacterObj = InstantiateRandomCharacter(currentCharacterInfo);
        mt.SetText(mt.BioToString(currentCharacterInfo));
    }

    public void SetCharacterChoice(ChoiceManager.ChoiceEnum choice)
    {
        currentCharacterInfo.choice = choice.ToString();
        currentCharacterInfo.activeChoice = choice;
    }

    public void LogCharacter()
    {
        lm.LogCharacter(currentCharacterInfo); // log previous character
    }

    public void AnimatorSendBackwards()
    {
        buisAnimator.SetTrigger("BuisAchter");
    }
}
