using System;
using System.Collections;
using System.Collections.Generic;
using EntityManage;
using TMPro;
using UIManage;
using UnityEngine;
using UnityEngine.UI;

namespace InGameScene
{
    
    public class UIManager : MonoBehaviour
    {
        [Header("Player UI")]
        [SerializeField] private Image hp_gauge;
        [SerializeField] private Image attackButton;

        [Header("New Stage UI")] 
        [SerializeField] private UIInfo UI_NewStage;
        [SerializeField] private TextMeshProUGUI _stageText;
        [SerializeField] private float _newStageUIDisplayDuration = 1.5f;
        
        [Header("StageClear UI")]
        [SerializeField] private UIInfo UI_StageClear;
        [SerializeField] private float _stageClearUIDisplayDuration = 1;

        [Header("Infection UI")]
        [SerializeField] private UIInfo UI_Infection;
        [SerializeField] private TextMeshProUGUI _infectionText;
        [SerializeField] private float _displayDuration = 1f;

        [Header("GameOver UI")] [SerializeField]
        private UIInfo UI_GameOver;
        private void Start()
        {
            Player.OnPlayerHpChanged += RefreshHpGauge;
            
        }

        public void RefreshHpGauge()
        {
            Status playerStatus = GameManager.Instance._Player.Status;
            float t = (float)playerStatus.hp / playerStatus.hpMax;
            hp_gauge.fillAmount = Mathf.Clamp01(t);
            
        }
        public void RefreshUIs()
        {
            
        }

        [ContextMenu("ShowStageClear")]
        public void ShowStageClear()
        {
            StartCoroutine(ShowStageClearRoutine());
            
        }

        private IEnumerator ShowStageClearRoutine()
        {
            UI_StageClear.MoveOn();
            yield return new WaitForSeconds(_stageClearUIDisplayDuration);
            UI_StageClear.MoveOff();


        }

        public void ShowInfectionAlert(int infectLevel)
        {
            _infectionText.text =
                $"[Warning] \n<size=32>감염도가 <size=64>{infectLevel}%</size> 에 도달했습니다</size>";
            StartCoroutine(ShowInfectionAlertRoutine());

        }

        private IEnumerator ShowInfectionAlertRoutine()
        {
            UI_Infection.MoveOn();
            yield return new WaitForSeconds(_displayDuration);
            UI_Infection.MoveOff();
        }


        public void ShowNewStageUI(int currentChapter, int currentStage)
        {
            StartCoroutine(ShowNewStageUICoroutine(currentChapter, currentStage));
        }

        private IEnumerator ShowNewStageUICoroutine(int currentChapter, int currentStage)
        {
            _stageText.text = $"Stage {currentChapter}-{currentStage}";
            UI_NewStage.MoveOn();
            yield return new WaitForSeconds(_newStageUIDisplayDuration);
            UI_NewStage.MoveOff();
        }
        
        
    }
}
