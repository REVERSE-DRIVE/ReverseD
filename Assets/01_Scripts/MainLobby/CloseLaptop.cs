using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class CloseLaptop : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler
{
    [SerializeField] private RectTransform _laptopPanel;
    [SerializeField] private Vector2 _targetPos;
    [SerializeField] private Ease _ease;
    private void Awake()
    {
        _targetPos = _laptopPanel.anchoredPosition;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_laptopPanel.anchoredPosition == _targetPos) return;
        Debug.Log("Close Laptop");
        //_laptopPanel.DOAnchorPos(_targetPos, 0.5f).SetEase(_ease);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Enter");
    }
}
