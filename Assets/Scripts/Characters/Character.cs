using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Character")]
public class Character : ScriptableObject
{
    public string nam = "Enter Name";
    public int age;
    public string gender = "Enter Gender (either M or W)";
    public string nationality = "Enter Nationality";
    public string description = "Enter Description";

    public string choice = "Enter Choice";
    public Image image;

    public GameObject obj;

    //Animatie stuff
    public Animator animator;

    public ChoiceManager.ChoiceEnum activeChoice;


    public void SetCharacterChoice(ChoiceManager.ChoiceEnum givenChoice)
    {
        choice = givenChoice.ToString();
    }

}
