using System.Collections;
using TMPro;
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
    [SerializeField] private bool hasStart;
    [SerializeField][TextArea] private string[] startText;
    private bool inStart = true;
    [SerializeField] private GameObject popupBox;

    [SerializeField] private TextMeshProUGUI[] textObjects = new TextMeshProUGUI[3];

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip ejectTyping; 

    private int tab;

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

    public void SetText(string text)
    {
        StartCoroutine(WriteText(text));
        Debug.Log("started with; " + text);
    }

    public string GetText() { return monitorText; }

    public void HandleUI()
    {
        textObject.text = monitorText;

        if (hasStart)
        {
            if (inStart && !writing)
            {
                continueButton.SetActive(true);
            }
            else
            {
                continueButton.SetActive(false);
            }
        }
    }

    public string BioToString(Character characterInfo)
    {
        string str = "Name: " + characterInfo.nam + "\n"
              + "Age: " + characterInfo.age + "\n"
              + "Gender: " + characterInfo.gender + "\n"
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
        if (hasStart)
        {
            inStart = true;
            CameraMovement camMovement = Camera.main.GetComponent<CameraMovement>();

            camMovement.SetLock(true);
            try
            {
                StartCoroutine(WriteText(startText[startDialogueCurrent], writeSpeed * 4));
            }
            catch
            {
                EndStartDialogue();
            }
        }
    }

    private void EndStartDialogue()
    {
        inStart = false;

        CameraMovement camMovement = Camera.main.GetComponent<CameraMovement>();

        camMovement.SetLock(false);
        try
        {
            popupBox.SetActive(false);
            MonitorUI.instance?.Unlock();
        }
        catch { }
    }

    public void ContinueStartDialogue()
    {
        startDialogueCurrent++;
        StartDialogue();
    }

    public IEnumerator WriteText(string text)
    {
        yield return new WaitForEndOfFrame();
        StartCoroutine(WriteText(text, writeSpeed));
    }

    private bool writing = false;
    private IEnumerator WriteText(string text, float writeSpeed)
    {
        textObject.fontSize = sizePersonData;

        if (text == null) text = string.Empty;

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

            // audio
            if (!audioSource.isPlaying)
            {
                audioSource.pitch = Random.Range(0.95f, 1.05f);
                audioSource.Play();
            }

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
        //audioSource.Stop();
        writing = false;

        HandleUI();
    }

    public void SwitchTab(int i)
    {
        if (i >= 0 && i < textObjects.Length)
        {
            textObjects[i].enabled = false;
            tab = i;
            textObjects[i].enabled = true;
        }
    }

    public int GetTab() { return tab; }
}
