using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldObject : MonoBehaviour
{
    public bool canDestroy = true;
    
    public int hp = 3;
    public int hpMax = 3;

    [SerializeField] private GameObject _destroyParticle;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int amount)
    {
        hp -= amount;
    }

    private void IsDestroy()
    {
        if (hp <= 0)
        {
            Destroy();
        }
    }

    public void Destroy()
    {
        PoolManager.Release(gameObject);
        
    }
    
    
    
}
