using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using System.Drawing;
using UnityEngine.InputSystem.HID;
using static UnityEngine.EventSystems.StandaloneInputModule;

enum PlayerState { Idle, Walk, TakeHit, Attack, Die}

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public abstract class Player : MonoBehaviour 
{
    [Header("플레이어 스탯")]
    [SerializeField] protected float maxHp;
    [SerializeField] protected float curHp;
    [SerializeField] protected float damage;
    [SerializeField] protected float def;
    [SerializeField] protected Vector2 boxRange;
    [SerializeField] protected float playerSpeed;
    
    [Header("플레이어 이동벡터")]
    [SerializeField] private Vector2 inPutMove;

    private Rigidbody2D rigid;
    private PlayerInput playerInput;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    protected void Start()
    {
        curHp = maxHp;

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
        if (curHp <= 0)
        {   Die();
            return;
        }

        DetectEnemy();

        if (inPutMove.x >= 0)
            spriteRenderer.flipX = false;
        else
            spriteRenderer.flipX = true;

        Vector2 vecMove = inPutMove * playerSpeed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + vecMove);
    }

    public void OnMove(InputValue value)// 굳이 상속x
    {
        inPutMove = value.Get<Vector2>();
    }

    public void PlayerTakeHit(float damage)// 굳이 상속x  // 몬스터 동일
    {
        curHp -= damage;

        
        // 데미지 받음
    }

    protected virtual void DetectEnemy()
    {
        Collider2D collider = Physics2D.OverlapBox(transform.position, boxRange, 0, LayerMask.GetMask("Monster"));
        if (collider != null)
        {
            animator.SetBool("Walk", false);
            animator.SetBool("Idle", false);
            Attack();
            collider.GetComponent<Monster>().MonsterTakeHit(damage);
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

    private void pickUp() // 굳이 상속x 
    {
        // 주변에 아이템 있으면 줍기 
        // 조건 (무게 초과시 안줍기)
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = UnityEngine.Color.yellow;
        Gizmos.DrawWireCube(transform.position, boxRange);
    }
}
