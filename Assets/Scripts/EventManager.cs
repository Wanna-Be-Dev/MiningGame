using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager
{
    public static Action<int> OnAddScore;

    public static void SendScore(int points)
    {
        if(OnAddScore != null) OnAddScore.Invoke(points);
    }
}
