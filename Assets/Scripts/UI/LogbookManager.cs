using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class LogbookManager : MonoBehaviour
{
    private bool opened = false;
    private bool canTurn = true;
    private int page = 0; 
    public List<GameObject> leftPages = new List<GameObject>();
    public List<GameObject> rightPages = new List<GameObject>();
    public List<GameObject[]> pages = new List<GameObject[]>();

    [Header("Page objects")]
    [SerializeField] private GameObject leftPagePrefab, rightPagePrefab;
    [SerializeField] private Transform leftSide, rightSide;

    private Animator animator;

    [SerializeField] private PlayableDirector outroCutscene;
    [SerializeField] private LogbookTurnPageAudio turnPageAudio;

    // playtest dingen 
    [SerializeField] private GameObject imageBefore;
    [SerializeField] private GameObject imageAfter;

    public enum Page
    {
        Left, Right
    }

    private void Start()
    {
        leftSide = transform.Find("Logbook Left");
        rightSide = transform.Find("Logbook Right");
        animator = GetComponent<Animator>();

        imageBefore.SetActive(false);
        imageAfter.SetActive(false);

        CloseLogbook();
    }

    private bool lockLogbook;
    private void Update()
    {
        if (opened)
        {
            if (lastCharSorted && page != leftPages.Count - 1) lockLogbook = true;
            else lockLogbook = false;

            // input
            if (Input.GetKeyDown(KeyCode.A))
            {
                PreviousPage();
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                NextPage();
            }     

            /*            // turning pages animation 
                        if (turners.Count > 0)
                            for (int i = 0; i < turners.Count; i++)
                                if (turners[i].Turn())
                                    turners.Remove(turners[i]);*/

            if (lastCharSorted && page == GetLogbookSize() - 1)
            {
                imageBefore.SetActive(true);
                imageAfter.SetActive(true);
            }
            else
            {
                imageBefore.SetActive(false);
                imageAfter.SetActive(false);
            }
        }
    }

    public bool GetLock() { return lockLogbook; }
    public void LogCharacter(Character characterInfo)
    {
        GameObject newLeftPage = Instantiate(leftPagePrefab, leftSide.transform);
        GameObject newRightPage = Instantiate(rightPagePrefab, rightSide.transform);

        newLeftPage.transform.Find("Case Number").GetComponent<TextMeshProUGUI>().text =
            pages.Count.ToString();
        newLeftPage.transform.Find("Personal Information").GetComponent<TextMeshProUGUI>().text =
            characterInfo.nam + "\n" + characterInfo.age + "\n" + characterInfo.gender + "\n" + characterInfo.nationality;
        newLeftPage.transform.Find("Subject Biography").GetComponent<TextMeshProUGUI>().text =
            characterInfo.description;
        newRightPage.transform.Find("Case Results").GetComponent<TextMeshProUGUI>().text =
            characterInfo.choice + "\n" + characterInfo.timeElapsed;
        newRightPage.transform.Find("Documented Results").GetComponent<TextMeshProUGUI>().text =
            characterInfo.afterText;
        newRightPage.transform.Find("Page Number").GetComponent<TextMeshProUGUI>().text =
            pages.Count.ToString();  

        newLeftPage.transform.Find("Picture").GetComponent<Image>().sprite =
            characterInfo.imageBefore;
        newRightPage.transform.Find("Picture").GetComponent<Image>().sprite =
            characterInfo.imageAfter;

        AddPage(newLeftPage, newRightPage);

        LastPage();
    }

    private void AddPage(GameObject newLeftPage, GameObject newRightPage)
    {
        GameObject[] pagePair = new GameObject[2];
        pagePair[0] = newLeftPage;
        pagePair[1] = newRightPage;
    }

    bool lastCharSorted = false;
    public void LogLastCharacter()
    {
        GameObject newLeftPage = Instantiate(leftPagePrefab, leftSide.transform);
        GameObject newRightPage = Instantiate(rightPagePrefab, rightSide.transform);

        newLeftPage.transform.Find("Case Number").GetComponent<TextMeshProUGUI>().text =
            leftPages.Count.ToString();
        newLeftPage.transform.Find("Personal Information").GetComponent<TextMeshProUGUI>().text =
            "Sorting Unit" + "\n" + "Unknown" + "\n" + "Unknown" + "\n" + "Dutch";
        newLeftPage.transform.Find("Subject Biography").GetComponent<TextMeshProUGUI>().text =
            "The sorting unit has aided our goal to better understand the Homo sapiens. After sorting various members of their" +
            " own species, their worth became less than adequate.";
        newRightPage.transform.Find("Case Results").GetComponent<TextMeshProUGUI>().text =
            "N/A\nN/A";
        newRightPage.transform.Find("Documented Results").GetComponent<TextMeshProUGUI>().text =
            "N/A";

        newLeftPage.transform.Find("Picture").GetComponent<Image>().sprite = null;
        newRightPage.transform.Find("Picture").GetComponent<Image>().sprite = null;

        AddPage(newLeftPage, newRightPage);

        lastCharSorted = true;
        FirstPage();
    }

    public void ActivateStamp()
    {
        try
        {
            pages[pages.Count - 1][(int)Page.Right].transform.Find("Stamp").gameObject.SetActive(true);
        }
        catch { }
    }

    public void NextPage()
    {
        turnPageAudio.Play();

        ClosePage(page);
        page = Mathf.Min(pages.Count - 1, page + 1);
        OpenPage(page);
    }

    public void PreviousPage()
    {
        turnPageAudio.Play();

        ClosePage(page);
        page = Mathf.Max(page - 1, 0);
        OpenPage(page);
    }

    public void FirstPage() { ClosePage(page); page = 0; OpenPage(page); }

    public void LastPage() { ClosePage(page); page = Mathf.Max(pages.Count - 1, 0); OpenPage(page); }

    public void ClosePage(int page)
    {
        if (pages.Count > 0)
        {
            pages[page][(int)Page.Left].SetActive(false); 
            pages[page][(int)Page.Right].SetActive(false);
        }
    }

    private void OpenPage(int page)
    {
        if (pages.Count > 0)
        {
            pages[page][(int)Page.Left].SetActive(true);
            pages[page][(int)Page.Right].SetActive(true);
        }
    }

    public int GetPage() { return page; }

    public int GetLogbookSize() { return leftPages.Count; }

    public void OpenLogbook()
    {
        if (!opened)
        {
            opened = true;

            if (lastCharSorted) FirstPage();
            else LastPage();

            animator.SetTrigger("Open");

            DeskLamp.TurnOff();
        }
    }

    public void CloseLogbook()
    {
        if (opened)
        {
            opened = false;

            ClosePage(page);

            animator.SetTrigger("Close");

            // outro cutscene
            if (lastCharSorted)
                outroCutscene.Play();
        }
    }
}
