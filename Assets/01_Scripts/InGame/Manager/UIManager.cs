using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
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
        [SerializeField] private Image joystick;

        [Header("New Stage UI")] 
        [SerializeField] private UIInfo UI_NewStage;
        [SerializeField] private TextMeshProUGUI _stageText;
        [SerializeField] private float _newStageUIDisplayDuration = 1.5f;
        
        [Header("StageClear UI")]
        [SerializeField] private UIInfo UI_StageClear;
        [SerializeField] private float _stageClearUIDisplayDuration = 1;

        [Header("ShowStageChangeEvent UI")]
        [SerializeField] private UIInfo UI_ShowStageChangeEvent;
        [SerializeField] private UIInfo UI_Loading;
        [SerializeField] private Image _loadingGauge;
        [SerializeField] private UIInfo UI_Tip;
        [SerializeField] private TextMeshProUGUI _tipText;
        [Range(1,3)]
        [SerializeField] private float loadingDuration = 1.5f;

        [Header("BossUI")] [SerializeField] private BossBar _bossBar;
        public float LoadingDuration
        {
            get
            {
                return loadingDuration;
            }
        }
        
        [Header("Infection UI")]
        [SerializeField] private UIInfo UI_Infection;
        [SerializeField] private TextMeshProUGUI _infectionText;
        [SerializeField] private Image _infectionGauge;
        [SerializeField] private float _displayDuration = 1f;

        [Header("GameOver UI")] 
        [SerializeField] private UIInfo UI_GameOver;
        private void Start()
        {
            Player.OnPlayerHpChangedEvent += RefreshHpGauge;
            
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
        
        public void JoyStickEnable(bool enable)
        {
            joystick.enabled = enable;
        }

        public void ShowStageChangeEvent()
        { // 스테이지 넘어가는 경계 재생
            StartCoroutine(ShowStageChangeEventCoroutine());
        }
        private IEnumerator ShowStageChangeEventCoroutine()
        {
            UI_ShowStageChangeEvent.MoveOn();
            UI_Loading.MoveOn();
            UI_Tip.MoveOn();
            yield return new WaitForSeconds(loadingDuration);
            UI_ShowStageChangeEvent.MoveOff();
            UI_Loading.MoveOff();
            UI_Tip.MoveOff();
        }

        [ContextMenu("ShowStageClear")]
        public void ShowRoomClear()
        { // 룸 클리어 클리어
            StartCoroutine(ShowRoomClearRoutine());
            
        }

        private IEnumerator ShowRoomClearRoutine()
        {
            UI_StageClear.MoveOn();
            UI_Infection.MoveOff();
            yield return new WaitForSeconds(_stageClearUIDisplayDuration);
            UI_StageClear.MoveOff();


        }

        public void ShowInfectionAlert(int infectLevel)
        { // 감염도 경고창
            InfectGaugeColor(infectLevel);
            GameManager.Instance._RenderingManager.SetGlobalLightColor(infectLevel);
            DOTween.To(() => _infectionGauge.fillAmount, 
                x => _infectionGauge.fillAmount = x, 
                infectLevel * 0.01f,
                    1f);
            _infectionText.text = $"{infectLevel}%";
            InfectionActive(true);
        }
        
        public void InfectionActive(bool active)
        {
            if (active)
            {
                UI_Infection.MoveOn();
            }
            else
            {
                UI_Infection.MoveOff();
            }
        }
        
        private void InfectGaugeColor(float infectLevel)
        {
            switch (infectLevel)
            {
                case < 25f:
                    _infectionGauge.color = new Color32(84, 255, 130, 255);
                    break;
                case > 25f and < 50f:
                    _infectionGauge.DOColor(new Color32(255, 255, 46, 255), 1f);
                    break;
                case > 50f and < 75f:
                    _infectionGauge.DOColor(new Color32(255, 140, 46, 255), 1f);
                    break;
                default:
                    _infectionGauge.DOColor(new Color32(255, 46, 46, 255), 1f);
                    break;
            }
        }
        


        public void ShowNewStageUI(int currentChapter, int currentStage)
        { // 스테이지 전환
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
