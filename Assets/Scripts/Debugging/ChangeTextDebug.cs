using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeTextDebug : MonoBehaviour
{
    private MonitorText monitor;

    [SerializeField][TextArea] private string text;

    private void Start()
    {
        monitor = GetComponent<MonitorText>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            monitor.SetText(text);
        }
    }
}
