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

    [SerializeField] GameObject logBookCanvas;
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


    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && oldRaySystemOn)  // Old method to check for buttonclicks, can still be used to open logbook.
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Logbook"))
                {
                    // Open Logbook!!
                    logBookCanvas.SetActive(true);
                    Debug.Log("Tried to open logbookCanvas");
                }
            }
        }
    }

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
                    FavourManager.instance.UpdateFavour(FavourManager.FavourType.popu);
                    characterManager.SetCharacterChoice(Choice);                    
                    StartCoroutine(ChoiceCoolDown());
                    break;
                case 1:
                    Choice = ChoiceEnum.Museum;
                    FavourManager.instance.UpdateFavour(FavourManager.FavourType.ente, FavourManager.FavourType.anth);
                    characterManager.SetCharacterChoice(Choice);                    
                    StartCoroutine(ChoiceCoolDown());
                    break;
                case 2:
                    Choice = ChoiceEnum.Experiments;
                    FavourManager.instance.UpdateFavour(FavourManager.FavourType.ente, FavourManager.FavourType.anth);
                    characterManager.SetCharacterChoice(Choice);
                    StartCoroutine(ChoiceCoolDown());
                    break;
                case 3:
                    Choice = ChoiceEnum.Pet;
                    FavourManager.instance.UpdateFavour(FavourManager.FavourType.ente);
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
            Debug.Log("Choice made for character with button is " + Choice.ToString());
            //StartCoroutine(WaitBeforeAnimation());
            StartTubeAnimation();
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
