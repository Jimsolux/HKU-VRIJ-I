using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DeskLamp
{ 

    public static void TurnOn()  {
        GameObject.FindGameObjectWithTag("DeskLights").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.FindGameObjectWithTag("DeskLights").transform.GetChild(1).GetComponent<AudioSource>().Play(); 
    }

    public static void TurnOff()  {
        GameObject.FindGameObjectWithTag("DeskLights").transform.GetChild(0).gameObject.SetActive(false);
        GameObject.FindGameObjectWithTag("DeskLights").transform.GetChild(1).GetComponent<AudioSource>().Play();
    }
}


// I'm not sorry for the formatting. 
// reply ayser: thx