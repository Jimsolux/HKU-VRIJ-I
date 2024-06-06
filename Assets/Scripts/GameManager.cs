using UnityEngine;
using UnityEngine.Playables;

public class GameManager : MonoBehaviour
{
    //References
    public static GameManager Instance;
    [SerializeField] CharacterManager characterManager;
    [SerializeField] ChoiceManager choiceManager;
    [SerializeField] private PlayableDirector hcTimeline;

    [SerializeField] private LogbookManager logbookManager;
    [SerializeField] private CameraMovement cameraMovement;

    //Timer
    private float fourMinuteTimer = 240;

    private bool updateInsanity = false;
    private float timeNextInsanityUpdate;

    private bool mainTimerIsOff = false;
    private bool charTimerIsOff = true;
    //CharacterTimer
    [SerializeField] private float characterTimerLength;
    private float characterTimer;

    //Animaties
    private void Awake()
    {
        timeNextInsanityUpdate = fourMinuteTimer - 48;
        Instance = this;
        //SetCanChoice(true);
        ResetCharacterTimerLength();
    }

    private void FixedUpdate()
    {
        if (!mainTimerIsOff) GameTimer();   // Telt de gameTimer af
        if (!charTimerIsOff && hcTimeline.state != PlayState.Playing && !mainTimerIsOff) CharacterChoiceTimer(); // Telt de Chartimer af
    }


    #region Timers
    private void GameTimer()
    {
        fourMinuteTimer -= Time.deltaTime;

        if (fourMinuteTimer < timeNextInsanityUpdate)
        {
            updateInsanity = true;
            timeNextInsanityUpdate = timeNextInsanityUpdate - 48;
        }

        if (fourMinuteTimer <= 0)
        {
            mainTimerIsOff = true;
            //ForcelogBook();
        }
    }

    private void ForcelogBook()
    {
        cameraMovement.SetLock(true);
        cameraMovement.SetCurrentCameraDirection(CameraMovement.CameraDirection.Left);

        logbookManager.OpenLogbook(); // voor de zekerheid
    }

    public void ResetCharacterTimerLength()
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
                choiceManager.OnClick(5);
                //characterManager.SetCharacterChoice(ChoiceManager.ChoiceEnum.Skip, null);
                //characterManager.NextCharacter();
                charTimerIsOff = true;
                ResetCharacterTimerLength();
            }
        }
    }
    #endregion

    public bool AddInsanity()
    {
        if (updateInsanity)
        {
            updateInsanity = false;
            return true;
        }
        return false;
    }

    public bool OutOfTime()
    {
        return mainTimerIsOff;
    }

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

    public void StopCharTimer()
    {
        charTimerIsOff = true;
        ResetCharacterTimerLength();
    }

    #endregion
}

