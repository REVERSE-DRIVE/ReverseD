using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera _virtualCamera;
    private CinemachineBasicMultiChannelPerlin _cinemachineBasicMultiChannelPerlin;

                                                                            [SerializeField] private float hitShakePower = 5;
    [SerializeField] private float hitShakeDuration = 0.1f;
    [SerializeField] private float cameraDefaultZoom = 8f;

    private Transform _defaultCameraTarget;

    private bool _isNewFollowing;
    private bool _isShaking;
    
    private void Awake()
    {
        _cinemachineBasicMultiChannelPerlin =
            _virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        _defaultCameraTarget = _virtualCamera.Follow;
    }

    public void Follow(Transform newTarget, float duration)
    {
        if (!_isNewFollowing)
        {
            StartCoroutine(FollowCoroutine(newTarget, duration));
        }
    }

    private IEnumerator FollowCoroutine(Transform newTarget, float duration = 1)
    {
        GameManager.Instance._UIManager.JoyStickEnable(false);
        _virtualCamera.Follow = newTarget;
        _isNewFollowing = true;
        yield return new WaitForSeconds(duration);
        _virtualCamera.Follow = _defaultCameraTarget;
        _isNewFollowing = false;
        GameManager.Instance._UIManager.JoyStickEnable(true);
    }

    public void ShakeHit()
    {
        Shake(hitShakePower, hitShakeDuration);
    }

    public void Shake(float shakePower, float duration)
    {
        StartCoroutine(ShakeCoroutine(shakePower, duration));
    }

    private IEnumerator ShakeCoroutine(float power, float duration)
    {
        if (_isShaking) yield break;
        _isShaking = true;
        SetShake(power, power / 2);
        yield return new WaitForSeconds(duration);
        ShakeOff();
        _isShaking = false;
    }
    

    public void SetShake(float Amplitude, float frequency)
    {
        _cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = Amplitude * TimeManager.TimeScale;
        _cinemachineBasicMultiChannelPerlin.m_FrequencyGain = frequency * TimeManager.TimeScale;

    }

    public void ShakeOff()
    {
        SetShake(0,0);
    }

    public void StageStartCameraZoomEvent()
    {
        ZoomDefault(13, 1.5f);
    }

    public void ZoomDefault()
    {
        _virtualCamera.m_Lens.OrthographicSize = cameraDefaultZoom;
    }
    public void ZoomDefault(float before, float duration)
    {
        StartCoroutine(ZoomCoroutine(before, cameraDefaultZoom, duration));
    }

    public void Zoom(float before, float after, float duration)
    {
        StartCoroutine(ZoomCoroutine(before, after, duration));
    }
    
    

    private IEnumerator ZoomCoroutine(float before, float after, float duration)
    {
        float currentTime = 0;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            float t = currentTime / duration;
            _virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(before, after, t);
            yield return null;
        }
        _virtualCamera.m_Lens.OrthographicSize = after;
    }
}