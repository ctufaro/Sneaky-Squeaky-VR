using UnityEngine;

public class MonsterAITest : MonoBehaviour
{
    // Animator and camera settings
    public Animator animator;
    public Vector3 cameraOffset = new Vector3(0, 1.07f, 2.39f);  // Adjust this offset as needed (z should be negative to be in front)
    public float cameraSmoothSpeed = 0.125f;

    // Parameters for controlling the monster
    private float speed = 0f;
    private float soundIntensity = 0f;
    private bool isAttacking = false;

    // New camera reference
    private Camera followCamera;

    void Start()
    {
        // Create a new camera dynamically
        followCamera = new GameObject("FollowCamera").AddComponent<Camera>();

        // Position the camera in front of the monster
        followCamera.transform.position = transform.position + transform.forward * cameraOffset.z + Vector3.up * cameraOffset.y + transform.right * cameraOffset.x;

        // Make the camera look at the monster
        followCamera.transform.LookAt(transform.position + Vector3.up * cameraOffset.y);

        // Deactivate the main camera
        Camera.main.gameObject.SetActive(false);

        // Make the new camera the active camera
        followCamera.gameObject.SetActive(true);
    }

    void Update()
    {
        // Update monster state based on input
        HandleInput();
        UpdateAnimatorParameters();
        MoveMonster(); // Optional: If you want the monster to move as well
    }

    void LateUpdate()
    {
        // Smoothly follow the monster with the new camera
        CameraFollow();
    }

    void HandleInput()
    {
        // Adjust speed with up and down arrow keys
        if (Input.GetKey(KeyCode.UpArrow))
        {
            speed += Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            speed -= Time.deltaTime;
        }
        speed = Mathf.Clamp(speed, 0f, 1f); // Clamp speed between 0 and 1

        // Adjust sound intensity with left and right arrow keys
        if (Input.GetKey(KeyCode.RightArrow))
        {
            soundIntensity += Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            soundIntensity -= Time.deltaTime;
        }
        soundIntensity = Mathf.Clamp(soundIntensity, 0f, 1f); // Clamp sound intensity between 0 and 1

        // Toggle attack mode with the space bar
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isAttacking = !isAttacking;
        }
    }

    void UpdateAnimatorParameters()
    {
        // Set Animator parameters
        animator.SetFloat("Speed", speed);
        animator.SetFloat("SoundIntensity", soundIntensity);
        animator.SetBool("IsAttacking", isAttacking);

        // Debug output to monitor states
        Debug.Log($"Speed: {speed}, SoundIntensity: {soundIntensity}, IsAttacking: {isAttacking}");
    }

    void CameraFollow()
    {
        // Calculate the desired position in front of the monster's face
        Vector3 desiredPosition = transform.position + transform.forward * cameraOffset.z + Vector3.up * cameraOffset.y + transform.right * cameraOffset.x;

        // Smoothly move the camera to the desired position
        Vector3 smoothedPosition = Vector3.Lerp(followCamera.transform.position, desiredPosition, cameraSmoothSpeed);
        followCamera.transform.position = smoothedPosition;

        // Rotate the camera to look at the monster's face
        followCamera.transform.LookAt(transform.position + Vector3.up * cameraOffset.y);
    }

    void MoveMonster()
    {
        // Example of moving the monster forward based on speed (optional)
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
