using System;
using SkillManage;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSkillManager : MonoBehaviour
{
    public PlayerSkill currentSkill;
    [SerializeField] private Button _SkillButton;
    [SerializeField] private Image _skillIconImage;
    
    private bool _isPassive;
    private float _coolTime;
    private float _duration;
    
    private float _currentTime;
    private Player _player;
    private bool _isSkillActivated;
    public bool IsCoolDowned => _currentTime >= _coolTime;

    private void Awake()
    {
        _player = GetComponent<Player>();
        _SkillButton.onClick.AddListener(UseSkill);
        
    }
    
    public void ApplySkill(PlayerSkill skillSO)
    {
        currentSkill = skillSO;
        _duration = skillSO.skillDuration;
        _coolTime = skillSO.coolTime;
        _isPassive = skillSO.isPassive;
        _skillIconImage.sprite = skillSO.skillIcon;
        currentSkill.SetSkillValue(_player);
        
    }

    private void Update()
    {
        if (TimeManager.TimeScale == 0)
            return;
        if (_isPassive)
        {  // 패시브 스킬일 시 그냥 실행함
            currentSkill.UpdateSkill();
            return;
        }

        RefreshSkill();
        if (_isSkillActivated)
        {
            _currentTime -= Time.deltaTime * TimeManager.TimeScale;

            currentSkill.UpdateSkill();
            if(_currentTime <= 0)
                EndSkill();
            return;
        }
        _currentTime += Time.deltaTime * TimeManager.TimeScale;
        

    }

    private void RefreshSkill()
    {
        float ratio = Mathf.Clamp01(_currentTime / _coolTime);

        _skillIconImage.fillAmount = ratio;
        
    }


    public void UseSkill()
    {
        if (_isSkillActivated) return;
        if (IsCoolDowned )
        {
            _isSkillActivated = true;
            _currentTime = _duration;
            currentSkill.ActiveSkill();

        }
    }

    private void EndSkill()
    {
        _isSkillActivated = false;
        _currentTime = 0;
    }

}
