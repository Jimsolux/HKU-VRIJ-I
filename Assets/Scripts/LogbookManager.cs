using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LogbookManager : MonoBehaviour
{
    private int page = 0; // current page number
    private List<string> logs; // all logs currently made 

    [SerializeField] private TextMeshProUGUI textObject;

    public void LogCharacter(Character characterInfo, string currentInfo)
    {
        string additionalInfo = characterInfo.choice;
        logs.Add(currentInfo + "Choice: <b>" + additionalInfo);
        // we have to add images aswell as the result of your choice
    }

    public void NextPage() { page = Mathf.Min(logs.Count - 1, page + 1); }

    public void PreviousPage() { page = Mathf.Max(page - 1, 0); }

    public void UpdatePage(){ textObject.text = logs[page]; }
}
