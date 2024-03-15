using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float disableTime = 0.5f;

    private GameObject parent;
    private void OnEnable()
    {
        StartCoroutine(Disable());
        parent = GameObject.Find("Player").transform.GetChild(1).gameObject;
    }

    private IEnumerator Disable()
    {
        float time = 0;
        while (true)
        {
            time += Time.deltaTime;
            if (time > disableTime)
            {
                transform.parent = parent.transform;
                gameObject.SetActive(false);
                yield break;
            }
            transform.Translate(Vector3.right * (_speed * Time.deltaTime));
            yield return null;


        }
    }
}
