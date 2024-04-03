using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : InteractionObject
{
    public override void Interact()
    {
        base.Interact();
        GameManager.Instance._StageManager.NextStage();
        StartCoroutine(PortalCoroutine());
    }

    private IEnumerator PortalCoroutine()
    {
        yield return new WaitForSeconds(0.1f);
        PoolManager.Release(gameObject);

    }
}
