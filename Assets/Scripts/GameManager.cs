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
    private bool mainTimerIsOff = true;
    private bool charTimerIsOff = true;
    //CharacterTimer
    [SerializeField] private float characterTimerLength;
    private float characterTimer;

    //Animaties 

    private void Awake()
    {
        Instance = this;
        //SetCanChoice(true);
        ResetCharacterTimerLength();
    }

    private void FixedUpdate()
    {
        if (!mainTimerIsOff) GameTimer();   // Telt de gameTimer af
        if (!charTimerIsOff) CharacterChoiceTimer(); // Telt de Chartimer af
    }


    #region Timers
    private void GameTimer()
    {
        fourMinuteTimer -= Time.deltaTime;
        if (fourMinuteTimer <= 0) mainTimerIsOff = true;
    }

    
    private void ResetCharacterTimerLength()
    {
        characterTimer = characterTimerLength;
    }

    private void CharacterChoiceTimer()
    {
        if (choiceManager.canChoose)
        {
            characterTimer -= Time.deltaTime;
            if (characterTimer <= 0)
            {
                characterManager.SetCharacterChoice(ChoiceManager.ChoiceEnum.Skip);
                //characterManager.NextCharacter();
                charTimerIsOff = true;
                ResetCharacterTimerLength();
            }
        }
    }
    #endregion

    #region Call From Animator
    public void SetCanChoice(bool theBool)
    {
        choiceManager.SetCanChoose(theBool);

    }

    public void PrepareNextCharacter() // Call from animator
    {
        characterManager.NextCharacter();
        //charTimerIsOff = false; // Turn on Character Timer.
    }

    public void StartMainTimer()
    {
        mainTimerIsOff = false;
    }

    public void StartCharTimer()
    {
        charTimerIsOff = false;
    }

    #endregion
}

