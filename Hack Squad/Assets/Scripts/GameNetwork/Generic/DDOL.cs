using UnityEngine;

/// <summary>
/// Script que se usa para decirle a Unity que no destruya el objeto al que se le adjunta este Script.
/// </summary>
public class DDOL : MonoBehaviour {

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
