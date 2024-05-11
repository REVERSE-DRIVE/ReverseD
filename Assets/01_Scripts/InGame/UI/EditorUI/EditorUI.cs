using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EditorUI : MonoBehaviour
{
    private CanvasGroup _canvasGroup;

    [SerializeField] private EditorOptionUI _currentSelectedOption;
    
    [Header("UI")]

    [SerializeField] private TextMeshProUGUI _useResourceAmountText;
    [SerializeField] private Button _resourceRemoveBtn;
    [SerializeField] private Button _resourceAddBtn;
    private int useResourceAmount = 0;
    
    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        
        
        
    }


    public void SetEditorUI(bool value)
    {
        _canvasGroup.alpha = value ? 1 : 0;
        _canvasGroup.interactable = value;
        _canvasGroup.blocksRaycasts = value;
    }

    public void AddResource()
    {
        
    }

    public void RemoveResource()
    {
        if()
    }
    
}
