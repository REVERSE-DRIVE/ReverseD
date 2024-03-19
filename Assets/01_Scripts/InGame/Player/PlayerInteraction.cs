using System;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public Action interactionEvent;
    
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        
        
    }

    public void Interaction()
    {
        interactionEvent?.Invoke();
        
    }
}