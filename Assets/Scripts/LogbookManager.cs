using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LogbookManager : MonoBehaviour
{
    private int page = 0; // current page number
    private List<string[]> logs = new();

    // new logbook
    [SerializeField] private TextMeshProUGUI caseNumber;
    [SerializeField] private TextMeshProUGUI personalInformation;
    [SerializeField] private TextMeshProUGUI subjectBiography;
/*    [SerializeField] private TextMeshProUGUI caseResults;
    [SerializeField] private TextMeshProUGUI documentResults;*/

    public void LogCharacter(Character characterInfo)
    {
        string[] log = new string[3]; // 5 textboxes 

        log[0] = logs.Count.ToString();
        log[1] = characterInfo.nam + "\n" + characterInfo.age + "\n" + characterInfo.gender + "\n" + characterInfo.nationality;
        log[2] = characterInfo.description;
/*        log[3] = characterInfo.choice + "\n" + characterInfo.timeElapsed;
        log[4] = "Deze tekst bestaat nog niet.";*/
        logs.Add(log);

        LastPage();
    }

    public void NextPage() { page = Mathf.Min(logs.Count - 1, page + 1); UpdatePage(); Debug.Log(page); }

    public void PreviousPage() { page = Mathf.Max(page - 1, 0); UpdatePage(); Debug.Log(page); }

    public void LastPage() { page = logs.Count - 1; UpdatePage(); }

    public void UpdatePage()
    {
        caseNumber.text          = logs[page][0]; 
        personalInformation.text = logs[page][1];
        subjectBiography.text    = logs[page][2];
/*        caseResults.text         = logs[page][3];
        documentResults.text     = logs[page][4]; */
    }

    public void SetPage(int page) { this.page = page; }
    public int GetPage() { return page; }
}
