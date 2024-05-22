using System.Collections;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Vector2 screenResolution;

    private Camera cam;

    [Header("Camera")]
    [Range(0, 0.5f)][SerializeField] private float percentageCamSwap;
    [SerializeField] private float cameraSpeed = 2;
    private int camSwapPixels;
    private CameraDirection currentCameraDirection;
    [SerializeField] private float cooldown = 0.5f;
    private bool lockMotion; // cutscene stuff
    private bool onCooldown;

    [Header("Angle")]
    [SerializeField] private Vector3 angleLeft, angleRight;
    [SerializeField] private int fovLeft, fovCenter, fovRight;
    [SerializeField] private float fovChangeSpeed = 30;

    [Header("UI")]
    [SerializeField] private GameObject leftUI, rightUI;
    private bool triggeredLeft, triggeredRight;

    private enum CameraDirection
    {
        Left, Center, Right
    }

    private void Start()
    {
        cam = GetComponent<Camera>();

        screenResolution = new() { x = Screen.width, y = Screen.height };

        camSwapPixels = Mathf.RoundToInt(screenResolution.x * percentageCamSwap);
    }

    void Update()
    {
        if (!lockMotion && !onCooldown)
        {
            currentCameraDirection = GetDirection();
        }
        Vector3 targetAngle = GetCameraAngle();

        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(targetAngle), cameraSpeed);

        SetCameraFOV();
    }

    private IEnumerator CameraMoveCooldown()
    {
        onCooldown = true;
        yield return new WaitForSeconds(cooldown);
        onCooldown = false;
    }

    private CameraDirection GetDirection()
    {
        StartCoroutine(CameraMoveCooldown());

        Vector2 mousePixels = Input.mousePosition;

        CameraDirection cameraDirLeft = currentCameraDirection;
        CameraDirection cameraDirRight = currentCameraDirection;

        if (currentCameraDirection == CameraDirection.Left)
        {
            cameraDirRight = CameraDirection.Center; 
        }
        else if (currentCameraDirection == CameraDirection.Center)
        {
            cameraDirLeft = CameraDirection.Left;
            cameraDirRight = CameraDirection.Right;
        }
        else if (currentCameraDirection == CameraDirection.Right)
        {
            cameraDirLeft = CameraDirection.Center;
        }


            if (mousePixels.x <= camSwapPixels) return cameraDirLeft;
            else if (mousePixels.x >= screenResolution.x - camSwapPixels) return cameraDirRight;
        return currentCameraDirection;

    }

    private Vector3 GetCameraAngle()
    {
        switch (currentCameraDirection)
        {
            case CameraDirection.Left:
                if (!triggeredLeft)
                {
                    triggeredLeft = true;
                    HandleUI();
                }
                return angleLeft;

            case CameraDirection.Center: return new() { x = 15, y = 0, z = 0 };

            case CameraDirection.Right:
                if (!triggeredRight)
                {
                    triggeredRight = true;
                    MonitorText.instance.StartCoroutine(MonitorText.instance.StartSequence());
                    HandleUI();
                }

                return angleRight;
        }
        return new() { x = 15, y = 0, z = 0 };
    }

    private void SetCameraFOV()
    {
        int targetFOV = 0;
        switch (currentCameraDirection)
        {
            case CameraDirection.Left:
                targetFOV = fovLeft;
                break;

            case CameraDirection.Center:
                targetFOV = fovCenter;
                break;

            case CameraDirection.Right:
                targetFOV = fovRight;
                break;
        }
        if (Mathf.Abs(cam.fieldOfView - targetFOV) > 0.1f)
        {
            if (cam.fieldOfView > targetFOV) cam.fieldOfView -= Time.deltaTime * fovChangeSpeed;
            else if (cam.fieldOfView < targetFOV) cam.fieldOfView += Time.deltaTime * fovChangeSpeed;
        }
    }

    private void HandleUI()
    {
        if (triggeredLeft) leftUI.SetActive(false);
        if (triggeredRight) rightUI.SetActive(false);
    }

    public void SetLock(bool value)
    {
        lockMotion = value;
    }
}
