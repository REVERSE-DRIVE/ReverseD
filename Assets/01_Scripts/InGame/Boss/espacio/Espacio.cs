using System;
using System.Collections;
using System.Collections.Generic;
using entityManage;
using UnityEngine;

public class Espacio : Entity
{
    [SerializeField] private GameObject mob;
    
    private List<GameObject> mobList = new();
    private Status status;

    private void Awake()
    {
        for (int i = 0; i < 10; i++)
        {
            GameObject m = Instantiate(mob);
            m.SetActive(false);
            mobList.Add(m);
        }
    }

    private void Pattern1()
    {
        for (int i = 0; i < 5; i++)
        {
            mobList[i].SetActive(true);
        }
    }
    
    private void Pattern2()
    {
        // 벽 세우기
        // 텔레포트
        // 체력 회복
        Heal();
    }

    private void Heal()
    {
        //hpmax의 50%까지 2만큼 매 초 회복
        while (status.hp < status.HpMax * 0.5f)
        {
            status.hp += 2;
        }
    }
}
