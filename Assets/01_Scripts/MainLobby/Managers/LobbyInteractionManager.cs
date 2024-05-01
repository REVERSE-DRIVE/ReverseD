using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MainLobby
{
    
    public class LobbyInteractionManager : MonoBehaviour
    {
        [SerializeField] private LayerMask _whatIsInteractable;

        private void Update()
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, 100f, _whatIsInteractable);
            
            if (hit.collider != null)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    hit.collider.GetComponent<InteractionObject>().Interact();
                }
            }
        }
    }

}