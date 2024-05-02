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
    public Character currentCharacterInfo;

    [SerializeField] private Transform characterTransform;
 
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
    }

    public Character GetRandomCharacterIndex(List<Character> charactersLeft)
    {
        int randomIndex = Mathf.RoundToInt(Random.Range(0, charactersLeft.Count));
        Character characterInfo = charactersLeft[randomIndex];
        charactersLeft.RemoveAt(randomIndex);

        return characterInfo;
    }

    // location to instantiate, etc?
    public GameObject InstantiateRandomCharacter(Character randomCharacterInfo)
    {
        GameObject randomCharacterObj = Instantiate(randomCharacterInfo.obj, characterTransform.position, Quaternion.identity);

        return randomCharacterObj;
    }

    public void NextCharacter()
    {
        //lm.LogCharacter(currentCharacterInfo, mt.GetText()); // log previous character
        currentCharacterInfo = GetRandomCharacterIndex(charactersLeft);
        currentCharacterObj = InstantiateRandomCharacter(currentCharacterInfo);
        mt.SetText(mt.BioToString(currentCharacterInfo));
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
}
