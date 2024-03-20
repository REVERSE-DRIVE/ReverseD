using UnityEngine;

public abstract class InteractionObject : MonoBehaviour
{
    public string objectName;
    public string objectDescription;
    
    public virtual void Interact()
    {
        // override로 구현
        
    }

    public void ShowName()
    {
        
    }
}
