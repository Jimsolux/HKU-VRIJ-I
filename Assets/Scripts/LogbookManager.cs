using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogbookManager : MonoBehaviour
{
    // UI
    public List<Character> characters = new List<Character>();

    public void LogCharacter(Character characterInfo)
    {
        characters.Add(characterInfo);

        characterInfo.age = 0;
    }
}
