using UnityEngine;

public class MotionManager : MonoBehaviour
{
    public float moveSpeed = 5f;  // Speed of movement
    public float mouseSensitivity = 100f;  // Sensitivity for mouse movement
    public GameObject xrOrigin;   // Reference to the XR Origin

    private Transform cameraTransform;  // Reference to the camera's transform
    private float xRotation = 0f;  // Rotation around the X-axis for looking up and down

    void Start()
    {
        if (xrOrigin == null)
        {
            Debug.LogError("XR Origin is not assigned!");
            return;
        }

        // Find the camera within the XR Origin
        cameraTransform = xrOrigin.GetComponentInChildren<Camera>().transform;

        if (cameraTransform == null)
        {
            Debug.LogError("Camera not found in XR Origin.");
        }

        // Lock the cursor to the game window
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (DebugConfig.Instance.isDebuggingOnPC)
        {
            HandleMovement();
            HandleMouseLook();
        }
    }

    void HandleMovement()
    {
        // Get input for movement
        float moveX = Input.GetAxis("Horizontal");  // A/D or Left/Right
        float moveZ = Input.GetAxis("Vertical");    // W/S or Up/Down

        // Calculate movement direction relative to the XR Origin's orientation
        Vector3 move = xrOrigin.transform.right * moveX + xrOrigin.transform.forward * moveZ;

        // Apply movement to the XR Origin's position
        xrOrigin.transform.position += move * moveSpeed * Time.deltaTime;
    }

    void HandleMouseLook()
    {
        // Get mouse movement input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Rotate the XR Origin (yaw) around the Y-axis only
        xrOrigin.transform.Rotate(Vector3.up * mouseX);

        // Rotate the camera (pitch) around the X-axis
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);  // Limit the pitch to prevent flipping
        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Reset XR Origin's X and Z rotation to prevent tilting
        Vector3 currentRotation = xrOrigin.transform.eulerAngles;
        xrOrigin.transform.rotation = Quaternion.Euler(0f, currentRotation.y, 0f);
    }
}
