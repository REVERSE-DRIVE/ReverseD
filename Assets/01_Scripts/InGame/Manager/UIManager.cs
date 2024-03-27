using System;
using System.Collections;
using System.Collections.Generic;
using EntityManage;
using UnityEngine;
using UnityEngine.UI;

namespace InGameScene
{
    
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private Image hp_gauge;
        [SerializeField] private Image attackButton;

        private void Start()
        {
            Player.OnPlayerHpChanged += RefreshHpGauge;
        }

        public void RefreshHpGauge()
        {
            Status playerStatus = GameManager.Instance._Player.Status;
            float t = playerStatus.hp / playerStatus.HpMax;
            hp_gauge.fillAmount = Mathf.Clamp01(t);
            
        }
        public void RefreshUIs()
        {
            
        }
        
        
    }
}
