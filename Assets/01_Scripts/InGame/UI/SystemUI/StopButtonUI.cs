using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StopButtonUI : MonoBehaviour
{
    private Button _stopButton;

    private void Awake()
    {
        _stopButton = GetComponent<Button>();
        _stopButton.onClick.AddListener(Pause);
    }

    public void Pause()
    {
        TimeManager.TimeScale = 0;
    }

    public void Continue()
    {
        TimeManager.TimeScale = 1;
    }
}
