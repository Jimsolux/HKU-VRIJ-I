using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class GameManager : MonoBehaviour 
{
    //References
    public static GameManager Instance;
    [SerializeField] CharacterManager characterManager;
    [SerializeField] ChoiceManager choiceManager;


    //Timer
    private float fourMinuteTimer = 240;
    private bool timerHasRunOut = false;
    //CharacterTimer
    [SerializeField] private float characterTimerLength;
    private float characterTimer;

    //Animaties 


    private void Awake()
    {
        Instance = this;
        SetCanChoice(true);
    }

    private void Update()
    {
        if (!timerHasRunOut) GameTimer();
    }

    public void SetCanChoice(bool theBool)
    {
        choiceManager.SetCanChoose(theBool);
    }

    #region Timers
    private void GameTimer()
    {
        fourMinuteTimer -= Time.deltaTime;
        if (fourMinuteTimer <= 0) timerHasRunOut = true;
    }

    
    private void ResetCharacterTimerLength()
    {
        characterTimer = characterTimerLength;
    }

    private void TimerCountDown()
    {
        if (choiceManager.canChoose)
        {
            characterTimer -= Time.deltaTime;
            if (characterTimer < 0)
            {
                //INVOKE NEXT CHARACTER
                characterManager.SetCharacterChoice(ChoiceManager.ChoiceEnum.Skip);
                characterManager.NextCharacter();
                ResetCharacterTimerLength();
            }
        }
    }
    #endregion
}
