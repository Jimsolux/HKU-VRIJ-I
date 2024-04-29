using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LogbookManager : MonoBehaviour
{
    private int page = 0; // current page number
    private List<string> logs; // all logs currently made 

    // UI
    public List<Character> characters = new List<Character>();
    private List<TextMeshProUGUI> textObjects = new List<TextMeshProUGUI>();

    public void LogCharacter(Character characterInfo)
    {
        logs.Add("Name: "        + characterInfo.nam + "/n"
               + "Age: "         + characterInfo.age + "/n"
               + "Gender: "      + characterInfo.gender + "/n"
               + "Nationality: " + characterInfo.nationality + "/n/n"
               + "Description: " + characterInfo.description);

        // we have to add images aswell as the result of your choice
    }

    public void NextPage() { page = Mathf.Min(logs.Count - 1, page + 1); }
    public void PreviousPage() { page = Mathf.Max(0, page + 1); }

    public void UpdatePage()
    {

    }
}
