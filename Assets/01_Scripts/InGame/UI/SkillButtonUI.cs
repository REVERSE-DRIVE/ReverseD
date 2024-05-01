using UnityEngine;
using UnityEngine.UI;

public class SkillButtonUI : MonoBehaviour
{
    [SerializeField] private Image _skillGaugeHandle;
    [SerializeField] private Image _skillIconBG;

    public void SetSkillIcon(Sprite icon)
    {
        _skillGaugeHandle.sprite = icon;
        _skillIconBG.sprite = icon;
    }

    public void RefreshSkillGaugeFill(float fill)
    {
        _skillGaugeHandle.fillAmount = fill;
        
    }
}
