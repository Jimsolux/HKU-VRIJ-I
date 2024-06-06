using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Mathematics;

public class LogbookManager : MonoBehaviour
{
    private bool opened = false; 
    private int page = 0; // current page number
    private List<string[]> logs = new();
    private List<Sprite[]> images = new();

    [Header("Buttons")]
    [SerializeField] private GameObject buttons;

    [SerializeField] private TextMeshProUGUI pageNumber; 

    [Header("Page objects")]
    [SerializeField] private GameObject leftSide, rightSide, leftPage, rightPage;
    [SerializeField] private GameObject leftPagePrefab, rightPagePrefab;
    [SerializeField] private GameObject pageTurnerLeft, pageTurnerRight;

    private KeepPagesAttached pageTurner;

    [Header("Information fields")]
    [SerializeField] private TextMeshProUGUI caseNumber;
    [SerializeField] private TextMeshProUGUI personalInformation;
    [SerializeField] private TextMeshProUGUI subjectBiography;
    [SerializeField] private TextMeshProUGUI caseResults;
    [SerializeField] private TextMeshProUGUI documentResults;

    [SerializeField] private Image imageBefore, imageAfter;

    private Animator logbookAnimator;

    private void Start()
    {
        logbookAnimator = GetComponent<Animator>();

        CloseLogbook();
    }

    private void Update()
    {
        if (pageTurner != null)
        {
            Debug.Log("IM ALIVE");
            if (!pageTurner.Turn())
                pageTurner = null;
        }
    }

    public void LogCharacter(Character characterInfo)
    {
        string[] currentLog = new string[5]; // 5 textboxes
        Sprite[] currentImages = new Sprite[2];

        currentLog[0] = logs.Count.ToString();
        currentLog[1] = characterInfo.nam + "\n" + characterInfo.age + "\n" + characterInfo.gender + "\n" + characterInfo.nationality;
        currentLog[2] = characterInfo.description;
        currentLog[3] = characterInfo.choice + "\n" + characterInfo.timeElapsed;
        currentLog[4] = characterInfo.afterText;

        currentImages[0] = characterInfo.imageBefore;
        currentImages[1] = characterInfo.imageAfter;

        logs.Add(currentLog);
        images.Add(currentImages);

        LastPage();
    }

    public void NextPage() 
    { 
        page = Mathf.Min(logs.Count - 1, page + 1);

        GameObject newLeftPage = Instantiate(leftPagePrefab, leftSide.transform); 
        GameObject newRightPage = Instantiate(rightPagePrefab, rightSide.transform);
        pageTurner = new KeepPagesAttached(newLeftPage, rightPage, leftPage);

        // set variables
        caseNumber          = newLeftPage.transform.Find("Case Number").GetComponent<TextMeshProUGUI>();
        personalInformation = newLeftPage.transform.Find("Personal Information").GetComponent<TextMeshProUGUI>();
        subjectBiography    = newLeftPage.transform.Find("Subject Biography").GetComponent<TextMeshProUGUI>();
        caseResults         = newRightPage.transform.Find("Case Results").GetComponent<TextMeshProUGUI>();
        documentResults     = newRightPage.transform.Find("Documented Results").GetComponent<TextMeshProUGUI>();

        imageBefore = newLeftPage.transform.Find("Picture").GetComponent<Image>();
        imageAfter = newRightPage.transform.Find("Picture").GetComponent<Image>();

        UpdatePage();

        leftPage = newLeftPage;
        rightPage = newRightPage;
    }

    public void PreviousPage() { page = Mathf.Max(page - 1, 0); UpdatePage(); }

    public void LastPage() { page = logs.Count - 1; UpdatePage(); }

    public void FirstPage() { page = 0; UpdatePage(); }

    private void UpdatePage()
    {
        if (logs.Count > 0)
        {
            caseNumber.text          = logs[page][0]; 
            personalInformation.text = logs[page][1];
            subjectBiography.text    = logs[page][2];
            caseResults.text         = logs[page][3];
            documentResults.text     = logs[page][4];

            imageBefore.sprite = images[page][0];
            imageAfter.sprite  = images[page][1];

            pageNumber.text = page.ToString();
        }
    }

    public void SetPage(int page) { this.page = page; }

    public int GetPage() { return page; }

    public void OpenLogbook()
    {
        if (!opened)
        {
            opened = true;

            leftPage.SetActive(true); 
            rightPage.SetActive(true);
            //buttons.SetActive(true);

            logbookAnimator.SetTrigger("Open");

            LastPage();

            DeskLamp.TurnOff(); 
        }
    }

    public void CloseLogbook()
    {
        if (opened)
        {
            opened = false;

            leftPage.SetActive(false);
            rightPage.SetActive(false);
            //buttons.SetActive(false);

            logbookAnimator.SetTrigger("Close");
        }
    }
}
