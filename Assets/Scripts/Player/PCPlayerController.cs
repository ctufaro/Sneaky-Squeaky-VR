using UnityEngine;
using UnityEngine.InputSystem;

public class PCPlayerController : MonoBehaviour, PlayerInputActions.IPlayerActions
{
    public float speed = 5.0f;
    public float gravity = -9.81f;
    public Transform cameraTransform;
    public float lookSensitivity = 2.0f; // Sensitivity multiplier for look input
    public float rotationSmoothTime = 0.1f; // Smooth time for rotation

    private CharacterController controller;
    private Vector3 velocity;

    private PlayerInputActions playerInputActions;
    private Vector2 moveInput;
    private Vector2 lookInput;

    private float yaw = 0.0f; // Horizontal rotation
    private float pitch = 0.0f; // Vertical rotation

    private float currentYaw;
    private float currentPitch;
    private float yawVelocity;
    private float pitchVelocity;

    void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.SetCallbacks(this);
    }

    void OnEnable()
    {
        playerInputActions.Player.Enable();
    }

    void OnDisable()
    {
        playerInputActions.Player.Disable();
    }

    void Start()
    {
        controller = GetComponent<CharacterController>();
        // Hide and lock the cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Ensure the camera is looking forward on startup
        cameraTransform.localRotation = Quaternion.Euler(0, 0, 0);
    }

    void Update()
    {
        // Check if the Escape key is pressed to unlock and show the cursor, or quit the application
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            // Unlock the cursor and make it visible
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            // Optional: Quit the application
            // Application.Quit();
        }

        // Get the forward and right directions relative to the camera
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        // Ensure the player only moves on the XZ plane
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        // Calculate the desired move direction
        Vector3 moveDirection = forward * moveInput.y + right * moveInput.x;

        // Move the character
        controller.Move(moveDirection * speed * Time.deltaTime);

        // Handle gravity
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // Apply sensitivity multiplier to look input
        yaw += lookInput.x * lookSensitivity;
        pitch -= lookInput.y * lookSensitivity;
        pitch = Mathf.Clamp(pitch, -85f, 85f); // Constrain pitch

        // Smooth the rotation
        currentYaw = Mathf.SmoothDamp(currentYaw, yaw, ref yawVelocity, rotationSmoothTime);
        currentPitch = Mathf.SmoothDamp(currentPitch, pitch, ref pitchVelocity, rotationSmoothTime);

        cameraTransform.localRotation = Quaternion.Euler(currentPitch, currentYaw, 0.0f);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        //Debug.Log("Move action performed");
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        //Debug.Log("Look action performed");
        lookInput = context.ReadValue<Vector2>();
    }
}
