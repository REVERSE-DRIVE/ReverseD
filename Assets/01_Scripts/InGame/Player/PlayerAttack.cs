using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private GameObject _arrowPrefab;
    [SerializeField] private Collider2D[] attackColliders;
    
    private GameObject _playerAttackCollider;
    private List<GameObject> arrows = new List<GameObject>();
    
    public GameObject PlayerAttackCollider => _playerAttackCollider;

    private void Awake()
    {
        attackColliders = transform.GetChild(0).GetComponentsInChildren<Collider2D>();
        _playerAttackCollider = transform.GetChild(0).gameObject;
    }
    public void Attack(WeaponType weaponType)
    {
        foreach (var col in attackColliders)
        {
            col.enabled = false;
        }

        if (weaponType == WeaponType.sword)
        {
            attackColliders[0].enabled = true;
        }
        else if (weaponType == WeaponType.bow)
        {
            transform.GetChild(1).rotation = _playerAttackCollider.transform.rotation;
            ArrowShot();
        }
        else if (weaponType == WeaponType.lazor)
        {
            attackColliders[1].enabled = true;
        }
        else if (weaponType == WeaponType.shield)
        {
            attackColliders[2].enabled = true;
        }
    }
    
    public void ArrowSpawn(int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject arrow = Instantiate(_arrowPrefab, transform.GetChild(1));
            arrows.Add(arrow);
            arrow.SetActive(false);
        }
    }
    
    public void ArrowShot()
    {
        float time = 0;
        while (true)
        {
            time += Time.deltaTime;
            if (time > PlayerManager.Instance.AttackSpeed)
            {
                for (int i = 0; i < arrows.Count; i++)
                {
                    if (!arrows[i].activeSelf)
                    {
                        arrows[i].transform.position = transform.position;
                        arrows[i].transform.rotation = transform.GetChild(1).rotation;
                        arrows[i].SetActive(true);
                        break;
                    }
                }
                time = 0;
            }
        }
        
    }
}
