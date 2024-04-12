using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableUI : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private RectTransform parentTransform;
    private Canvas canvas;
    private RectTransform rectTransform;
    private bool isDragging;
    private CanvasGroup canvasGroup;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = transform.root.GetComponent<Canvas>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        isDragging = true;
        transform.SetParent(canvas.transform);
        transform.SetAsLastSibling();
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / GetComponentInParent<Canvas>().scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false;
        
        if (eventData.pointerCurrentRaycast.gameObject.GetComponent<DropableUI>() != null)
        {
            Debug.Log("DropableUI is not null");
            rectTransform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform);
        }
        else
        {
            Debug.Log("DropableUI is null");
            rectTransform.SetParent(parentTransform);
        }
        rectTransform.anchoredPosition = Vector2.zero;
        canvasGroup.blocksRaycasts = true;
    }
}
