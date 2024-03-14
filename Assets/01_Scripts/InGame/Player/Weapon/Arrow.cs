using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float disableTime = 0.5f;
    private void OnEnable()
    {
        StartCoroutine(Disable());
    }

    private IEnumerator Disable()
    {
        float time = 0;
        while (true)
        {
            time += Time.deltaTime;
            if (time > disableTime)
            {
                gameObject.SetActive(false);
                yield break;
            }
            transform.Translate(Vector3.right * (0.1f * _speed));
            yield return null;


        }
    }
}
