using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceManager : MonoBehaviour
{
    public bool canChoose;  // Decides if a choice can be made at this moment.
    private bool notOnChoiceCooldown = true;
    [SerializeField] public CharacterManager characterManager;
    [SerializeField] AudioSource buttonPanelAudio;

    [SerializeField] private MonitorText mt; 

    public enum ChoiceEnum
    {
        Breeding,
        Museum,
        Experiments,
        Pet,
        MakeFood,
        Skip
    }

    [Header("After sprites")]
    [SerializeField] private Sprite breedingSprite;   
    [SerializeField] private Sprite museumSprite;    
    [SerializeField] private Sprite experimentSprite;  
    [SerializeField] private Sprite petSprite;       
    [SerializeField] private Sprite makeFoodSprite;   
    [SerializeField] private Sprite skipSprite;      

    public ChoiceEnum Choice;

    #region CheckIfCanChoose
    public void SetCanChoose(bool canItChoose)
    {
        canChoose = canItChoose;
    }
    
    private IEnumerator ChoiceCoolDown()
    {
        notOnChoiceCooldown = false;
        yield return new WaitForSeconds(1);
        notOnChoiceCooldown = true;
    }
    #endregion
    private IEnumerator WaitBeforeAnimation()
    {
        yield return new WaitForSeconds(1);
    }
    bool tubeIsEmpty = false;
    public void LockdownChoice()
    {
        tubeIsEmpty = true;
    }
    public void OnClick(int ID)
    {
        if (notOnChoiceCooldown && canChoose && !tubeIsEmpty)
        {
            GameManager.Instance.ResetCharacterTimerLength();

            if (GameManager.Instance.AddInsanity())
            {
                Hallucination.instance.ChangeHallucinationStrength(2);
            }

            PlayButtonSound();
            switch (ID)
            {
                case 0:
                    Choice = ChoiceEnum.Breeding;
                    FavourManager.instance.UpdateFavour(FavourManager.FavourType.population);
                    characterManager.SetCharacterChoice(Choice, breedingSprite);           
                    StartCoroutine(ChoiceCoolDown());
                    break;
                case 1:
                    Choice = ChoiceEnum.Museum;
                    FavourManager.instance.UpdateFavour(FavourManager.FavourType.entertainment, FavourManager.FavourType.anthropology);
                    characterManager.SetCharacterChoice(Choice, museumSprite);                    
                    StartCoroutine(ChoiceCoolDown());
                    break;
                case 2:
                    Choice = ChoiceEnum.Experiments;
                    FavourManager.instance.UpdateFavour(FavourManager.FavourType.entertainment, FavourManager.FavourType.anthropology);
                    characterManager.SetCharacterChoice(Choice, experimentSprite);
                    StartCoroutine(ChoiceCoolDown());
                    break;
                case 3:
                    Choice = ChoiceEnum.Pet;
                    FavourManager.instance.UpdateFavour(FavourManager.FavourType.entertainment);
                    characterManager.SetCharacterChoice(Choice, petSprite);
                    StartCoroutine(ChoiceCoolDown());
                    break;
                case 4:
                    Choice = ChoiceEnum.MakeFood;
                    FavourManager.instance.UpdateFavour(FavourManager.FavourType.food);
                    characterManager.SetCharacterChoice(Choice, makeFoodSprite);
                    StartCoroutine(ChoiceCoolDown());
                    break;
                case 5:
                    Choice = ChoiceEnum.Skip;
                    FavourManager.instance.DecreaseFavour();
                    FavourManager.instance.CheckFavourLevels();
                    characterManager.SetCharacterChoice(Choice, skipSprite);
                    StartCoroutine(ChoiceCoolDown());
                    break;
            }
            //StartCoroutine(WaitBeforeAnimation());
            StartTubeAnimation();
            DeskLamp.TurnOn();
            MonitorUI.instance.UpdatePerson();

            canChoose = false;
        }
    }//Button Click 

    private void StartTubeAnimation()
    {
        characterManager.AnimatorSendBackwards();
    }

    #region audio
    public void PlayButtonSound()
    {
        buttonPanelAudio.Play();
    }
    #endregion

}
