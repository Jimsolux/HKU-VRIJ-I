using UnityEngine;
using UnityEngine.UI;

public static class DisplayWebCam 
{
    // display webcam feed
    static public void Display(Renderer renderer, int i = 0)
    {
        WebCamTexture texture = FindWebCamTexture(i);
        ApplyWebCamTextureToRenderer(renderer, texture);
    }

    // display webcam feed
    static public void DisplayUI(Image image, int i = 0)
    {
        WebCamTexture texture = FindWebCamTexture(i);
        ApplyWebCamTextureToImage(image, texture);
    }

    static private WebCamTexture FindWebCamTexture(int i)
    {
        // Debugging
        foreach(WebCamDevice device in WebCamTexture.devices)
            Debug.Log(device.name);

        return new WebCamTexture(WebCamTexture.devices[i].name);
    }

    static private void ApplyWebCamTextureToRenderer(Renderer renderer, WebCamTexture texture)
    {
        renderer.material.mainTexture = texture;
        texture.Play();
    }

    static private void ApplyWebCamTextureToImage(Image image, WebCamTexture texture)
    {
        image.material.mainTexture = texture;
        texture.Play();
    }
}
