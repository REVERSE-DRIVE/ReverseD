using System.Collections;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float disableTime = 0.5f;

    private GameObject parent;
    private Rigidbody2D _rigidbody2D;
    private void OnEnable()
    {
        parent = GameObject.Find("Player").transform.GetChild(1).gameObject;
        _rigidbody2D = GetComponent<Rigidbody2D>();
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
                transform.parent = parent.transform;
                gameObject.SetActive(false);
                yield break;
            }
            _rigidbody2D.velocity = transform.right * (_speed * TimeManager.TimeScale);
            yield return null;
        }
    }
}
