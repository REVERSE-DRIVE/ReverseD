using System;
using UnityEngine;
using UnityEngine.UI;

public abstract class EditorOptionUI : MonoBehaviour
{
    [SerializeField] protected Button _selectButton;
    [SerializeField] protected CanvasGroup _panelGroup;

    public int _needResourceMultiple = 1;
    
    
    private void Awake()
    {
        _selectButton.onClick.AddListener(HandleOptionSelected);
    }

    private void HandleOptionSelected()
    {
        SetPanelGroup(true);
    }

    public virtual void SelectOption()
    {
        
        // 트렌스폼의 부모위치상 순서를 맨 아래로 바꾸면 된다
    }

    public virtual void UnSelectOption()
    {
        SetPanelGroup(false);
        // 선택을 해제하며 
    }

    private void SetPanelGroup(bool value)
    {
        _panelGroup.alpha = value ? 1 : 0;
        _panelGroup.interactable = value;
        _panelGroup.blocksRaycasts = value;
    }

    public void Apply()
    {
        
    }
}