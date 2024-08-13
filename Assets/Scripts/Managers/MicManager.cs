using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;

public class MicrophonePermission : MonoBehaviour
{
    public Image colorBar; // Reference to the Image component
    public TMPro.TextMeshProUGUI statusText; // Reference to the status text UI
    public float updateInterval = 0.1f;  // Slightly less frequent updates for stability
    public float threshold = 0.02f;  // Higher threshold to filter out background noise
    public float sensitivity = 5.0f; // Lower sensitivity factor to prevent maxing out
    public float smoothFactor = 0.7f; // Higher smoothing factor for gradual response

    private string microphone;
    private AudioClip micClip;
    private float[] samples;
    private float micVolume;
    private float maxSoundLevel = 1.0f;
    private bool isMicrophoneInitialized = false;
    private int sampleRate = 44100; // Default sample rate
    private float smoothVolume = 0f; // Smoothed volume for smooth transitions

    void Start()
    {
        if (!Permission.HasUserAuthorizedPermission(Permission.Microphone))
        {
            Permission.RequestUserPermission(Permission.Microphone);
        }
        else
        {
            InitializeMicrophone();
        }

        InvokeRepeating("CheckMicrophonePermission", 1f, updateInterval);
    }

    void CheckMicrophonePermission()
    {
        if (Permission.HasUserAuthorizedPermission(Permission.Microphone) && !isMicrophoneInitialized)
        {
            InitializeMicrophone();
            CancelInvoke("CheckMicrophonePermission");
        }
    }

    void InitializeMicrophone()
    {
        string targetMicrophone = null;

        // Loop through the available devices to find the headset microphone
        foreach (string device in Microphone.devices)
        {
            if (device.ToLower().Contains("headset") && device.ToLower().Contains("virtual")) // Adjust based on your target headset's name
            {
                targetMicrophone = device;
                break;
            }
        }


        // Fallback to the first available device if the headset isn't found
        if (targetMicrophone == null && Microphone.devices.Length > 0)
        {
            targetMicrophone = Microphone.devices[0];
            Debug.LogWarning("Headset microphone not found, using default: " + targetMicrophone);
        }

        if (targetMicrophone != null)
        {
            microphone = targetMicrophone;
            micClip = Microphone.Start(microphone, true, 1, sampleRate);
            samples = new float[sampleRate / 20]; // Buffer for 0.05 second of audio at sampleRate
            isMicrophoneInitialized = true;
            InvokeRepeating("UpdateMicrophone", 1f, updateInterval); // Start updating after 1 second delay
        }
        else
        {
            Debug.LogError("No microphone devices found!");
        }

    }

    void UpdateMicrophone()
    {
        if (isMicrophoneInitialized && Microphone.IsRecording(microphone))
        {
            int micPosition = Microphone.GetPosition(microphone);

            if (micPosition > samples.Length || (micPosition == 0 && samples.Length < micClip.samples))
            {
                int startPosition = micPosition - samples.Length;
                if (startPosition < 0)
                {
                    startPosition = 0;
                }

                micClip.GetData(samples, startPosition);
                micVolume = CalculateRMS(samples) * sensitivity; // Apply sensitivity factor

                // Smooth the volume transition with a smoother release
                smoothVolume = Mathf.Lerp(smoothVolume, micVolume, smoothFactor);

                UpdateMeter(smoothVolume);
            }
        }
    }

    float CalculateRMS(float[] samples)
    {
        float sum = 0;
        for (int i = 0; i < samples.Length; i++)
        {
            sum += samples[i] * samples[i];
        }
        float rms = Mathf.Sqrt(sum / samples.Length);
        return rms;
    }

    void UpdateMeter(float volume)
    {
        // Apply threshold to filter out background noise
        if (volume < threshold)
        {
            volume = 0;
        }

        float normalizedVolume = Mathf.Clamp01(volume / maxSoundLevel);

        // Convert the normalized volume to a decibel scale
        float decibels = 20 * Mathf.Log10(Mathf.Max(normalizedVolume, 0.0001f)); // Avoid log of zero

        // Set the fill amount of the color bar based on the volume
        colorBar.fillAmount = normalizedVolume;

        // Update status text with fun descriptions based on the volume level
        statusText.text = GetFunText(normalizedVolume);
    }

    string GetFunText(float normalizedVolume)
    {
        if (normalizedVolume < 0.2f)
        {
            return "Whisper";
        }
        else if (normalizedVolume < 0.4f)
        {
            return "Talking";
        }
        else if (normalizedVolume < 0.6f)
        {
            return "Speaking Up";
        }
        else if (normalizedVolume < 0.8f)
        {
            return "Loud";
        }
        else
        {
            return "Shouting";
        }
    }
}
