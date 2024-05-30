using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    [SerializeField] private GameObject leftSide, leftPage, rightPage;

    [Header("Information fields")]
    [SerializeField] private TextMeshProUGUI caseNumber;
    [SerializeField] private TextMeshProUGUI personalInformation;
    [SerializeField] private TextMeshProUGUI subjectBiography;
    [SerializeField] private TextMeshProUGUI caseResults;
    [SerializeField] private TextMeshProUGUI documentResults;

    [SerializeField] private Image imageBefore, imageAfter;

    [Header("Transformations")]
    //[SerializeField] private Transform transformOpened, transformClosed;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();

        CloseLogbook();
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

    public void NextPage() { page = Mathf.Min(logs.Count - 1, page + 1); UpdatePage(); Debug.Log("hi"); }

    public void PreviousPage() { page = Mathf.Max(page - 1, 0); UpdatePage(); Debug.Log("bye"); }

    public void LastPage() { page = logs.Count - 1; UpdatePage(); }

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

    /*
     * This code is redundant, using new system now. 
    private void SetOpenPosition()
    {
        transform.localPosition = transformOpened.localPosition;
        transform.localRotation = transformOpened.localRotation;
        leftSide.transform.localRotation = Quaternion.Euler(0, 180, 90);
    }

    private void SetClosedPosition()
    {
        transform.localPosition = transformClosed.localPosition;
        transform.localRotation = transformClosed.localRotation;
        leftSide.transform.localRotation = Quaternion.Euler(0, 0, 90);
    }
    */ 
    public void OpenLogbook()
    {
        if (!opened)
        {
            opened = true;

            leftPage.SetActive(true); 
            rightPage.SetActive(true);
            //buttons.SetActive(true);

            //SetOpenPosition();
            animator.SetTrigger("Open");

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

            //SetClosedPosition();
            animator.SetTrigger("Close");
        }
    }
}
