using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicTimerFaster : MonoBehaviour
{
    public Timer timerscp;
    public float maxTime;
    public float minTime;

    void OnEnable()
    {
        Debug.Log("TIMER");
        timerscp.speedMultiplier = maxTime;
    }

    void OnDisable()
    {
        timerscp.speedMultiplier = minTime;
    }
}
