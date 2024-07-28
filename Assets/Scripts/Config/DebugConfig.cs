using UnityEngine;

[CreateAssetMenu(fileName = "DebugConfig", menuName = "Config/DebugConfig")]
public class DebugConfig : ScriptableObject
{
    public bool isDebuggingOnPC;

    private static DebugConfig instance;

    public static DebugConfig Instance
    {
        get
        {
            if (instance == null)
            {
                instance = Resources.Load<DebugConfig>("DebugConfig");
            }
            return instance;
        }
    }
}
