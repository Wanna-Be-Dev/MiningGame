using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager
{
    public static Action<int> OnAddScore;

    public static UnityEvent<float> onTriggerEnter = new UnityEvent<float>();
    public static void SendScore(int points)
    {
        if(OnAddScore != null) OnAddScore.Invoke(points);
    }

    public static void CloseFinish(float distance)
    {
        onTriggerEnter.Invoke(distance);
    }
}
