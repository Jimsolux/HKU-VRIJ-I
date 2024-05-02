using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Character")]
public class Character : ScriptableObject
{
    // This script will have - 

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
    //whatever hierkomt.

    //public enum ChoiceEnum
    //{
    //    Breeding,
    //    Museum,
    //    Experiments,
    //    Pet,
    //    MakeFood,
    //    Skip
    //}
    public ChoiceManager.ChoiceEnum activeChoice;


}
