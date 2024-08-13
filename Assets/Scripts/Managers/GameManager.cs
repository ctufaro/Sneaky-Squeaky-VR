using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] toggleObjects; // Array of GameObjects to toggle visibility (e.g., hands in VR)

    void Start()
    {
        CheckDebugMode();
    }

    private void ToggleVisibility(bool isVisible)
    {
        foreach (GameObject obj in toggleObjects)
        {
            if (obj != null)
            {
                obj.SetActive(isVisible);
            }
        }
        Debug.Log($"Toggled visibility of specified objects to {(isVisible ? "visible" : "hidden")}.");
    }

    private void CheckDebugMode()
    {
        if (DebugConfig.Instance.isDebuggingOnPC)
        {
            Debug.Log("Debugging on PC - hiding specific objects.");
            ToggleVisibility(false); // Hide objects when debugging on PC
        }
        else
        {
            Debug.Log("Not debugging on PC - applying normal settings.");
            ToggleVisibility(true); // Ensure objects are visible when not debugging
        }
    }
}
