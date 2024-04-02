using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerChoiceSlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private PlayerSO _playerSO;
    [SerializeField] private Ease _ease;
    [SerializeField] private Image _darkness;
    [SerializeField] private TMP_Text _nameTxt;
    [SerializeField] private TMP_Text _descTxt;
    [SerializeField] private bool isLocked;
    
    private Vector3 _originalScale;
    private Sequence _sequence;
    
    private void OnEnable()
    {
        _originalScale = transform.localScale;
        _nameTxt.text = _playerSO.playerName;
        _descTxt.text = _playerSO.playerDescription;
    }

    private void SetSequence()
    {
        _sequence = DOTween.Sequence();
        _sequence.Append(transform.DOScale(_originalScale * 1.2f, 0.3f).SetEase(_ease))
            .AppendInterval(0.5f)
            .Append(transform.root.GetChild(0).DOScale(Vector3.zero, 0.5f).SetEase(_ease))
            .AppendInterval(0.5f)
            .Append(SetTween())
            .AppendInterval(0.5f)
            .OnComplete(() => SceneManager.LoadScene("InGame"));
    }

    private Tween SetTween()
    {
        _darkness.gameObject.SetActive(true); 
        return _darkness.DOFade(1, 0.5f);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isLocked) return;
        
        SetSequence();
        _sequence.Play();
        PlayerSOMover.Instance.SetSO(_playerSO);
    }
}
