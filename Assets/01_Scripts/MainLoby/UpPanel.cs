using UnityEngine;
using UnityEngine.EventSystems;

public class UpPanel : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        transform.SetAsLastSibling();
    }
}
