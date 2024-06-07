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

    Renderer renderer1;
    Renderer renderer2;

    private void Start()
    {
        logbookManager = logbookObject.GetComponent<LogbookManager>();
        renderer1 = transform.Find("WebcamTextureBefore").GetComponent<Renderer>();
        renderer2 = transform.Find("WebcamTextureAfter").GetComponent<Renderer>();
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

        DisplayWebCam.DisplayLogbookImages(renderer1, renderer2);
    }
}
