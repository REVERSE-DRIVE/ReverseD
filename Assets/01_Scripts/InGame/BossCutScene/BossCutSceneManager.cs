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

    public void SetBossText(int index)
    {
        bossNameText.text = bossCutsceneSets[index].bossName;
        bossNameText.font = bossCutsceneSets[index].font;
        bossNameText.color = bossCutsceneSets[index].bossNameColor;

        bossImage.sprite = bossCutsceneSets[index].bossImage;
        bossImage.SetNativeSize();
        bossImage.rectTransform.sizeDelta = new Vector2(bossImage.rectTransform.sizeDelta.x * 0.5f, bossImage.rectTransform.sizeDelta.y * 0.5f);
    }
}
