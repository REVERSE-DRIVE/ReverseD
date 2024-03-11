using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EventObject : MonoBehaviour
{
    [SerializeField] protected float lifetime = 1;

    /**
     * <summary>
     * update에서 작동
     * </summary>
     */
    protected abstract void LifeRoutine();
}
