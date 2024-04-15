using UnityEngine;
using UnityEngine.EventSystems;

public class DropableUI : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        RectTransform rect = eventData.pointerDrag.GetComponent<RectTransform>();
        Debug.Log("OnDrop");
        if (eventData.pointerDrag != null)
        {
            rect.anchoredPosition = Vector2.zero;
            if (eventData.pointerDrag.GetComponent<CanvasGroup>() == null) return;
            eventData.pointerDrag.GetComponent<CanvasGroup>().blocksRaycasts = true;
            rect.SetParent(transform);
        }
    }
} 
