using System;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public static float TimeScale { get; set; }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 150, 100), "TimeScale 1"))
        {
            TimeScale = 1f;
        }
        if (GUI.Button(new Rect(10, 110, 150, 100), "TimeScale 0.5"))
        {
            TimeScale = 0.5f;
        }
        if (GUI.Button(new Rect(10, 210, 150, 100), "TimeScale 0.1"))
        {
            TimeScale = 0.1f;
        }
        if (GUI.Button(new Rect(10, 310, 150, 100), "TimeScale 0"))
        {
            TimeScale = 0f;
        }
    }
}