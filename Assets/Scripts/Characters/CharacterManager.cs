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

    [SerializeField] private Character emptyCharacter;

    [SerializeField] private Transform characterTransform;

    [SerializeField] private GameObject tubePrefab;
    [SerializeField] private GameObject tubePrefabEmpty;
    [SerializeField] private Transform tubeTransform;

    public Animator buisAnimator;

    [Header("After texts")]
    [SerializeField][TextArea] private List<string> petText = new();
    [SerializeField][TextArea] private List<string> museumText = new();
    [SerializeField][TextArea] private List<string> experimentText = new();
    [SerializeField][TextArea] private List<string> breedingText = new();
    [SerializeField][TextArea] private List<string> makeFoodText = new();
    [SerializeField][TextArea] private List<string> skipText = new();

    private void Start()
    {
        ui = MonitorUI.instance;
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

    public void InstantiateCharacter(Character randomCharacterInfo)
    {
        if(randomCharacterInfo == emptyCharacter)
        {
            currentTubeObj = Instantiate<GameObject>(tubePrefabEmpty, tubeTransform.position, tubeTransform.rotation);
            choiceManager.LockdownChoice();
        }
        else currentTubeObj = Instantiate<GameObject>(tubePrefab, tubeTransform.position, tubeTransform.rotation);
    }

    public void NextCharacter()
    {
        Character potentialNextCharacterInfo = GetRandomCharacterIndex(charactersLeft);

        if (potentialNextCharacterInfo != null)
        {
            lm.LogCharacter(currentCharacterInfo);

            if (GameManager.Instance.OutOfTime())
            {
                currentCharacterInfo = emptyCharacter;
                choiceManager.canChoose = false;
                lm.LogLastCharacter();
            }
            else
            {
                currentCharacterInfo = potentialNextCharacterInfo;
                choiceManager.canChoose = true;
            }

            InstantiateCharacter(currentCharacterInfo);
            buisAnimator = currentTubeObj.GetComponent<Animator>();

            ui.SetPopUpPersonalInfo();
            ui.SetStringCharacter(mt.BioToString(currentCharacterInfo));

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

    public void SetCharacterChoice(ChoiceManager.ChoiceEnum choice, Sprite sprite)
    {
        currentCharacterInfo.choice = choice.ToString();
        currentCharacterInfo.activeChoice = choice;
        DecideAfterText(choice, currentCharacterInfo, sprite);

    }

    public void LogCharacter()
    {
        lm.LogCharacter(currentCharacterInfo); // log previous character
    }

    public void AnimatorSendBackwards()
    {
        buisAnimator.SetTrigger("BuisAchter");
    }

    private void DecideAfterText(ChoiceManager.ChoiceEnum givenChoice, Character characterInfo, Sprite sprite)
    {
        string randomText = "";
        switch (givenChoice)
        {
            case ChoiceManager.ChoiceEnum.Pet:
                randomText = GetRandomPrompt(petText);
                CreateAfterText(randomText, characterInfo);
                break;
            case ChoiceManager.ChoiceEnum.Museum:
                randomText = GetRandomPrompt(museumText);
                CreateAfterText(randomText, characterInfo);
                break;
            case ChoiceManager.ChoiceEnum.Experiments:
                randomText = GetRandomPrompt(experimentText);
                CreateAfterText(randomText, characterInfo);
                break;
            case ChoiceManager.ChoiceEnum.Breeding:
                randomText = GetRandomPrompt(breedingText);
                CreateAfterText(randomText, characterInfo);
                break;
            case ChoiceManager.ChoiceEnum.MakeFood:
                randomText = GetRandomPrompt(makeFoodText);
                CreateAfterText(randomText, characterInfo);
                break;
            case ChoiceManager.ChoiceEnum.Skip:
                randomText = GetRandomPrompt(skipText);
                CreateAfterText(randomText, characterInfo);
                break;
        }

        currentCharacterInfo.SetAfterSprite(sprite);
    }

    string GetRandomPrompt(List<string> texts)
    {
        return texts[Random.Range(0, texts.Count)];
    }

    void CreateAfterText(string text, Character characterInfo)
    {
        characterInfo.afterText = "";
        char[] chars = text.ToCharArray();

        for (int i = 0; i < chars.Length; i++)
        {
            if (chars[i] == '<')
            {
                if (chars[i + 1] == 'n')
                {
                    characterInfo.afterText += characterInfo.nam;
                }
                else if (chars[i + 1] == 'a')
                {
                    characterInfo.afterText += characterInfo.age;
                }

                i += 2;
                continue;
            }
            else
            {
                characterInfo.afterText += chars[i];
            }
        }
    }
}
