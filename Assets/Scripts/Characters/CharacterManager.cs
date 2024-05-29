using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    [SerializeField] LogbookManager lm;
    [SerializeField] MonitorText mt;
    [SerializeField] ChoiceManager choiceManager;
    MonitorUI ui;

    [SerializeField] private List<Character> characters = new List<Character>();
    [SerializeField] private List<Character> charactersLeft;

    private GameObject currentCharacterObj;
    private GameObject currentTubeObj;
    private Character currentCharacterInfo;

    [SerializeField] private Transform characterTransform;

    [SerializeField] private GameObject tubePrefab;
    [SerializeField] private Transform tubeTransform;

    public Animator buisAnimator;

    private void Start()
    {
        ui = MonitorUI.instance;
        charactersLeft = new List<Character>(characters);

        //FirstCharacter();
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

    public void InstantiateCharacter(Character randomCharacterInfo)
    {
        currentTubeObj = Instantiate<GameObject>(tubePrefab, tubeTransform.position, tubeTransform.rotation);
    }

    public void NextCharacter()
    {
        Character potentialNextCharacterInfo = GetRandomCharacterIndex(charactersLeft);

        if (potentialNextCharacterInfo != null)
        {
            lm.LogCharacter(currentCharacterInfo);
            currentCharacterInfo = potentialNextCharacterInfo;
            InstantiateCharacter(currentCharacterInfo);
            buisAnimator = currentTubeObj.GetComponent<Animator>();

            ui.SetPopUpPersonalInfo();
            ui.SetStringCharacter(mt.BioToString(currentCharacterInfo));

            choiceManager.canChoose = true;
        }
    }

    public void FirstCharacter()
    {
        currentCharacterInfo = GetRandomCharacterIndex(charactersLeft);
        InstantiateCharacter(currentCharacterInfo);

        ui.SetStringCharacter(mt.BioToString(currentCharacterInfo));

        buisAnimator = currentTubeObj.GetComponent<Animator>();

        choiceManager.canChoose = true;
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
