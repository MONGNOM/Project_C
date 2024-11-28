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
    [Header("�÷��̾� ����")]
    [SerializeField] protected float maxHp;
    [SerializeField] protected float curHp;
    [SerializeField] protected float damage;
    [SerializeField] protected float def;
    [SerializeField] protected Vector2 boxRange;
    [SerializeField] protected float playerSpeed;
    
    [Header("�÷��̾� �̵�����")]
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

    public void OnMove(InputValue value)// ���� ���x
    {
        inPutMove = value.Get<Vector2>();
    }

    public void PlayerTakeHit(float damage)// ���� ���x  // ���� ����
    {
        curHp -= damage;

        
        // ������ ����
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

    private void pickUp() // ���� ���x 
    {
        // �ֺ��� ������ ������ �ݱ� 
        // ���� (���� �ʰ��� ���ݱ�)
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = UnityEngine.Color.yellow;
        Gizmos.DrawWireCube(transform.position, boxRange);
    }
}
