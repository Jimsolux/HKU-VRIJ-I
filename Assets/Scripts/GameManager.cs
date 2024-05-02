using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class GameManager : MonoBehaviour 
{
    public static GameManager Instance;
    [SerializeField] CharacterManager characterManager;
    //Timer
    private float fourMinuteTimer = 240;
    private bool timerHasRunOut = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (!timerHasRunOut) GameTimer();
    }

    private void GameTimer()
    {
        fourMinuteTimer -= Time.deltaTime;
        if (fourMinuteTimer <= 0) timerHasRunOut = true;
    }

}
