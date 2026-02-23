using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

    [SerializeField] private Image timeBar;
    [SerializeField] private float minTime = 0f;
    public float currentTime;

    public float maxTime;
    public float speedMultiplier = 1f;
    public bool isTimerStarted;
    public GameObject bar;

    public PanelManager panelManager;

    private void Start()
    {
        isTimerStarted = false;
        //StartTimer();
    }

    // Update is called once per frame
    void Update()
    {
        if(isTimerStarted)
        {
            if (currentTime > minTime)
            {
                currentTime -= Time.deltaTime * speedMultiplier;
                timeBar.fillAmount = currentTime / maxTime;

            }
            else
            {
                ClearTimeBar();
                panelManager.GetGameOver();

            }
        }
    }

    public void ClearTimeBar() //cambiar nombre a gameover
    {
        bar.SetActive(false);
        Time.timeScale = 0f;
    }

    public void StartTimer()
    {
        Time.timeScale = 1f;
        currentTime = maxTime;
        bar.SetActive(true);
        isTimerStarted = true;
    }

    public void TimePenalty()
    {
        Debug.Log("penalty");
        //currentTime--;
    }
}
