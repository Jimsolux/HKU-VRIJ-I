using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
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
    public void UpdateFavour(FavourType type, FavourType type2)// Called after choice loop
    {
        DecreaseFavour();
        IncreaseFavour(type);
        IncreaseFavour(type2);
        Debug.Log("Increased Value of " + type + "And of " + type2);
        CheckFavourLevels();
    }

    public void DecreaseFavour() // Decreases all amounts by decreaseAmount
    {
        foodFavour -= decreaseAmount;
        populationFavour -= decreaseAmount;
        anthropologyFavour -= decreaseAmount;
        entertainmentFavour -= decreaseAmount;
        //switch (type)
        //{
        //    case FavourType.food:
        //        populationFavour -= decreaseAmount;
        //        anthropologyFavour -= decreaseAmount;
        //        entertainmentFavour -= decreaseAmount;
        //        break;
        //    case FavourType.popu:
        //        foodFavour -= decreaseAmount;
        //        anthropologyFavour -= decreaseAmount;
        //        entertainmentFavour -= decreaseAmount;
        //        break;
        //    case FavourType.ente:
        //        foodFavour -= decreaseAmount;
        //        populationFavour -= decreaseAmount;
        //        anthropologyFavour -= decreaseAmount;
        //        break;
        //    case FavourType.anth:
        //        foodFavour -= decreaseAmount;
        //        populationFavour -= decreaseAmount;
        //        entertainmentFavour -= decreaseAmount;
        //        break;
        //}
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


    public void GetFavour()
    {

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
        Debug.Log("WARN PLAYER THEIR " + favourType.ToString() + " Is very low! BECOME ANGRY ALIEN!");
    }
}
