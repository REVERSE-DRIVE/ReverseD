using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

namespace MainLoby
{
    public class Laptop : InteractionObject
    {
        [Space(20)]
        [SerializeField] private RectTransform playerChoicePanel;
        [SerializeField] private Ease ease;
        public override void Interact()
        {
            base.Interact();
            Debug.Log("Laptop Interact");
            playerChoicePanel.DOLocalMoveX(0, 0.5f).SetEase(ease);
        }
    }
}
