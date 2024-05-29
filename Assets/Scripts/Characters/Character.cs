using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Character")]
public class Character : ScriptableObject
{
    public string nam =         "Enter Name";
    public int age;
    public string gender =      "Enter Gender";
    public string nationality = "Enter Nationality";
    public string description = "Enter Description";

    public string choice =      "Enter Choice";
    public float timeElapsed = 0.0f;
    public Sprite imageBefore;
    public Sprite imageAfter;

    public string afterText = "Empty result text";

    public GameObject obj;

    //Animatie stuff
    public Animator animator;

    public ChoiceManager.ChoiceEnum activeChoice;


    public void SetCharacterChoice(ChoiceManager.ChoiceEnum givenChoice)
    {
        choice = givenChoice.ToString();
    }
}
