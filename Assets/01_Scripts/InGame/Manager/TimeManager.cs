using System;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public static float TimeScale { get; set; } = 1f;
}