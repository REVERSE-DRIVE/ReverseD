using System;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class UpgradeInventoryMove : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Ease ease;
    [SerializeField] private bool isInventoryOpen;
    [SerializeField] GameObject inventory;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (isInventoryOpen)
        {
            isInventoryOpen = false;
            transform.DOLocalMove(new Vector3(960, -550, 0), 0.5f).SetEase(ease);
            inventory.gameObject.transform.DOLocalMove(new Vector3(0, -800, 0), 0.5f).SetEase(ease);
        }
        else
        {
            isInventoryOpen = true;
            transform.DOLocalMove(new Vector3(960, -300, 0), 0.5f).SetEase(ease);
            inventory.gameObject.transform.DOLocalMove(new Vector3(0, -550, 0), 0.5f).SetEase(ease);
        }
    }
}
