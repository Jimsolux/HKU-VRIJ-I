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
    private Character currentCharacterInfo;

    [SerializeField] private Transform characterTransform;

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
        }

    }

    private void InitializeCharacterManager()
    {
        charactersLeft = new List<Character>(characters);
        FirstCharacter();
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
        GameObject randomCharacterObj = Instantiate(randomCharacterInfo.obj, characterTransform.position, Quaternion.identity);

        return randomCharacterObj;
    }

    public void NextCharacter()
    {
        Character potentialNextCharacterInfo = GetRandomCharacterIndex(charactersLeft);

        if (potentialNextCharacterInfo != null)
        {
            //lm.LogCharacter(currentCharacterInfo, mt.GetText()); // log previous character
            currentCharacterInfo = potentialNextCharacterInfo;
            currentCharacterObj = InstantiateRandomCharacter(currentCharacterInfo);
            mt.SetText(mt.BioToString(currentCharacterInfo));
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
        NextCharacter();
    }

}
