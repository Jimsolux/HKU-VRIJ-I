using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerTriggers : MonoBehaviour
{
    [SerializeField] private GameManager gm;

    private void Awake()
    {
        gm = GameObject.Find("GameManagerObject").GetComponent<GameManager>();
    }


    public void SetChoiceTrue()
    {
        gm.SetCanChoice(true);
    }
    public void SetChoiceFalse()
    {
        gm.SetCanChoice(false);
    }

    public void NextChar()
    {
        gm.PrepareNextCharacter();
    }

    public void StartCharTimer()
    {
        gm.StartCharTimer();
    }

    public void DeleteGameObject()
    {
        this.gameObject.SetActive(false);
    }
}
