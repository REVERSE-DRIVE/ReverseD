using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BossCutSceneManager : MonoBehaviour
{
    [Header("Boss Setting")]
    [SerializeField] private BossCutsceneSet[] bossCutsceneSets;
    
    [Space(10)]
    [SerializeField] private Image bossImage;
    [SerializeField] private TextMeshProUGUI bossNameText;
    
    private SoundObject _soundObject;

    private void Awake()
    {
        _soundObject = GetComponent<SoundObject>();
    }

    private void Start()
    {
        SetBossText(4);
    }

    /**
     * <summary>
     * 보스 컷씬 기본 설정 적용
     * </summary>
     * <param name="index">보스 번호</param>
     */
    public void SetBossText(int index)
    {
        _soundObject.PlayAudio(1);
        bossNameText.text = bossCutsceneSets[index].bossName;
        bossNameText.font = bossCutsceneSets[index].font;
        bossNameText.color = bossCutsceneSets[index].bossNameColor;

        //bossImage.sprite = bossCutsceneSets[index].bossImage;
        bossImage.SetNativeSize();
        bossImage.rectTransform.sizeDelta = new Vector2(bossImage.rectTransform.sizeDelta.x * 7f, bossImage.rectTransform.sizeDelta.y * 7f);
    }
}
