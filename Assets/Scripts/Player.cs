using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

abstract class Player
{
    [Header("플레이어 스탯")]
    [SerializeField] private float maxHp;
    [SerializeField] private float curHp;
    [SerializeField] private float damage;
    [SerializeField] private float def;
    [SerializeField] private float range;
    [SerializeField] private float playerSpeed;


    public void Move()
    {
        // 움직임 구현
    }

    private void TakeHit(float damage)
    {
        curHp -= damage;
        // 데미지 받음
    }

    private void Die()
    {
        // 플레이어 사망
    }

    private void Attack()
    { 
        // 주변에 몬스터 있으면 공격
    }

    private void pickUp()
    {
        // 주변에 아이템 있으면 줍기 
        // 조건 (무게 초과시 안줍기)
    }
}
