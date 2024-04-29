using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MonitorText : MonoBehaviour
{
    [SerializeField][TextArea] private string monitorText;
    [SerializeField] private float writeSpeed = .1f;

    [SerializeField] private int sizePersonData = 80;
    [SerializeField] private int sizeBiography = 92;

    private Text textObject;

    private void Start()
    {
        textObject = GetComponent<Text>();
        StartCoroutine(WriteText(monitorText));
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

    private IEnumerator WriteText(string text)
    {
        textObject.fontSize = sizePersonData;

        char[] characters = text.ToCharArray();

        string activeText = string.Empty; // this is needed to close the special text box if  inside one 

        bool rush = false; // skip wait time

        bool insideSize = false; // if inside a box where text is a different size
        bool insideBold = false; // if inside a box where text is bold

        ClearText();


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

    }
}
