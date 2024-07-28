using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;

public class MicrophonePermission : MonoBehaviour
{
    public Image colorBar; // Reference to the Image component
    public Text statusText; // Reference to the status text UI
    public float updateInterval = 0.05f;  // More frequent updates for real-time feedback
    public float threshold = 0.001f;  // Lower threshold for higher sensitivity
    public float sensitivity = 50.0f; // Higher sensitivity factor to detect even whispers
    public float smoothFactor = 0.2f; // Smoothing factor for quick release

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
        if (Microphone.devices.Length > 0)
        {
            microphone = Microphone.devices[0];
            if (microphone != null)
            {
                micClip = Microphone.Start(microphone, true, 1, sampleRate);
                samples = new float[sampleRate / 20]; // Buffer for 0.05 second of audio at sampleRate
                isMicrophoneInitialized = true;
                InvokeRepeating("UpdateMicrophone", 1f, updateInterval); // Start updating after 1 second delay
            }
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
        float normalizedVolume = Mathf.Clamp01(volume / maxSoundLevel);

        // Set the fill amount of the color bar based on the volume
        colorBar.fillAmount = normalizedVolume;

        // Update status text with the volume level
        statusText.text = $"Noise Meter: {normalizedVolume * 100:F1}%";
    }
}
