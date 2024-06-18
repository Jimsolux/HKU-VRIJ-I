using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraMovement : MonoBehaviour
{
    public static CameraMovement instance;

    private void Awake()
    {

        instance = this;
        if(SceneManager.GetActiveScene().name == "MainGame")
        Cursor.lockState = CursorLockMode.Locked;
    }

    private Vector2 screenResolution;

    private Camera cam;

    [Header("Camera")]
    [Range(0, 0.5f)] [SerializeField] private float percentageCamSwap;
    [SerializeField] private float cameraSpeed = 2;
    [SerializeField] private float cooldown = 0.5f;
    private CameraDirection currentCameraDirection = CameraDirection.Center;
    private int camSwapPixels;
    private bool lockMotion; // cutscene stuff
    private bool onCooldown;
    public bool inCutscene = true;

    [Header("Angle")]
    [SerializeField] private Vector3 angleLeft, angleCenter, angleRight, angleButtons;
    [SerializeField] private int fovLeft, fovCenter, fovRight, fovButtons;
    [SerializeField] private float fovChangeSpeed = 30;

    [Header("UI")]
    [SerializeField] private GameObject leftUI, rightUI;
    private bool triggeredLeft, triggeredRight;
    [SerializeField] private GameObject zoomInButton;

    [SerializeField] private LogbookManager logbookManager;
    [SerializeField] private Animator logbookCameraAnimator;
    public enum CameraDirection
    {
        Left, Center, Right, Buttons
    }

    public CameraDirection Direction() { return currentCameraDirection; }
 
    private void Start()
    {
        cam = GetComponent<Camera>();

        screenResolution = new() { x = Screen.width, y = Screen.height };

        camSwapPixels = Mathf.RoundToInt(screenResolution.x * percentageCamSwap);
    }

    private bool forceCenter = false;
    public void ForceCenter()
    {
        forceCenter = true;
    }
    void Update()
    {
        if (inCutscene == false && !logbookManager.GetLock())
        {
            Cursor.lockState = CursorLockMode.None;
            Vector3 targetAngle = new();
            if (forceCenter)
            {
                currentCameraDirection = CameraDirection.Center;
                targetAngle = GetCameraAngle();
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(targetAngle), cameraSpeed);
                return;
            }

            CameraDirection previousCameraDirection = currentCameraDirection;
            if (!lockMotion && !onCooldown)
            {
                currentCameraDirection = GetDirection();
            }
            targetAngle = GetCameraAngle();

            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(targetAngle), cameraSpeed);

            //if (previousCameraDirection != currentCameraDirection)
            SetCameraFOV();
        }
    }

    private IEnumerator CameraMoveCooldown()
    {
        onCooldown = true;
        yield return new WaitForSeconds(cooldown);
        onCooldown = false;
    }

    private CameraDirection GetDirection()
    {
        if (inCutscene) return CameraDirection.Center;

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

        if (currentCameraDirection == CameraDirection.Buttons)
        {
            if (mousePixels.y < screenResolution.y / 6)
            {
                ButtonZoomOut();
                return CameraDirection.Center;
            }
        }

        if (mousePixels.x <= camSwapPixels) return cameraDirLeft;
        else if (mousePixels.x >= screenResolution.x - camSwapPixels) return cameraDirRight;
        return currentCameraDirection;
    }

    bool restartedTyping = false;
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
                logbookCameraAnimator.SetBool("InView", true);
                return angleLeft;

            case CameraDirection.Center: 
                restartedTyping = false;
                logbookCameraAnimator.SetBool("InView", false);
                return angleCenter;

            case CameraDirection.Buttons:  return angleButtons;

            case CameraDirection.Right:
                if (!triggeredRight)
                {
                    triggeredRight = true;
                    MonitorUI.instance.StartDialogue();
                    HandleUI();
                }
                if (!restartedTyping)
                {
                    MonitorUI.instance?.ReturnToMonitor();
                    restartedTyping = true;
                }
                return angleRight;
        }
        return angleCenter;
    }

    private void SetCameraFOV()
    {
        int targetFOV = 0;

        if (currentCameraDirection != CameraDirection.Left) 
            logbookManager.CloseLogbook(); 
        else  
            logbookManager.OpenLogbook(); 

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

            case CameraDirection.Buttons:
                targetFOV = fovButtons;
                break;
        }
        if (Mathf.Abs(cam.fieldOfView - targetFOV) > 1f)
        {
            if (cam.fieldOfView > targetFOV) cam.fieldOfView -= Time.deltaTime * fovChangeSpeed;
            else if (cam.fieldOfView < targetFOV) cam.fieldOfView += Time.deltaTime * fovChangeSpeed;
        }
        else { cam.fieldOfView = targetFOV; }
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
    public void SetCutscene(bool value)
    {
        inCutscene = value;
        if (value == false)
        {
            currentCameraDirection = CameraDirection.Right;
            logbookCameraAnimator.gameObject.SetActive(true);
        }
    }

    public void SetCurrentCameraDirection(CameraDirection direction)
    {
        currentCameraDirection = direction;
    }

    public void ButtonZoomIn()
    {
        if (!lockMotion)
        {
            currentCameraDirection = CameraDirection.Buttons;
            zoomInButton.SetActive(false);
        }
    }

    public void ButtonZoomOut()
    {
        currentCameraDirection = CameraDirection.Center;
        zoomInButton.SetActive(true);
    }
}
