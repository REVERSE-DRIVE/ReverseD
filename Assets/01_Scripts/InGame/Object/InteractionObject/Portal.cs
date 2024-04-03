using System.Collections;
using UnityEngine;

public class Portal : InteractionObject
{
    public override void Interact()
    {
        base.Interact();
        GameManager.Instance._StageManager.NextStage();
        // 나중에 중간과정을 만들것
        StartCoroutine(PortalCoroutine());
    }

    private IEnumerator PortalCoroutine()
    {
        yield return new WaitForSeconds(0.5f);
        PoolManager.Release(gameObject);

    }
}
