using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using System.Drawing;
using UnityEngine.InputSystem.HID;
using static UnityEngine.EventSystems.StandaloneInputModule;
using System.Runtime.CompilerServices;
using System;

enum PlayerState { Idle, Walk, TakeHit, Attack, Die}

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public abstract class Player : MonoBehaviour 
{
    public float CurHp { get {return curHp; } private set { curHp = value; Hp?.Invoke(curHp); } }
    public event Action<float> Hp;

    [Header("�÷��̾� ����")]
    [SerializeField] protected float maxHp;
    [SerializeField] protected float curHp;
    [SerializeField] protected float damage;
    [SerializeField] protected float def;
    
    [Header("�÷��̾� ���ݹ���")]
    [SerializeField] protected Vector2 boxRange;
    [Header("�÷��̾� �ݱ����")]
    [SerializeField] protected Vector2 pickupRange;
    [Header("�÷��̾� �̵��ӵ�")]
    [SerializeField] protected float moveSpeed;
    [Header("�÷��̾� ���ݼӵ�")]
    [SerializeField] protected float attackSpeed;

    [Header("�÷��̾� �̵�����")]
    [SerializeField] private Vector2 inPutMove;

    private Rigidbody2D rigid;
    private PlayerInput playerInput;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private Monster monster;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        monster = FindAnyObjectByType<Monster>();
        pickupRange = new Vector2(1, 1);
        attackSpeed = 0.5f;
        curHp = maxHp;
        Hp += HpChange;
        animator.SetFloat("AttackSpeed", attackSpeed);
    }

    protected void Start()
    {
       

        if (Gamepad.current != null)
        {
            // Gamepad�� ����Ǿ� �ִٸ� Control Scheme�� Gamepad�� ����
            playerInput.SwitchCurrentControlScheme("Gamepad", Gamepad.current);
            Debug.Log("��Ʈ�ѷ� ���Ἲ��");
        }
        else
        {
            Debug.LogWarning("��Ʈ�ѷ� �������");
        }
    }

    private void FixedUpdate()
    {
        DetectEnemy();
        ItemPickUp();
        
        if (inPutMove.x >= 0)
            spriteRenderer.flipX = false;
        else
            spriteRenderer.flipX = true;

        Vector2 vecMove = inPutMove * moveSpeed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + vecMove);
    }

    private void HpChange(float hp)
    {
        UIManager.instance.playerHpImage.fillAmount = hp;
        Debug.Log("hp����");
    }

    public void HitDamage()
    {
        // �̰� ������ �ʿ䰡 ����
        // ������ �������� �����ϰ� ���� ��߰ڴµ�
        if (monster != null)
        monster.GetComponent<Monster>().MonsterTakeHit(damage);
        // ���Ͱ� �ٸ��ְ� �ִٸ�..?
    }


    public void OnMove(InputValue value)// ���� ���x
    {
        inPutMove = value.Get<Vector2>();
    }

    public void PlayerTakeHit(float damage)// ���� ���x  // ���� ����
    {
        if (curHp <= 0)
        {
            Die();
            return;
        }

        CurHp -= damage;
    }

    protected virtual void DetectEnemy()
    {
        Collider2D collider = Physics2D.OverlapBox(transform.position, boxRange, 0, LayerMask.GetMask("Monster"));
        if (collider != null)
        {
            animator.SetBool("Walk", false);
            animator.SetBool("Idle", false);
            Attack();
            //collider.GetComponent<Monster>().MonsterTakeHit(damage);
        }
        else
        {
            animator.SetBool("attack", false);

            if (inPutMove.x == 0 && inPutMove.y == 0)
            {
                animator.SetBool("Walk", false);
                animator.SetBool("Idle", true);
            }
            else
            {
                animator.SetBool("Idle", false);
                animator.SetBool("Walk", true);
            }

        }
    }

    protected virtual void Die() // ���� ���x // ���� ����
    {
        animator.SetBool("Idle", false);
        animator.SetBool("attack", false);
        animator.SetBool("Walk", false);

        // �÷��̾� ���
        animator.SetBool("Die", true);
    }

    protected virtual void Attack() // ���� ���x // ���� ����
    {
        animator.SetBool("attack", true);
    }

    private void ItemPickUp() // ���� ���x 
    {
        Collider2D collider = Physics2D.OverlapBox(transform.position, pickupRange, 0, LayerMask.GetMask("Item"));
        if (collider != null)
        {
            Debug.Log("������ ����");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = UnityEngine.Color.red;
        Gizmos.DrawWireCube(transform.position, boxRange);

        Gizmos.color = UnityEngine.Color.green;
        Gizmos.DrawWireCube(transform.position, pickupRange);
    }
}
