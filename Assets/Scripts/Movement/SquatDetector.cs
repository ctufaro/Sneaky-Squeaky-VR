using UnityEngine;

public class SquatDetector : MonoBehaviour
{
    public Transform headset;  // Reference to the VR headset
    public Transform leftController;  // Reference to the left controller
    public Transform rightController;  // Reference to the right controller
    public AudioSource fartSound;  // Reference to the AudioSource component for playing fart sounds

    private float squatMinHeight = 0.5f;
    private float squatMaxHeight = 1.2f;
    private bool isSquatting = false;  // To track squat state

    void Update()
    {
        DetectSquatting();
    }

    private void DetectSquatting()
    {
        float headsetHeight = headset.position.y;
        float leftControllerHeight = leftController.position.y;
        float rightControllerHeight = rightController.position.y;

        if (headsetHeight > squatMinHeight && headsetHeight < squatMaxHeight &&
            leftControllerHeight < headsetHeight && rightControllerHeight < headsetHeight)
        {
            if (!isSquatting)
            {
                isSquatting = true;
                Debug.Log("Player is squatting");
                PlayFartSound();
            }
        }
        else
        {
            isSquatting = false;
        }
    }

    private void PlayFartSound()
    {
        if (fartSound != null)
        {
            fartSound.Play();
        }
        else
        {
            Debug.LogWarning("Fart sound AudioSource is not assigned.");
        }
    }
}
