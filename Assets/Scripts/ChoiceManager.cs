using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceManager : MonoBehaviour
{
    public bool canChoose;  // Decides if a choice can be made at this moment.
    private bool notOnChoiceCooldown = true;
    private bool oldRaySystemOn = true;
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

    public void OnClick(int ID)
    {
        if (notOnChoiceCooldown && canChoose)
        {
            PlayButtonSound();
            switch (ID)
            {
                case 0:
                    Choice = ChoiceEnum.Breeding;
                    FavourManager.instance.UpdateFavour(FavourManager.FavourType.population);
                    characterManager.SetCharacterChoice(Choice);                    
                    StartCoroutine(ChoiceCoolDown());
                    break;
                case 1:
                    Choice = ChoiceEnum.Museum;
                    FavourManager.instance.UpdateFavour(FavourManager.FavourType.entertainment, FavourManager.FavourType.anthropology);
                    characterManager.SetCharacterChoice(Choice);                    
                    StartCoroutine(ChoiceCoolDown());
                    break;
                case 2:
                    Choice = ChoiceEnum.Experiments;
                    FavourManager.instance.UpdateFavour(FavourManager.FavourType.entertainment, FavourManager.FavourType.anthropology);
                    characterManager.SetCharacterChoice(Choice);
                    StartCoroutine(ChoiceCoolDown());
                    break;
                case 3:
                    Choice = ChoiceEnum.Pet;
                    FavourManager.instance.UpdateFavour(FavourManager.FavourType.entertainment);
                    characterManager.SetCharacterChoice(Choice);
                    StartCoroutine(ChoiceCoolDown());
                    break;
                case 4:
                    Choice = ChoiceEnum.MakeFood;
                    FavourManager.instance.UpdateFavour(FavourManager.FavourType.food);
                    characterManager.SetCharacterChoice(Choice);
                    StartCoroutine(ChoiceCoolDown());
                    break;
                case 5:
                    Choice = ChoiceEnum.Skip;
                    FavourManager.instance.DecreaseFavour();
                    characterManager.SetCharacterChoice(Choice);
                    StartCoroutine(ChoiceCoolDown());
                    break;
            }
            mt.SetBottomText(Choice.ToString());
            //StartCoroutine(WaitBeforeAnimation());
            StartTubeAnimation();
            DeskLamp.TurnOn();
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
