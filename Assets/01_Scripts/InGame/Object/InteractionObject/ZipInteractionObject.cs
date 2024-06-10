using System.Collections;
using DG.Tweening;
using ItemManage;
using UnityEngine;

public class ZipInteractionObject : InteractionObject
{
    [SerializeField] private Item _zipItem;
    [SerializeField] private ParticleSystem _zipParticle;
    
    private Animator _animator;
    private readonly int _openHash = Animator.StringToHash("Open");

    protected override void Awake()
    {
        base.Awake();
        _animator = GetComponent<Animator>();
    }

    public override void Interact()
    {
        OpenZip();
    }

    private void OpenZip()
    {
        StartCoroutine(OpenCoroutine());
    }

    private IEnumerator OpenCoroutine()
    {
        _zipParticle.Play();
        yield return new WaitForSeconds(0.1f);
        _animator.SetTrigger(_openHash);
        yield return new WaitForSeconds(0.5f);
        ItemDropManager.Instance.DropItem(_zipItem, transform.position);
        yield return new WaitForSeconds(0.2f);
        Sequence seq = DOTween.Sequence();

        seq.Append(transform.DOScaleY(0.1f, 0.2f));
        seq.AppendInterval(0.1f);
        seq.Append(transform.DOScaleX(0.1f, 0.2f));

        seq.Play().OnComplete(() => gameObject.SetActive(false));
    }
}