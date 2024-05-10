using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Vector2 screenResolution;

    [Range(0, 0.4f)][SerializeField] private float percentageCamSwap;
    private int camSwapPixels;
    private CameraDirection currentCameraDirection;
    [SerializeField] private float cooldown = 0.5f;

    private enum CameraDirection
    {
        Left, Center, Right
    }

    private void Start()
    {
        screenResolution = new() { x = Screen.width, y = Screen.height };

        camSwapPixels = Mathf.RoundToInt(screenResolution.x * percentageCamSwap);
    }

    void Update()
    {
        currentCameraDirection = GetDirection();

        Vector3 targetAngle = GetCameraAngle();

        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(targetAngle), 1);
    }

    private CameraDirection GetDirection()
    {
        Vector2 mousePixels = Input.mousePosition;

        if (mousePixels.x <= camSwapPixels) return CameraDirection.Left;
        else if (mousePixels.x >= screenResolution.x - camSwapPixels) return CameraDirection.Right;
        return CameraDirection.Center;

    }

    private Vector3 GetCameraAngle()
    {
        switch (currentCameraDirection)
        {
            case CameraDirection.Left: return new() { x = 15, y = -30, z = 0 };
            case CameraDirection.Center: return new() { x = 15, y = 0, z = 0 };
            case CameraDirection.Right: return new() { x = 15, y = 45, z = 0 };
        }
        return new() { x = 15, y = 0, z = 0 };
    }

}
