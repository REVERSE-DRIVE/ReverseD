using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] private RectTransform _DialoguePanelRectTrm;
    [SerializeField] private RectTransform _CharacterImagePanelRectTrm;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _contentText;

    [Header("Setting Values")] 
    [SerializeField] private float contentPrintingTerm = 0.1f; 
    
    
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
        WaitForSeconds ws = new WaitForSeconds(contentPrintingTerm);
        for (int i = 0; i < content.Length; i++)
        {
            _contentText.text += content[i];
            yield return ws;
        }
    }
    

    public void OnDialoguePanel()
    {
        SetCanvas(true);
    }
    

    public void OffDialoguePanel()
    {
        SetCanvas(false);
    }

    private void SetCanvas(bool value)
    {
        _canvasGroup.interactable = value;
        _canvasGroup.blocksRaycasts = value;
    }
}
