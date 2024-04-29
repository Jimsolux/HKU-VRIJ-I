using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FavourSystem : MonoBehaviour
{
    public float foodFavour;
    public float populationFavour;
    public float anthropologyFavour;
    public float entertainmentFavour;
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

    private void Start()
    {
        //Set all the favour to 1.
        foodFavour = 1;
        populationFavour = 1;
        anthropologyFavour = 1;
        entertainmentFavour = 1;

    }

    public void UpdateFavour(FavourType type)// Called after choice loop
    {
        DecreaseFavour();
        IncreaseFavour(type);
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



}
