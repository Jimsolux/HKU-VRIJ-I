using UnityEngine;

public class WebCamTextureToFace : MonoBehaviour
{
    private DisplayWebCam webCam;

    void Start()
    {
        GameObject head = gameObject.transform.Find("HumanArmature/Hips/Torso/Neck/Head/Face").gameObject;

        Renderer renderer = head.GetComponent<Renderer>();

        webCam = new DisplayWebCam(renderer, 0);
    }
}
