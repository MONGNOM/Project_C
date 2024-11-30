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

    [Header("플레이어 스탯")]
    [SerializeField] protected float maxHp;
    [SerializeField] protected float curHp;
    [SerializeField] protected float damage;
    [SerializeField] protected float def;
    
    [Header("플레이어 공격범위")]
    [SerializeField] protected Vector2 boxRange;
    [Header("플레이어 줍기범위")]
    [SerializeField] protected Vector2 pickupRange;
    [Header("플레이어 이동속도")]
    [SerializeField] protected float moveSpeed;
    [Header("플레이어 공격속도")]
    [SerializeField] protected float attackSpeed;

    [Header("플레이어 이동벡터")]
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
            // Gamepad가 연결되어 있다면 Control Scheme을 Gamepad로 설정
            playerInput.SwitchCurrentControlScheme("Gamepad", Gamepad.current);
            Debug.Log("컨트롤러 연결성공");
        }
        else
        {
            Debug.LogWarning("컨트롤러 연결실패");
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
        Debug.Log("hp감소");
    }

    public void HitDamage()
    {
        // 이거 수정할 필요가 있음
        // 범위로 있을때만 가능하게 조건 줘야겠는데
        if (monster != null)
        monster.GetComponent<Monster>().MonsterTakeHit(damage);
        // 몬스터가 다른애가 있다면..?
    }


    public void OnMove(InputValue value)// 굳이 상속x
    {
        inPutMove = value.Get<Vector2>();
    }

    public void PlayerTakeHit(float damage)// 굳이 상속x  // 몬스터 동일
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

    protected virtual void Die() // 굳이 상속x // 몬스터 동일
    {
        animator.SetBool("Idle", false);
        animator.SetBool("attack", false);
        animator.SetBool("Walk", false);

        // 플레이어 사망
        animator.SetBool("Die", true);
    }

    protected virtual void Attack() // 굳이 상속x // 몬스터 동일
    {
        animator.SetBool("attack", true);
    }

    private void ItemPickUp() // 굳이 상속x 
    {
        Collider2D collider = Physics2D.OverlapBox(transform.position, pickupRange, 0, LayerMask.GetMask("Item"));
        if (collider != null)
        {
            Debug.Log("아이템 감지");
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
