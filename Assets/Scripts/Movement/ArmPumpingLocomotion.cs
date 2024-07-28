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

    void Start()
    {
        leftController = SetupController(leftHandNode);
        rightController = SetupController(rightHandNode);
        leftHandPrevPos = GetLocalPosition(leftHandNode);
        rightHandPrevPos = GetLocalPosition(rightHandNode);
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

        transform.position += moveDirection * Time.deltaTime;

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
