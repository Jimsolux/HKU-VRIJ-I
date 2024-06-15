using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class LogbookManager : MonoBehaviour
{
    private bool opened = false;
    private int page = 0; // current page number
    public List<GameObject> leftPages = new List<GameObject>();
    public List<GameObject> rightPages = new List<GameObject>();
    public List<PageTurner> turners = new List<PageTurner>();

    [Header("Page objects")]
    [SerializeField] private GameObject leftPagePrefab, rightPagePrefab;
    [SerializeField] private Transform leftSide, rightSide;

    private Animator animator;

    [SerializeField] PlayableDirector outroCutscene;
    // playtest dingen 
    [SerializeField] private GameObject imageBefore;
    [SerializeField] private GameObject imageAfter;

    private void Start()
    {
        leftSide = this.transform.Find("Logbook Left");
        rightSide = this.transform.Find("Logbook Right");
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

    public bool GetLock() {  return lockLogbook; }
    public void LogCharacter(Character characterInfo)
    {
        GameObject newLeftPage = Instantiate(leftPagePrefab, leftSide.transform);
        GameObject newRightPage = Instantiate(rightPagePrefab, rightSide.transform);

        newLeftPage.transform.Find("Case Number").GetComponent<TextMeshProUGUI>().text =
            leftPages.Count.ToString();
        newLeftPage.transform.Find("Personal Information").GetComponent<TextMeshProUGUI>().text =
            characterInfo.nam + "\n" + characterInfo.age + "\n" + characterInfo.gender + "\n" + characterInfo.nationality;
        newLeftPage.transform.Find("Subject Biography").GetComponent<TextMeshProUGUI>().text =
            characterInfo.description;
        newRightPage.transform.Find("Case Results").GetComponent<TextMeshProUGUI>().text =
            characterInfo.choice + "\n" + characterInfo.timeElapsed;
        newRightPage.transform.Find("Documented Results").GetComponent<TextMeshProUGUI>().text =
            characterInfo.afterText;

        newLeftPage.transform.Find("Picture").GetComponent<Image>().sprite =
            characterInfo.imageBefore;
        newRightPage.transform.Find("Picture").GetComponent<Image>().sprite =
            characterInfo.imageAfter;

        AddPage(newLeftPage, newRightPage);

        LastPage();
    }

    private void AddPage(GameObject newLeftPage, GameObject newRightPage)
    {
        leftPages.Add(newLeftPage);
        rightPages.Add(newRightPage);
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
        rightPages[GetLogbookSize() - 1].transform.Find("Stamp").gameObject.SetActive(true);
    }

    public void NextPage()
    {
        ClosePage(page);
        page = Mathf.Min(leftPages.Count - 1, page + 1);
        OpenPage(page);
    }

    public void PreviousPage()
    {
        ClosePage(page);
        page = Mathf.Max(page - 1, 0);
        OpenPage(page);
    }

    public void FirstPage() { ClosePage(page); page = 0; OpenPage(page); }

    public void LastPage() { ClosePage(page); page = Mathf.Max(leftPages.Count - 1, 0); OpenPage(page); }

    public void ClosePage(int page)
    {
        if (leftPages.Count > 0)
        {
            leftPages[page].SetActive(false);
            rightPages[page].SetActive(false);
        }
    }

    private void OpenPage(int page)
    {
        if (leftPages.Count > 0)
        {
            leftPages[page].SetActive(true);
            rightPages[page].SetActive(true);
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
            {
                outroCutscene.Play();
            }
        }
    }
}
