using UnityEngine;
using DG.Tweening;

namespace MainLobby
{
    public class Laptop : InteractionObject
    {
        [Space(20)]
        [SerializeField] private RectTransform laptopPanel;
        [SerializeField] private Ease ease;
        [SerializeField] private float duration;
        public override void Interact()
        {
            base.Interact();
            Debug.Log("Laptop Interact");
            laptopPanel.DOLocalMoveY(-0.3f, duration).SetEase(ease);
            CameraManager.Instance.IsOn = true;
        }
    }
}
