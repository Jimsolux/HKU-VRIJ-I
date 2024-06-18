using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToStart : MonoBehaviour
{
    void Start()
    {
        SceneManager.LoadScene(0);
    }
}
