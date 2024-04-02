using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Tutorial : InteractionObject
{
    public override void Interact()
    {
        base.Interact();
        Debug.Log("Tutorial Interact");
    }
}
