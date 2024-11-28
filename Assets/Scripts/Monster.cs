using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using System.Drawing;
using UnityEngine.InputSystem.HID;
using static UnityEngine.EventSystems.StandaloneInputModule;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public abstract class Monster : MonoBehaviour
{
    [Header("���� ����")]
    [SerializeField] protected float maxHp;
    [SerializeField] protected float curHp;
    [SerializeField] protected float damage;
    [SerializeField] protected float def;
    [SerializeField] protected float monsterSpeed;

    [Header("���� Ž�� �Ÿ�")]
    [SerializeField] protected Vector2 detectEnemyRange;

    [Header("���� ���� �Ÿ�")]
    [SerializeField] protected Vector2 attackEnemyRange;

    

    private Rigidbody2D rigid;
    private PlayerInput playerInput;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private Player player;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        player = FindAnyObjectByType<Player>();
    }

    protected void Start()
    {
        curHp = maxHp;
    }

    private void FixedUpdate()
    {

        if (curHp <= 0)
        {
            Die();
            return;
        }

        DetectEnemy();
    }

    public void MonsterTakeHit(float damage)// ���� ���x  // ���� ����
    {
        curHp -= damage;

    }

    protected virtual void DetectEnemy()
    {
        Collider2D collider = Physics2D.OverlapBox(transform.position, detectEnemyRange, 0, LayerMask.GetMask("Player"));
        if (collider != null)
        {
            Move(collider); // ���󰡱� 
            
        }
        else
        {
            animator.SetTrigger("Idle");
        }
    }

    private void Move(Collider2D player)
    {
        

        // �����϶� �����ؾ���
        Vector3 playerPos = player.transform.position - transform.position;
        playerPos.Normalize();

        transform.position += playerPos * monsterSpeed * Time.deltaTime;

        Collider2D collider = Physics2D.OverlapBox(transform.position, attackEnemyRange, 0, LayerMask.GetMask("Player"));
        if (collider != null)
        {
            monsterSpeed = 0;
            Attack();
            // ���� �������� attack�� �ϰ� ����ߤ��ϴµ�/
        }
        else
        {
            animator.SetTrigger("Jump");
            monsterSpeed = 0.5f;
        }
    }

    public void HitDamage()
    {
        player.GetComponent<Player>().PlayerTakeHit(damage);
    }

    protected virtual void Die() // ���� ���x // ���� ����
    {
        animator.SetBool("Die", true);
    }

    protected virtual void Attack() // ���� ���x // ���� ����
    {
        animator.SetTrigger("attack");
    }

    

    private void pickUp() // ���� ���x 
    {
        // �ֺ��� ������ ������ �ݱ� 
        // ���� (���� �ʰ��� ���ݱ�)
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = UnityEngine.Color.yellow;
        Gizmos.DrawWireCube(transform.position, detectEnemyRange);

        Gizmos.color = UnityEngine.Color.red;
        Gizmos.DrawWireCube(transform.position, attackEnemyRange);
    }
}
