using System;
using System.Collections;
using System.Collections.Generic;
using InGameScene;
using UnityEngine;

public class EditorObject : InteractionObject
{
    private void Start()
    {
        InteractionEvent.AddListener(HandleShowEditorUI);
    }

    public void HandleShowEditorUI()
    {
        GameManager.Instance._UIManager.ShowEditor();
        
    }
}
