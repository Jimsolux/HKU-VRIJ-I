using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;

public class MonitorText : MonoBehaviour
{
    public static MonitorText instance;

    private void Awake()
    {
        instance = this; 
    }

    [SerializeField][TextArea] private string monitorText;
    [SerializeField] private float writeSpeed = .1f;

    [SerializeField] private int sizePersonData = 80;
    [SerializeField] private int sizeBiography = 92;

    private Text textObject;
    [SerializeField] private Text bottomTextObject;
    [SerializeField] private GameObject continueButton;

    [Header("Start dialogue")]
    [SerializeField][TextArea] private string[] startText;
    private bool inStart = true;
    private string storedDialogue;

    private void Start()
    {
        textObject = GetComponent<Text>();
        //StartDialogue();
    }

    public void ClearText()
    {
        monitorText = "";
        HandleUI();
    }

    public void StoreText(string text)
    {
        storedDialogue = text;
    }

    public void SetText(string text)
    {
        StartCoroutine(WriteText(text));
        Debug.Log("started with; " + text);
    }

    public void SetBottomText(string choice)
    {
        bottomTextObject.text = "Last person was tasked to:" + choice + "\n Find more in the logbook!";
    }

    public string GetText() { return monitorText; }

    public void HandleUI()
    {
        textObject.text = monitorText;

        if(inStart && !writing)
        {
            continueButton.SetActive(true);
        }
        else
        {
            continueButton.SetActive(false);
        }
    }

    public string BioToString(Character characterInfo)
    {
        string str = "Name: "        + characterInfo.nam + "\n"
              + "Age: "         + characterInfo.age + "\n"
              + "Gender: "      + characterInfo.gender + "\n"
              + "Nationality: " + characterInfo.nationality + "\n\n"
              + "Description: " + characterInfo.description + "\n\n";

        return str;
    }
    /*
    public IEnumerator StartSequence()
    {
        CameraMovement camMovement = Camera.main.GetComponent<CameraMovement>();

        camMovement.SetLock(true);
        for(int i = 0; i < startText.Length; i++)
        {
            yield return new WaitForSeconds(4);
        }
        camMovement.SetLock(false);
        StartCoroutine(WriteText(""));
    }*/

    private int startDialogueCurrent = 0;
    public void StartDialogue()
    {
        inStart = true;
        CameraMovement camMovement = Camera.main.GetComponent<CameraMovement>();

        camMovement.SetLock(true);
        try
        {
            StartCoroutine(WriteText(startText[startDialogueCurrent], writeSpeed * 2));
        }
        catch 
        { 
            EndStartDialogue();
        }
    }

    private void EndStartDialogue()
    {
        inStart = false;

        CameraMovement camMovement = Camera.main.GetComponent<CameraMovement>();

        camMovement.SetLock(false);
        StartCoroutine(WriteText(storedDialogue));
    }

    public void ContinueStartDialogue()
    {
        startDialogueCurrent++;
        StartDialogue();
    }

    private IEnumerator WriteText(string text)
    {
        yield return new WaitForEndOfFrame();
        StartCoroutine(WriteText(text, writeSpeed));
    }

    private bool writing = false;
    private IEnumerator WriteText(string text, float writeSpeed)
    {
        textObject.fontSize = sizePersonData;

        char[] characters = text.ToCharArray();

        string activeText = string.Empty; // this is needed to close the special text box if  inside one 

        bool rush = false; // skip wait time

        bool insideSize = false; // if inside a box where text is a different size
        bool insideBold = false; // if inside a box where text is bold

        ClearText();

        writing = true;
        for (int i = 0; i < characters.Length; i++)
        {
            if (insideSize && characters[i].ToString() == "x") activeText += sizeBiography; // makes it easier to change the text of the biography in code
            else activeText += characters[i];

            try
            {
                if (characters[i].ToString() == "<")
                {
                    rush = true;
                    switch (characters[i + 1].ToString())
                    {
                        case "s": insideSize = true; break;
                        case "b": insideBold = true; break;
                        case "/":
                            switch (characters[i + 2].ToString())
                            {
                                case "s": insideSize = false; break;
                                case "b": insideBold = false; break;
                            }
                            break;
                    }
                }
                else if (characters[i - 1].ToString() == ">")
                {
                    rush = false;
                }

            }
            catch { } // if the index trying to be found is outside list of array

            if (!rush) yield return new WaitForSeconds(writeSpeed);

            monitorText = activeText;

            if (insideBold) monitorText += "</b>";
            if (insideSize) monitorText += "</size>";

            HandleUI();
        }

        writing = false;
        HandleUI();
    }
}
