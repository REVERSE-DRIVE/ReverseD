using System;
using UnityEngine;

public class TimeManager : MonoSingleton<TimeManager>
{
    [SerializeField] private static float _timeScale = 1f;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public static float TimeScale
    {
        get => _timeScale;
        set => _timeScale = value;
    }
}