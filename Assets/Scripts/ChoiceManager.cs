using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceManager : MonoBehaviour
{
    private bool notOnChoiceCooldown = false;
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
        if (Input.GetMouseButtonDown(0) && notOnChoiceCooldown)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("BreedingButton"))
                {
                    Choice = ChoiceEnum.Breeding;
                    FavourManager.instance.UpdateFavour(FavourManager.FavourType.popu);
                    GameManager.Instance.PushChoice(Choice);
                    StartCoroutine(ChoiceCoolDown() );
                }
                if (hit.collider.CompareTag("MuseumButton"))
                {
                    Choice = ChoiceEnum.Museum;
                    FavourManager.instance.UpdateFavour(FavourManager.FavourType.ente, FavourManager.FavourType.anth);
                    GameManager.Instance.PushChoice(Choice);
                    StartCoroutine(ChoiceCoolDown());
                }
                if (hit.collider.CompareTag("ExperimentsButton"))
                {
                    Choice = ChoiceEnum.Experiments;
                    FavourManager.instance.UpdateFavour(FavourManager.FavourType.ente, FavourManager.FavourType.anth);
                    GameManager.Instance.PushChoice(Choice);
                    StartCoroutine(ChoiceCoolDown());
                }
                if (hit.collider.CompareTag("PetButton"))
                {
                    Choice = ChoiceEnum.Pet;
                    FavourManager.instance.UpdateFavour(FavourManager.FavourType.ente);
                    GameManager.Instance.PushChoice(Choice);
                    StartCoroutine(ChoiceCoolDown());
                }
                if (hit.collider.CompareTag("MakeFoodButton"))
                {
                    Choice = ChoiceEnum.MakeFood;
                    FavourManager.instance.UpdateFavour(FavourManager.FavourType.food);
                    GameManager.Instance.PushChoice(Choice);
                    StartCoroutine(ChoiceCoolDown());
                }
                if (hit.collider.CompareTag("SkipButton"))
                {
                    Choice = ChoiceEnum.Skip;
                    GameManager.Instance.PushChoice(Choice);
                    StartCoroutine(ChoiceCoolDown());
                }

                if (hit.collider.CompareTag("Logbook"))
                {
                    // Open Logbook!!
                }
            }
        }
    }


    public void OnClick(int ID)
    {
        if (notOnChoiceCooldown)
        {

        switch (ID)
        {
            case 0:
                Choice = ChoiceEnum.Breeding;
                FavourManager.instance.UpdateFavour(FavourManager.FavourType.popu);
                GameManager.Instance.PushChoice(Choice);
                StartCoroutine(ChoiceCoolDown());
                break;
            case 1:
                Choice = ChoiceEnum.Museum;
                FavourManager.instance.UpdateFavour(FavourManager.FavourType.ente, FavourManager.FavourType.anth);
                GameManager.Instance.PushChoice(Choice);
                StartCoroutine(ChoiceCoolDown());
                break;
            case 2:
                Choice = ChoiceEnum.Experiments;
                FavourManager.instance.UpdateFavour(FavourManager.FavourType.ente, FavourManager.FavourType.anth);
                GameManager.Instance.PushChoice(Choice);
                StartCoroutine(ChoiceCoolDown());
                break;
            case 3:
                Choice = ChoiceEnum.Pet;
                FavourManager.instance.UpdateFavour(FavourManager.FavourType.ente);
                GameManager.Instance.PushChoice(Choice);
                StartCoroutine(ChoiceCoolDown());
                break;
            case 4:
                Choice = ChoiceEnum.MakeFood;
                FavourManager.instance.UpdateFavour(FavourManager.FavourType.food);
                GameManager.Instance.PushChoice(Choice);
                StartCoroutine(ChoiceCoolDown());
                break;
            case 5:
                Choice = ChoiceEnum.Skip;
                FavourManager.instance.DecreaseFavour();
                GameManager.Instance.PushChoice(Choice);
                StartCoroutine(ChoiceCoolDown());
                break;
        }
        }
    }


    private IEnumerator ChoiceCoolDown()
    {
        notOnChoiceCooldown = false;
        yield return new WaitForSeconds(2);
        notOnChoiceCooldown = true;
    }


}
