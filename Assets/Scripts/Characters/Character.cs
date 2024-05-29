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

    public GameObject obj;

    //Animatie stuff
    public Animator animator;

    public ChoiceManager.ChoiceEnum activeChoice;


    public void SetCharacterChoice(ChoiceManager.ChoiceEnum givenChoice)
    {
        choice = givenChoice.ToString();
    }

    //After texts
    private string afterText = "Empty result text";

    private string petText1 = "";
    private string petText2 = "";

    private string museumText1 = "";
    private string museumText2 = "";

    private string experimentsText1 = "";
    private string experimentsText2 = "";

    private string breedingText1 = "";
    private string breedingText2 = "";

    private string makeFoodText1 = "";
    private string makeFoodText2 = "";

    private string skipText1 = "";
    private string skipText2 = "";

    private void DecideAfterText(ChoiceManager.ChoiceEnum givenChoice, string thisName)
    {
        petText1 = thisName + " was sent to become one of our pets. They have become a quite well-behaved homo-sapien, after some small adjustments and sterilization. We are greatful for their joyous and loyal presence. ";
        petText2 = thisName;

        museumText1 = thisName + " stands in the museum as a wonderfully educational piece of the strange human body. It will be preserved to be admired at for ages to come.";
        museumText2 = thisName;

        experimentsText1 = "We have had great joy in experimenting with " + thisName + ". Their body was able to keep up living throughout multiple tests, teaching us so much more about the human body and its' capabilities. You could say their new form is something of a work of art.";
        experimentsText2 = thisName;

        breedingText1 = thisName + " was succesfully transformed into a breeding unit. They now serve as a host for many future subjects, which will be useful for the continuation of their species.";
        breedingText2 = thisName;

        makeFoodText1 = thisName + " has become a wonderful savoury soup, made by the finest of our chefs. The back of the soup can reads: Savor the rich, velvety broth infused with a symphony of exotic spices and Earth-grown herbs. Every spoonful unveils the tender, juicy texture of expertly prepared human meat, elevated with subtle sweetness and umami richness. ";
        makeFoodText2 = "";


        skipText1 = thisName + " was skipped. Their bodily contents have been sent to one of our food production lines.";
        skipText2 = thisName;


        switch (givenChoice)
        {
            case ChoiceManager.ChoiceEnum.Pet:
                afterText = petText1;
                break;
            case ChoiceManager.ChoiceEnum.Museum:
                afterText = museumText1;
                break;
            case ChoiceManager.ChoiceEnum.Experiments:
                afterText = experimentsText1;
                break;
            case ChoiceManager.ChoiceEnum.Breeding:
                afterText = breedingText1;
                break;
            case ChoiceManager.ChoiceEnum.MakeFood:
                afterText = makeFoodText1;
                break;
            case ChoiceManager.ChoiceEnum.Skip:
                afterText = skipText1;
                break;
        }
    }

}
