using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerChoiceSlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private PlayerSO _playerSO;
    [SerializeField] private Player _player;
    [SerializeField] private Ease _ease;
    
    private Vector3 _originalScale;
    private Sequence _sequence;

    private void Awake()
    {
        _originalScale = transform.localScale;
    }

    private void SetSequence()
    {
        _sequence = DOTween.Sequence();
        _sequence.Append(transform.DOScale(_originalScale * 1.2f, 0.3f).SetEase(_ease))
            .AppendInterval(0.5f)
            .Append(transform.root.GetChild(0).DOScale(Vector3.zero, 0.5f).SetEase(_ease))
            .OnComplete(() => transform.root.GetChild(0).gameObject.SetActive(false));
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        SetSequence();
        _sequence.Play();
        PlayerManager.Instance.PlayerSO = _playerSO;
    }
}
