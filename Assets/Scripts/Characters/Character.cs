using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character")]
public class Character : ScriptableObject
{
    // This script will have - 

    public GameObject characterObject;
    public int age;
    public string characterName = "New Character";
    public string description = "New Description";
    public string gender = "New Sex"; 

    //Animatie stuff
    public Animator animator;
    //whatever hierkomt.




}
