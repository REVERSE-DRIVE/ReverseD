using System;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public event Action interactionEvent;
    public event Action interactionDetectEvent;
    public event Action interactionUnDetectEvent;
    
    [SerializeField] private LayerMask interactionObjectLayerMask;
    [SerializeField] private float interactRange = 1.3f;
    [SerializeField] private bool isDetected;

    
    private InteractionObject targetObject;

    /**
     * <summary>
     * 상호작용 오브젝트가 감지되었는가
     * </summary>
     */
    public bool IsDetected
    {
        get { return isDetected; }
        private set { }
    }

    private void FixedUpdate()
    {
        CheckInteractionObject();
        
    }

    
    private void CheckInteractionObject()
    {
        Collider2D hit = Physics2D.OverlapCircle(
            transform.position, interactRange, 
            interactionObjectLayerMask);


        if (isDetected && hit == null)
        {
            print("감지되지 않음");
            targetObject.InteractionUnDetectEvent();
            //UnDetectInteraction();

            isDetected = false;
            targetObject = null;
            return;
        }
        
        if (!isDetected && hit != null)
        {
            print("감지됨");
            isDetected = true;
            targetObject = hit.GetComponent<InteractionObject>();
            targetObject.InteractionDetectEvent();
            //DetectInteraction();
            interactionEvent = targetObject.Interact;
        }


    }

    public void DetectInteraction()
    {
        interactionUnDetectEvent?.Invoke();
    }

    public void UnDetectInteraction()
    {
        interactionUnDetectEvent?.Invoke();
        interactionUnDetectEvent = null;
    }

    public void Interact()
    {
        interactionEvent?.Invoke();
        
    }
}