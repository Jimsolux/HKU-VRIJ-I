using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    [SerializeField] LogbookManager logbookManager;

    [SerializeField] private List<Character> characters = new List<Character>();
    [HideInInspector] public List<Character> charactersLeft;

    public GameObject currentCharacterObj;
    public Character currentCharacterInfo;

    private void Start()
    {
        InitializeCharacterManager();
    }

    private void InitializeCharacterManager()
    {
        charactersLeft = characters;
    }

    public Character GetRandomCharacterIndex(List<Character> charactersLeft)
    {
        int randomIndex = Mathf.RoundToInt(Random.Range(0, charactersLeft.Count + 1));
        Character characterInfo = charactersLeft[randomIndex];
        charactersLeft.RemoveAt(randomIndex);

        return characterInfo;
    }

    // location to instantiate, etc?
    public GameObject InstantiateRandomCharacter(Character randomCharacterInfo)
    {
        GameObject randomCharacterObj = Instantiate(randomCharacterInfo.obj);

        return randomCharacterObj;
    }

    public void NextCharacter()
    {
        // previous character play animation, play sfx, finishing up with the previous character
        // placing info in logbook

        currentCharacterInfo = GetRandomCharacterIndex(charactersLeft);
        currentCharacterObj = InstantiateRandomCharacter(currentCharacterInfo);

        // next character play animation
        // monitor text next character 
    }
}
