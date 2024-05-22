using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
using TMPro;
using static FavourManager;

public class FavourManager : MonoBehaviour
{
    public static FavourManager instance;
    [SerializeField] private float foodFavour = 1;
    [SerializeField] private float populationFavour = 1;
    [SerializeField] private float anthropologyFavour = 1;
    [SerializeField] private float entertainmentFavour = 1;
    [SerializeField] private float warningAmount;
    [SerializeField] private float increaseAmount;
    [SerializeField] private float decreaseAmount;

    [SerializeField] private PlayableDirector hcTimeline;
    [SerializeField] private TextMeshProUGUI warningText;

    public enum FavourType
    {
        food,
        popu,
        anth,
        ente,
        none
    }

    public void Awake()
    {
        instance = this;
    }

    public void UpdateFavour(FavourType type)// Called after choice loop
    {
        DecreaseFavour();
        IncreaseFavour(type);
        Debug.Log("Increased Value of " + type);
        CheckFavourLevels();
    }

    public void UpdateFavour(FavourType type, FavourType type2)// Called after choice loop, 2 inputs
    {
        DecreaseFavour();
        IncreaseFavour(type);
        IncreaseFavour(type2);
        Debug.Log("Increased Value of " + type + " and of " + type2);
        CheckFavourLevels();
    }

    public void DecreaseFavour() // Decreases all amounts by decreaseAmount
    {
        foodFavour -= decreaseAmount;
        populationFavour -= decreaseAmount;
        anthropologyFavour -= decreaseAmount;
        entertainmentFavour -= decreaseAmount;
    }

    public void IncreaseFavour(FavourType type)
    {
        switch(type)
        {
            case FavourType.food:
                foodFavour += increaseAmount;
                break;
            case FavourType.popu:
                populationFavour += increaseAmount;
                break;
            case FavourType.anth:
                anthropologyFavour += increaseAmount;
                break;
            case FavourType.ente:
                entertainmentFavour += increaseAmount;
                break;
            case FavourType.none:
                break;
        }
    }

    public void CheckFavourLevels()
    {
        if (foodFavour < warningAmount) WarnPlayer(FavourType.food);
        if (populationFavour < warningAmount) WarnPlayer(FavourType.popu);
        if (anthropologyFavour < warningAmount) WarnPlayer(FavourType.anth);
        if (entertainmentFavour < warningAmount) WarnPlayer(FavourType.ente);
    }


    public void WarnPlayer(FavourType favourType)
    {
        if (hcTimeline.state != PlayState.Playing)
        {
            hcTimeline.Stop();
            hcTimeline.time = 0;
            hcTimeline.Evaluate();
        }

        warningText.text = "WE NEED MORE " + favourType.ToString().ToUpper() + "!";
        hcTimeline.Play();
    }
}
