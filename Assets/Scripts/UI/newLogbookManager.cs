using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class newLogbookManager : MonoBehaviour
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

    [Header("Information fields")]
    [SerializeField] private TextMeshProUGUI caseNumber, personalInformation, subjectBiography, caseResults, documentResults;

    [SerializeField] private Image imageBefore, imageAfter;

    [SerializeField] private PageTurner pageTurner;

    [Header("Transformations")]
    //[SerializeField] private Transform transformOpened, transformClosed;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();

        CloseLogbook();
    }

    private void Update()
    {
        if (pageTurner != null)
        {
            if (pageTurner.Turn())
            {
                pageTurner = null;
            }
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

    bool lastCharSorted = false;
    public void LogLastCharacter()
    {
        string[] currentLog = new string[5]; // 5 textboxes
        Sprite[] currentImages = new Sprite[2];

        currentLog[0] = logs.Count.ToString();
        currentLog[1] = "Sorting Unit" + "\n" + "Unknown" + "\n" + "Unknown" + "\n" + "Dutch"; // vervang dutch na test voor school

        currentLog[2] = "The sorting unit has aided our goal to better understand the Homo sapiens. After sorting various members of their" +
            " own species, their worth became less than adequate.";
        currentLog[3] = "N/A\nN/A";
        currentLog[4] = "N/A";

        // hier moet webcam screenshot komen
        currentImages[0] = null;
        currentImages[1] = null;

        logs.Add(currentLog);
        images.Add(currentImages);

        lastCharSorted = true;
        FirstPage();
    }

    /*    public void NextPage() 
        { 
            page = Mathf.Min(logs.Count - 1, page + 1);

            // 1.1 spawn two new pages
            GameObject newLeftPage = Instantiate(leftPagePrefab, leftSide.transform);
            GameObject newRightPage = Instantiate(rightPagePrefab, rightSide.transform);

            // 1.2 set correct starter rotations 
            newLeftPage.transform.localRotation = Quaternion.Euler(new Vector3(-180, 0, -90));
            newRightPage.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));

            // 2.1 get the text objects
            caseNumber =          newLeftPage.transform.GetChild(3).GetComponent<TextMeshProUGUI>();
            personalInformation = newLeftPage.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
            subjectBiography =    newLeftPage.transform.GetChild(4).GetComponent<TextMeshProUGUI>();
            caseResults =         newRightPage.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
            documentResults =     newRightPage.transform.GetChild(3).GetComponent<TextMeshProUGUI>();

            imageBefore = newLeftPage.transform.GetChild(0).GetComponent<Image>();
            imageAfter = newRightPage.transform.GetChild(0).GetComponent<Image>();

            // 2.2 set the correct values to each page
            UpdatePage();

            // 3. move pages (page turner w/ direction?)
            pageTurner = new PageTurner(PageTurner.TurnDirection.Right, newLeftPage, leftPage, rightPage);

            leftPage = newLeftPage;
            rightPage = newRightPage;
        }*/

    public void NextPage() { page = Mathf.Min(logs.Count - 1, page + 1); UpdatePage(); }

    public void PreviousPage() { page = Mathf.Max(page - 1, 0); UpdatePage(); }

    public void FirstPage() { page = 0; UpdatePage(); }

    public void LastPage() { page = logs.Count - 1; UpdatePage(); }

    private void UpdatePage()
    {
        if (logs.Count > 0)
        {
            caseNumber.text = logs[page][0];
            personalInformation.text = logs[page][1];
            subjectBiography.text = logs[page][2];
            caseResults.text = logs[page][3];
            documentResults.text = logs[page][4];

            imageBefore.sprite = images[page][0];
            imageAfter.sprite = images[page][1];

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

            if (lastCharSorted) FirstPage();
            else LastPage();

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
