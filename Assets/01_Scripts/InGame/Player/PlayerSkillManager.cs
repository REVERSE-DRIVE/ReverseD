using System;
using SkillManage;
using UnityEngine;

public class PlayerSkillManager : MonoBehaviour
{
    private PlayerSkillSO _currentSkillSO;
    public PlayerSkill skill;

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
    }

    private void ApplySkill(PlayerSkillSO skillSO)
    {
        _currentSkillSO = skillSO;
        _duration = skillSO.skillDuration;
        _coolTime = skillSO.coolTime;
        skill = skillSO.GetSkill;
        _isPassive = skillSO.isPassive;
        skill.SetSkillValue(_player);
    }

    private void Update()
    {
        if (TimeManager.TimeScale == 0)
            return;
        if (_isPassive)
        {
            skill.UpdateSkill();
            return;
        }
        _currentTime += Time.deltaTime * TimeManager.TimeScale;

        if (_isSkillActivated)
        {
            skill.UpdateSkill();
            if(_currentTime >= _duration)
                EndSkill();
        }
    }


    public void UseSkill()
    {
        if (IsCoolDowned)
        {
            _isSkillActivated = true;
            _currentTime = 0;
            skill.ActiveSkill();

        }
    }

    private void EndSkill()
    {
        _isSkillActivated = false;
        _currentTime = 0;
    }

}
