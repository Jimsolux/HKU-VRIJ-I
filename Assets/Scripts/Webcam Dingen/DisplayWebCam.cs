using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;

public class DisplayWebCam 
{
    public DisplayWebCam(Renderer renderer, int i)
    {
        WebCamTexture texture = FindWebCamTexture(i);
        ApplyWebCamTexture(renderer, texture);
    }

    public WebCamTexture FindWebCamTexture(int i)
    {
        // Debugging
        foreach(WebCamDevice device in WebCamTexture.devices)
            Debug.Log(device.name);

        return new WebCamTexture(WebCamTexture.devices[i].name);
    }

    public void ApplyWebCamTexture(Renderer renderer, WebCamTexture texture)
    {
        renderer.material.mainTexture = texture;
        texture.Play();
    }
}
