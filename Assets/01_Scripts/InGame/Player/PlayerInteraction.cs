using System;
using InGameScene;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
    public event Action interactionEvent;
    public event Action interactionDetectEvent;
    public event Action interactionUnDetectEvent;
    
    [Header("Setting Values")]
    [SerializeField] private LayerMask interactionObjectLayerMask;
    [SerializeField] private float interactRange = 1.3f;
    [SerializeField] private bool isDetected;

    [Header("Interaction Controller")] [SerializeField]
    private Transform _interactionController;

    private Button _button;
    private Image _buttonImage;
    
    [SerializeField] private Sprite _attackButtonSprite;
    [SerializeField] private Sprite _InteractionButtonSprite;


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

    private void Start()
    {
        interactionDetectEvent += InteractionButtonOn;
        interactionUnDetectEvent += AttackButtonOn;
        
        _button = GetComponent<Button>();
        _button.onClick.AddListener(Interact);

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
            AttackButtonOn();
            UnDetectInteraction();

            isDetected = false;
            targetObject = null;
            return;
        }
        
        if (!isDetected && hit != null)
        {
            isDetected = true;
            targetObject = hit.GetComponent<InteractionObject>();
            targetObject.InteractionDetectEvent();
            InteractionButtonOn();
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
    
    
    public void AttackButtonOn()
    {
        _buttonImage.sprite = _attackButtonSprite;
    }
        
    public void InteractionButtonOn()
    {
        _buttonImage.sprite = _InteractionButtonSprite;
    }
}