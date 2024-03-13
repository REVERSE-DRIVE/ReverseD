using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
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
            if (time > 0.5f)
            {
                gameObject.SetActive(false);
                yield break;
            }
            transform.Translate(Vector3.right * 0.1f);
            yield return null;


        }
    }
}
