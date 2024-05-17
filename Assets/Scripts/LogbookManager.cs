using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class LogbookManager : MonoBehaviour
{
    private int page = 0; // current page number
    private List<string> logs = new(); // all logs currently made 

    [SerializeField] private TextMeshProUGUI textObject;

    public void LogCharacter(Character characterInfo, string currentInfo = "")
    {
        string additionalInfo = characterInfo.choice;
        Debug.Log(currentInfo + additionalInfo);
        logs.Add(currentInfo + "Chose: " + additionalInfo);
        // we have to add images aswell as the result of your choice

        LastPage();
    }

    public void NextPage() { page = Mathf.Min(logs.Count - 1, page + 1); UpdatePage(); }

    public void PreviousPage() { page = Mathf.Max(page - 1, 0); UpdatePage(); }

    public void LastPage() { page = logs.Count - 1; UpdatePage(); }

    public void UpdatePage(){ textObject.text = logs[page]; }
}
