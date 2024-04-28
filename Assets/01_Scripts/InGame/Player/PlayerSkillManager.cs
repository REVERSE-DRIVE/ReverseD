using System;
using SkillManage;
using UnityEngine;

public class PlayerSkillManager : MonoBehaviour
{
    public PlayerSkill currentSkill;

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

    private void ApplySkill(PlayerSkill skillSO)
    {
        currentSkill = skillSO;
        _duration = skillSO.skillDuration;
        _coolTime = skillSO.coolTime;
        _isPassive = skillSO.isPassive;
        currentSkill.SetSkillValue(_player);
    }

    private void Update()
    {
        if (TimeManager.TimeScale == 0)
            return;
        if (_isPassive)
        {
            currentSkill.UpdateSkill();
            return;
        }
        _currentTime += Time.deltaTime * TimeManager.TimeScale;

        if (_isSkillActivated)
        {
            currentSkill.UpdateSkill();
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
            currentSkill.ActiveSkill();

        }
    }

    private void EndSkill()
    {
        _isSkillActivated = false;
        _currentTime = 0;
    }

}
