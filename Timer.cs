using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    float time = 0;
    public float maxTime = 15;
    bool timerOn=false;

    public TextMeshProUGUI counterText;
    [SerializeField] AudioSource timerAudioSouce;
    private void Start()
    {
        ResetTimer();
    }
    private void Update()
    {
        if (timerOn)
        {
            time -= Time.deltaTime;

            counterText.text = Mathf.RoundToInt(time).ToString()+"s";

            if (time <= 0)
            {
                timerOn = false;
                ResetTimer();
                timerAudioSouce.Play();
            }
        }
    }

    public void SetMaxTime(float TimeM)
    {
        maxTime= TimeM;
    }

    public void PlayTimer()
    {
        timerOn = true;
        ScreenController.instance.AvoidScreenSleeping();
    }

    public void ResetTimer()
    {
        time = maxTime;
        counterText.text = Mathf.RoundToInt(time).ToString() + "s";

    }

    public void StopTimer() 
    {
        timerOn = false;
        ScreenController.instance.LetScreenSleep();

        ResetTimer();
    }
    public void AddTime() 
    {
        if (!timerOn)
        {
            maxTime += 1;
            ResetTimer();
        }
        
    }
    public void RemoveTime() 
    {
        if (!timerOn)
        {
            if (maxTime > 5)
            {
                maxTime -= 1;
                ResetTimer();
            }
            
        }
            
    }
}
