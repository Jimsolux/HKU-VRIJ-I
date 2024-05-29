using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DeskLamp
{ 

    public static void TurnOn()  { GameObject.FindGameObjectWithTag("DeskLights").transform.GetChild(0).gameObject.SetActive(true); }

    public static void TurnOff()  { GameObject.FindGameObjectWithTag("DeskLights").transform.GetChild(0).gameObject.SetActive(false); }
}


// I'm not sorry for the formatting. 
// reply ayser: thx