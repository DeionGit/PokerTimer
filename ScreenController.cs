using UnityEngine;

public class ScreenController : MonoBehaviour
{
    public static ScreenController instance { get; private set; }

    int timeToSleep;
    int activeRequest;

    public bool isPreventingSleep => activeRequest > 0;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        ObtainScreenTimeout();
    }

    private void ObtainScreenTimeout()
    {
        timeToSleep = Screen.sleepTimeout;
    }

    public void AvoidScreenSleeping()
    {
            activeRequest++;

            if(activeRequest == 1)
            {
                Screen.sleepTimeout = SleepTimeout.NeverSleep;
            }
    }

    public void LetScreenSleep()
    {
        if(activeRequest > 0) activeRequest--;

        if(activeRequest == 0)
        {
            Screen.sleepTimeout = timeToSleep;
        }
    }

    private void OnApplicationQuit()
    {
            Screen.sleepTimeout = timeToSleep;
    }
}
