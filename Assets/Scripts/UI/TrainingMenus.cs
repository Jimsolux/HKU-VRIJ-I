using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingMenus : MonoBehaviour
{
    [SerializeField] private GameObject popUpExperiment;
    [SerializeField] private GameObject popUpMuseum;
    [SerializeField] private GameObject popUpBreeding;
    [SerializeField] private GameObject popUpSkip;
    [SerializeField] private GameObject popUpFood;
    [SerializeField] private GameObject popUpPet;

    public void OpenPopup(int popup)
    {
        popUpExperiment.SetActive(false);
        popUpMuseum.SetActive(false);
        popUpBreeding.SetActive(false);
        popUpSkip.SetActive(false);
        popUpFood.SetActive(false);
        popUpPet.SetActive(false);

        switch (popup)
        {
            case 0:
                popUpExperiment.SetActive(true);
                break;
            case 1:
                popUpMuseum.SetActive(true);
                break;
            case 2:
                popUpBreeding.SetActive(true);
                break;
            case 3:
                popUpSkip.SetActive(true); 
                break;
            case 4:
                popUpFood.SetActive(true);
                break;
            case 5:
                popUpPet.SetActive(true); 
                break;
        }
    }
}
