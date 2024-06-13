using System;
using UnityEngine;

public class ScrollItem : MonoBehaviour
{
    [SerializeField] private PartType _partType;
    [SerializeField] private AxisType _axisType;
    [SerializeField] private BodyType _bodyType;
    [SerializeField] private LegType _legType;
    public float distance;
    private RectTransform _rect;
    private bool _isCenter;

    private void Awake()
    {
        _rect = transform as RectTransform;
    }

    private void Update()
    {
        distance = Mathf.Abs(transform.position.x);
        ScaleSize();
        
        if (distance < 3f)
        {
            if (!_isCenter)
            {
                _isCenter = true;
                switch (_partType)
                {
                    case PartType.Axis:
                        ImageChanger.Instance.ChangeAxisImage(_axisType);
                        break;
                    case PartType.Body:
                        ImageChanger.Instance.ChangeBodyImage(_bodyType);
                        break;
                    case PartType.Leg:
                        ImageChanger.Instance.ChangeLegImage(_legType);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
        else
        {
            _isCenter = false;
        }
    }

    private void ScaleSize()
    {
        float scale = 1 - (distance * 0.0001f);
        transform.localScale =
            Vector3.Lerp(transform.localScale, 
                new Vector3(scale, scale, 1), Time.deltaTime * 10f);
    }
}