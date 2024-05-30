using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inCutscenebool : MonoBehaviour
{
    public CameraMovement cameraMove;

    private void OnEnable()
    {
        cameraMove.inCutscene = false;
    }
}
