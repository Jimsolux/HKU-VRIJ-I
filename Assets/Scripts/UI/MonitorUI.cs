using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonitorUI : MonoBehaviour
{
    public static MonitorUI instance;

    [Header("Taskbar")]
    [SerializeField] private Sprite tabSelected;
    [SerializeField] private Sprite tabNotSelected;

    [SerializeField] private Image personalInfoImage;
    [SerializeField] private MonitorText personalInfoTextHandler;
    [SerializeField] private Image personalInfoProfilePicture; 
    [SerializeField] private MonitorText startTextHandler;
    [SerializeField] private Image trainingImage;
    [SerializeField] private Image desiresImage;

    [SerializeField] private GameObject popUpPersonalInfo; 
    [SerializeField] private GameObject popUpDesires; 
    // training needs no popup, since it won't update.
 
    [Header("Tabs")]
    [SerializeField] private GameObject personalInformationTab;
    [SerializeField] private GameObject trainingTab;
    [SerializeField] private GameObject desiresTab;
    [Header("Desires")]
    [SerializeField] private Slider desiresSliderFood;
    [SerializeField] private Slider desiresSliderPopulation;
    [SerializeField] private Slider desiresSliderAnthropology;
    [SerializeField] private Slider desiresSliderEntertainment;

    private bool updatedPerson = false;

    private void Awake()
    {
        instance = this; 
    }

    private bool lockOptions = true;
    public void Unlock() { lockOptions = false; }

    public void SetPopUpPersonalInfo() { popUpPersonalInfo.SetActive(true); }
    public void SetPopUpDesires() { popUpDesires.SetActive(true); }

    private string typeStringCharacter;

    public void SetStringCharacter(string newString) { typeStringCharacter = newString; }

    public void SetSpriteCharacter(Sprite picture) { personalInfoProfilePicture.sprite = picture; }

    public void StartDialogue()
    {
        startTextHandler.StartDialogue();
    }
    public void ReturnToMonitor()
    {
        SetSliderValues();
        if (personalInformationTab.activeSelf && updatedPerson)
        {
            updatedPerson = false;
            popUpPersonalInfo.SetActive(false);
            StartCoroutine(personalInfoTextHandler.WriteText(typeStringCharacter));
        }

       StartCoroutine(HallucinationEffects.instance.ClearMonitorEffect());
    }

    public void UpdatePerson()
    {
        updatedPerson = true;
    }

    public void SelectTab(int tab)
    {
        if (lockOptions) return;

        personalInformationTab.SetActive(false);
        trainingTab.SetActive(false);
        desiresTab.SetActive(false);

        personalInfoImage.sprite = tabNotSelected;
        trainingImage.sprite = tabNotSelected;
        desiresImage.sprite = tabNotSelected;

        switch (tab)
        {
            case 0: 
                personalInformationTab.SetActive(true);
                personalInfoImage.sprite = tabSelected;
                popUpPersonalInfo.SetActive(false);

                StartCoroutine(personalInfoTextHandler.WriteText(typeStringCharacter));
                break;

            case 1: 
                trainingTab.SetActive(true);
                trainingImage.sprite = tabSelected;
                break;

            case 2: desiresTab.SetActive(true);
                desiresImage.sprite = tabSelected;
                popUpDesires.SetActive(false);
                SetSliderValues();
                break;
        }
    }
    
    private void SetSliderValues()
    {
        desiresSliderFood.value = FavourManager.instance.GetFoodValue();
        desiresSliderPopulation.value = FavourManager.instance.GetPopulationValue();
        desiresSliderAnthropology.value = FavourManager.instance.GetAnthropologyValue();
        desiresSliderEntertainment.value = FavourManager.instance.GetEntertainmentValue();
    }
}
