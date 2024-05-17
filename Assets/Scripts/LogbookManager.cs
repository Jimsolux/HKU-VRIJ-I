using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LogbookManager : MonoBehaviour
{
    private int page = 0; // current page number
    private List<string[]> logs = new();
    private List<Sprite[]> images = new();

    // new logbook
    [SerializeField] private TextMeshProUGUI caseNumber;
    [SerializeField] private TextMeshProUGUI personalInformation;
    [SerializeField] private TextMeshProUGUI subjectBiography;
    [SerializeField] private TextMeshProUGUI caseResults;
    [SerializeField] private TextMeshProUGUI documentResults;

    [SerializeField] private Image imageBefore;
    [SerializeField] private Image imageAfter;

    public void LogCharacter(Character characterInfo)
    {
        string[] currentLog = new string[5]; // 5 textboxes
        Sprite[] currentImages = new Sprite[2];

        currentLog[0] = logs.Count.ToString();
        currentLog[1] = characterInfo.nam + "\n" + characterInfo.age + "\n" + characterInfo.gender + "\n" + characterInfo.nationality;
        currentLog[2] = characterInfo.description;
        currentLog[3] = characterInfo.choice + "\n" + characterInfo.timeElapsed;
        currentLog[4] = "Deze tekst bestaat nog niet.";

        currentImages[0] = characterInfo.imageBefore;
        currentImages[1] = characterInfo.imageAfter;

        logs.Add(currentLog);
        images.Add(currentImages);

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
        caseResults.text         = logs[page][3];
        documentResults.text     = logs[page][4];

        imageBefore.sprite = images[page][0];
        imageAfter.sprite  = images[page][1];
    }

    public void SetPage(int page) { this.page = page; }

    public int GetPage() { return page; }
}
