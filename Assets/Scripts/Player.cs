using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

abstract class Player
{
    [Header("�÷��̾� ����")]
    [SerializeField] private float maxHp;
    [SerializeField] private float curHp;
    [SerializeField] private float damage;
    [SerializeField] private float def;
    [SerializeField] private float range;
    [SerializeField] private float playerSpeed;


    public void Move()
    {
        // ������ ����
    }

    private void TakeHit(float damage)
    {
        curHp -= damage;
        // ������ ����
    }

    private void Die()
    {
        // �÷��̾� ���
    }

    private void Attack()
    { 
        // �ֺ��� ���� ������ ����
    }

    private void pickUp()
    {
        // �ֺ��� ������ ������ �ݱ� 
        // ���� (���� �ʰ��� ���ݱ�)
    }
}
