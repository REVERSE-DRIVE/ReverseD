using System;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public event Action interactionEvent;
    [SerializeField] private LayerMask interactionObjectLayerMask;
    [SerializeField] private float interactRange = 1.3f;
    [SerializeField] private bool isDetected = false;

    private InteractionObject targetObject;

    public bool IsDetected
    {
        get { return isDetected; }
        private set { }
    }
    
    private void CheckInteractionObject()
    {
        Collider2D hit = Physics2D.OverlapCircle(
            transform.position, interactRange, 
            interactionObjectLayerMask);

        if (hit == null)
        {
            isDetected = false;
            targetObject = null;
            return;
        }

        targetObject = hit.transform.GetComponent<InteractionObject>();
        interactionEvent = targetObject.Interact;

    }

    public void Interaction()
    {
        interactionEvent?.Invoke();
        
    }
}