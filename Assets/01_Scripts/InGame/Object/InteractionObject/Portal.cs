using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : InteractionObject
{
    public override void Interact()
    {
        base.Interact();
        GameManager.Instance._StageManager.MoveToNextStage();
    }
}
