using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageChanger : MonoSingleton<ImageChanger>
{
    [SerializeField] private Image _axisImage;
    [SerializeField] private Image _bodyImage;
    [SerializeField] private Image _legImage;
    [SerializeField] private Image _legImage2;
    [SerializeField] private List<Sprite> _axisSpriteList;
    [SerializeField] private List<Sprite> _bodySpriteList;
    [SerializeField] private List<Sprite> _legSpriteList;


    public void ChangeAxisImage(AxisType axisType)
    {
        _axisImage.sprite = _axisSpriteList[(int)axisType];
    }
    
    public void ChangeBodyImage(BodyType bodyType)
    {
        _bodyImage.sprite = _bodySpriteList[(int)bodyType];
    }
    
    public void ChangeLegImage(LegType legType)
    {
        _legImage.sprite = _legSpriteList[(int)legType * 2];
        _legImage2.sprite = _legSpriteList[(int)legType * 2 + 1];
    }
}