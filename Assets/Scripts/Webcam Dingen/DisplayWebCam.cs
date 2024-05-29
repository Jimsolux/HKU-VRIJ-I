using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayWebCam : MonoBehaviour
{

    private void Start()
    {
        WebCamDevice[] devices = WebCamTexture.devices;

        // print available webcams to the console
        for (int i = 0; i < devices.Length; i++)
            Debug.Log("Webcams available: " + devices[i].name);

        Renderer renderer = this.GetComponent<Renderer>();

        WebCamTexture texture = new WebCamTexture(devices[0].name);
        renderer.material.mainTexture = texture;
        texture.Play();
    }
}
