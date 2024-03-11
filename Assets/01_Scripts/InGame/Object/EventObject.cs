using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EventObject : MonoBehaviour
{
    [SerializeField] protected float lifetime;

    protected abstract void lifeRoutine();
}
