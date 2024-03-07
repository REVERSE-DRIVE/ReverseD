using UnityEngine;
using UnityEngine.EventSystems;

public enum SLOT_TYPE
{
    UPGRADE,
    INVENTORY
}
public class DropableUI : MonoBehaviour, IDropHandler
{
    public SLOT_TYPE slotType;
    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount >= 1 && slotType == SLOT_TYPE.UPGRADE)
        {
            return;
        }
        RectTransform rect = eventData.pointerDrag.GetComponent<RectTransform>();
        Debug.Log("OnDrop");
        if (eventData.pointerDrag != null)
        {
            rect.anchoredPosition = Vector2.zero + new Vector2(0, 150);
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            eventData.pointerDrag.GetComponent<CanvasGroup>().blocksRaycasts = true;
            eventData.pointerDrag.transform.SetParent(transform);
        }
    }
} 
