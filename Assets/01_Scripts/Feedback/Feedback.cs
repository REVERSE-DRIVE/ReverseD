﻿using UnityEngine;

public abstract class Feedback : MonoBehaviour
{
    public abstract void CreateFeedback();
    public abstract void FinishFeedback();

    protected Entity _owner;

    protected virtual void Awake()
    {
        _owner = transform.parent.GetComponent<Entity>();
    }

    protected void OnDisable()
    {
        FinishFeedback();
    }
}