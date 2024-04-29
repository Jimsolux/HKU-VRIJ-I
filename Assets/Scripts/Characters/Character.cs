using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character")]
public class Character : ScriptableObject
{
    // This script will have - 

    public string nam = "Enter Name";
    public int age;
    public string gender = "Enter Gender (either M or W)";
    public string nationality = "Enter Nationality";
    public string description = "Enter Description";

    public GameObject obj;

    //Animatie stuff
    public Animator animator;
    //whatever hierkomt.




}
