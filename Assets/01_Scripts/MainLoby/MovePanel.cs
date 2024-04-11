using UnityEngine;
using UnityEngine.EventSystems;

public class MovePanel : MonoBehaviour, IPointerClickHandler, IDragHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        transform.parent.SetAsLastSibling();
    }
    public void OnDrag(PointerEventData eventData)
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.parent.position = new Vector3(pos.x, pos.y, 0);
        transform.parent.SetAsLastSibling();
    }
}
