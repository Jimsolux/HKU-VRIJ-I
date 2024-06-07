using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class WebcamEvent : MonoBehaviour
{
    [SerializeField] private GameObject logbookObject;
    [SerializeField] private GameObject monitorObject;
    private LogbookManager logbookManager;

    private void Start()
    {
        logbookManager = logbookObject.GetComponent<LogbookManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            LogbookEvent();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            logbookManager.LogLastCharacter();
        }
    }

    public void LogbookEvent()
    {
        logbookManager.LogLastCharacter();

        int pages = logbookManager.GetLogbookSize();
        Image image1 = logbookManager.leftPages[pages - 1].transform.Find("Image").GetComponent<Image>();
        Image image2 = logbookManager.rightPages[pages - 1].transform.Find("Image").GetComponent<Image>();
        DisplayWebCam.DisplayUI(image1);
        DisplayWebCam.DisplayUI(image2);
    }
}
