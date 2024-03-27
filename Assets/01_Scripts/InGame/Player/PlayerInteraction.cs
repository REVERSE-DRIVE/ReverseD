using System;
using InGameScene;
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
            targetObject.InteractionUnDetectEvent();
            GameManager.Instance._UIManager.AttackButtonOn();
            //UnDetectInteraction();

            isDetected = false;
            targetObject = null;
            return;
        }
        
        if (!isDetected && hit != null)
        {
            isDetected = true;
            targetObject = hit.GetComponent<InteractionObject>();
            targetObject.InteractionDetectEvent();
            GameManager.Instance._UIManager.InteractionButtonOn();
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