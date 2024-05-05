using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] private RectTransform _DialoguePanelRectTrm;
    [SerializeField] private RectTransform _CharacterImagePanelRectTrm;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _contentText;

    [Header("Setting Values")] 
    [SerializeField] private float _dialoguePanelShowDuration = 0.3f;
    [SerializeField] private float _contentPrintingTerm = 0.1f; 
    
    
    private CanvasGroup _canvasGroup;


    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }


    public void PrintContent(string content)
    {
        StartCoroutine(PrintCoroutine(content));
    }

    private IEnumerator PrintCoroutine(string content)
    {
        _contentText.text = "";
        WaitForSeconds ws = new WaitForSeconds(_contentPrintingTerm);
        for (int i = 0; i < content.Length; i++)
        {
            _contentText.text += content[i];
            yield return ws;
        }
    }


    [ContextMenu("DebugOnPanel")]
    public void DebugOnPanel()
    {
        OnDialoguePanel();
    }

    [ContextMenu("DebugOffPanel")]
    public void DebugOffPanel()
    {
        OffDialoguePanel();
    }
    
    public void OnDialoguePanel()
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(_canvasGroup.DOFade(1f, _dialoguePanelShowDuration))
            .Join(_CharacterImagePanelRectTrm.DOAnchorPosX(-500, _dialoguePanelShowDuration))
            .AppendCallback(() => SetCanvas(true));

    }
    
    public void OffDialoguePanel()
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(_canvasGroup.DOFade(0f, _dialoguePanelShowDuration))
            .Join(_CharacterImagePanelRectTrm.DOAnchorPosX(500, _dialoguePanelShowDuration))
            .AppendCallback(() => SetCanvas(false));

    }

    private void SetCanvas(bool value)
    {
        _canvasGroup.interactable = value;
        _canvasGroup.blocksRaycasts = value;
    }
    
    
}
