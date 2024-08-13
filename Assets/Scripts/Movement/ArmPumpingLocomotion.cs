using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class ArmPumpingLocomotion : MonoBehaviour
{
    public XRNode leftHandNode = XRNode.LeftHand;
    public XRNode rightHandNode = XRNode.RightHand;
    public float speedMultiplier = 1.0f;

    private Vector3 leftHandPrevPos;
    private Vector3 rightHandPrevPos;

    private XRController leftController;
    private XRController rightController;

    private Rigidbody rb;  // Reference to the Rigidbody component

    void Start()
    {
        leftController = SetupController(leftHandNode);
        rightController = SetupController(rightHandNode);
        leftHandPrevPos = GetLocalPosition(leftHandNode);
        rightHandPrevPos = GetLocalPosition(rightHandNode);

        // Get the Rigidbody component
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody not found on the character. Please add a Rigidbody component.");
        }

        // Make sure the Rigidbody doesn't rotate due to physics interactions
        rb.freezeRotation = true;
    }

    void Update()
    {
        Vector3 leftHandCurrentPos = GetLocalPosition(leftHandNode);
        Vector3 rightHandCurrentPos = GetLocalPosition(rightHandNode);

        Vector3 leftHandMovement = leftHandCurrentPos - leftHandPrevPos;
        Vector3 rightHandMovement = rightHandCurrentPos - rightHandPrevPos;

        float movementMagnitude = (leftHandMovement.magnitude + rightHandMovement.magnitude) / 2;

        Vector3 forwardDirection = Camera.main.transform.forward;
        forwardDirection.y = 0; // Ensure movement is parallel to the ground
        forwardDirection.Normalize();

        Vector3 moveDirection = forwardDirection * movementMagnitude * speedMultiplier;

        // Use Rigidbody to move the character, which respects collisions and gravity
        rb.MovePosition(rb.position + moveDirection * Time.deltaTime);

        // Keep the player's upright orientation
        Vector3 currentRotation = transform.eulerAngles;
        rb.MoveRotation(Quaternion.Euler(0f, currentRotation.y, 0f));

        leftHandPrevPos = leftHandCurrentPos;
        rightHandPrevPos = rightHandCurrentPos;
    }

    private Vector3 GetLocalPosition(XRNode node)
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(node);
        if (device.TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 position))
        {
            return position;
        }
        return Vector3.zero;
    }

    private XRController SetupController(XRNode node)
    {
        GameObject controllerObj = new GameObject(node.ToString());
        XRController controller = controllerObj.AddComponent<XRController>();
        controller.controllerNode = node;
        return controller;
    }
}
