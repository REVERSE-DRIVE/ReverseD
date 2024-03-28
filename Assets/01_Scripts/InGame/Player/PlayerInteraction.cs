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
        _button = _interactionController.GetComponent<Button>();
        _buttonImage = _interactionController.GetComponent<Image>();
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
            SwitchButtonToAttack(); // 컨트롤러 버튼 공격으로 전환
            
            UnDetectInteraction();

            targetObject.InteractionUnDetectEvent();
            // interactionDetectEvent -= targetObject.InteractionDetectEvent;
            interactionEvent -= targetObject.Interact;
            // interactionUnDetectEvent -= targetObject.InteractionUnDetectEvent;


            isDetected = false;
            targetObject = null;
            return;
        }
        
        if (!isDetected && hit != null)
        {
            SwitchButtonToInteract(); // 컨트롤러 버튼 상호작용으로 전환
            
            isDetected = true;
            targetObject = hit.GetComponent<InteractionObject>();
            
            // interactionDetectEvent += targetObject.InteractionDetectEvent;
            interactionEvent += targetObject.Interact;
            // interactionUnDetectEvent += targetObject.InteractionUnDetectEvent;

            targetObject.InteractionDetectEvent();
            
            DetectInteraction();
            

        }


    }

    public void DetectInteraction()
    {
        interactionUnDetectEvent?.Invoke();
    }

    public void UnDetectInteraction()
    {
        interactionUnDetectEvent?.Invoke();
    }

    /**
     * <summary>
     * 상호작용 버튼을 누르면 실행되는 함수
     * </summary>
     */
    public void Interact()
    {
        interactionEvent?.Invoke();
        
    }
    
    
    public void SwitchButtonToAttack()
    {
        _buttonImage.sprite = _attackButtonSprite;
    }
        
    public void SwitchButtonToInteract()
    {
        _buttonImage.sprite = _InteractionButtonSprite;
    }
}