using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour 
{
    public static GameManager Instance;
    [SerializeField] CharacterManager characterManager;

    private void Awake()
    {
        Instance = this;
    }

    public void PushChoice(ChoiceManager.ChoiceEnum choice)
    {
        characterManager.SetCharacterChoice(choice);
        characterManager.currentCharacterInfo.choice = choice.ToString();   //Sent value as string to scriptableObject
    }

}
