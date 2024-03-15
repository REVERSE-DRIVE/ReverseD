using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private GameObject _arrowPrefab;
    [SerializeField] private Collider2D[] attackColliders;
    
    private GameObject _playerAttackCollider;
    private List<GameObject> arrows = new();
    private bool _isAllowedAttack = true;
    
    public GameObject PlayerAttackCollider => _playerAttackCollider;

    private void Awake()
    {
        attackColliders = transform.GetChild(0).GetComponentsInChildren<Collider2D>();
        _playerAttackCollider = transform.GetChild(0).gameObject;
    }
    public void Attack()
    {
        foreach (var col in attackColliders)
        {
            col.enabled = false;
        }

        if (PlayerManager.Instance.WeaponType == WeaponType.sword)
        {
            attackColliders[0].enabled = true;
        }
        else if (PlayerManager.Instance.WeaponType == WeaponType.bow)
        {
            transform.GetChild(1).rotation = _playerAttackCollider.transform.rotation;
            StartCoroutine(ArrowShot());
        }
        else if (PlayerManager.Instance.WeaponType == WeaponType.lazor)
        {
            attackColliders[1].enabled = true;
        }
        else if (PlayerManager.Instance.WeaponType == WeaponType.shield)
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
    
    private IEnumerator ArrowShot()
    {
        if (!_isAllowedAttack) yield break;

        _isAllowedAttack = false;
        for (int i = 0; i < arrows.Count; i++)
        {
            if (arrows[i].activeSelf) continue;
            arrows[i].transform.position = transform.position;
            arrows[i].transform.rotation = transform.GetChild(1).rotation;
            arrows[i].SetActive(true);
            arrows[i].transform.parent = null;
            break;
        }
        yield return new WaitForSeconds(PlayerManager.Instance.AttackSpeed * 0.3f);
        _isAllowedAttack = true;
    }
}
