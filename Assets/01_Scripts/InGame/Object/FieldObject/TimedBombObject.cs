using System;
using System.Collections;
using UnityEngine;

public class TimedBombObject : BombObject
{
    [Header("Timed Setting")] [SerializeField]
    private float _limitedTime = 5;


    private void OnEnable()
    {
        throw new NotImplementedException();
    }
    
    private IEnumerator
}