using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class RenderingManager : MonoBehaviour
{
    [SerializeField] private float renderingDistance = 40;
    [SerializeField] private Transform mapGrid;
    [SerializeField] private Volume _globalVolume;
    
    [SerializeField] private Color _normalColor;
    [SerializeField] private Color _targetColor;
    [SerializeField] private Light2D _globalMapThemeLight;
    private bool onRendering;

    private void Awake()
    {
        if (mapGrid == null)
            mapGrid = GameObject.Find("Grid").transform;

    }

    public void _Start()
    {
        onRendering = true;
        

    }

    private void Update()
    {
        if (onRendering)
        {
            foreach (Transform map in mapGrid)
            {
                if (IsCancelRendering(map.position))
                {
                    map.gameObject.SetActive(false);
                }
                else
                {
                    map.gameObject.SetActive(true);
                }
            }
        }
    }

    public float DistanceToPlayer(Vector2 position)
    {
        return Vector2.Distance(GameManager.Instance._PlayerTransform.position, position); ;
    }
    
    public float DistanceToCamera(Vector2 position)
    {
        return Vector2.Distance(Camera.main.transform.position, position);
    }

    public bool IsCancelRendering(Vector2 position)
    {
        return DistanceToCamera(position) > renderingDistance;
    }
    
    public void SetGlobalLightColor(int infectLevelPercent)
    {
        float ratio = Mathf.Clamp01(infectLevelPercent / 100f);
        StartCoroutine(ColorChangeCoroutine(ratio, 1));
    }
    
    private IEnumerator ColorChangeCoroutine(float infectPercent, float duration)
    {
        float elapsedTime = 0;
        Color beforeColor = _globalMapThemeLight.color;
        Color targetColor = Color.Lerp(beforeColor, _targetColor, infectPercent);
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            _globalMapThemeLight.color = Color.Lerp(beforeColor, targetColor, elapsedTime / duration);
            yield return null;
        }
    }

    public void SetLensEffect()
    {
        
    }
}
