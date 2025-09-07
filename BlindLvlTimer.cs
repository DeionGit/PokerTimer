using UnityEngine;
using TMPro;

public class BlindLvlTimer : MonoBehaviour
{
    [SerializeField] AudioSource blindAudioSource;

    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI blindLvlText;

    [SerializeField] float remainingTime = 0;
    [SerializeField] float blindLvlTime = 60f;
    [SerializeField] int blindLevel = 1;
    bool timerOn = false;
    void Start()
    {
        remainingTime = blindLvlTime;
        SetTimer();
        SetBlindLvl(blindLevel);
    }

    // Update is called once per frame
    void Update()
    {
        SetTimer();
    }

    public void PlayTimerBlind()
    {
        timerOn = true;
        ScreenController.instance.AvoidScreenSleeping();
    }
    public void ResetTimer()
    {
        blindLevel = 1;
        SetBlindLvl(blindLevel);

        timerOn = false;
        remainingTime = blindLvlTime;
        SetTimer();

        ScreenController.instance.LetScreenSleep();
    }
    public void SetTimer()
    {

        if(remainingTime>0 && timerOn) 
        {
            remainingTime -= Time.deltaTime;
        }else if(remainingTime < 0 && timerOn)
        {
            remainingTime = blindLvlTime;
            blindLevel++;
            blindAudioSource.Play();
            SetBlindLvl(blindLevel);
        }
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}",minutes,seconds);
    }

    public void AddMinute()
    {
        if (!timerOn) 
        {
            remainingTime += 60f;
            blindLvlTime = remainingTime;
            SetTimer() ;
        } 
    }
    public void RemoveMinute()
    {
        if (!timerOn)
        {
            if (remainingTime > 0)
            {
                remainingTime -= 60f;
                blindLvlTime = remainingTime;
                SetTimer();
            }
        }
    }

    void SetBlindLvl(int lvl)
    {
        blindLvlText.text = "Blind Lvl " + lvl;
    }
}
