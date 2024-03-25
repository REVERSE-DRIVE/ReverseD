using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace InGameScene
{
    
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private Image hp_gauge;
        [SerializeField] private Image attackButton;
        
        [SerializeField] private Sprite[] attackButtonSprite;

        public void RefreshHpGauge()
        {
            //float t = GameManager.Instance._PlayerTransform.GetComponent<Player>().
            //hp_gauge.fillAmount = Mathf.Clamp01();
            
        }
        public void RefreshUIs()
        {
            
        }
        
        public void RefreshAttackButton()
        {
            attackButton.sprite = GameManager.Instance._PlayerTransform.GetComponent<PlayerInteraction>().IsDetected
                ? attackButtonSprite[1]
                : attackButtonSprite[0];
        }
    }
}
