 using UnityEngine;

public class GameManager : MonoBehaviour
{
    void Start()
    {
        CheckDebugMode();
    }

    private void CheckDebugMode()
    {
        if (DebugConfig.Instance.isDebuggingOnPC)
        {
            Debug.Log("Debugging on PC - applying debug-specific settings.");
            // Apply any debug-specific behavior or settings here
        }
        else
        {
            Debug.Log("Not debugging on PC - applying normal settings.");
            // Apply normal behavior or settings here
        }
    }
}
