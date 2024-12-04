using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using System.Drawing;
using UnityEngine.InputSystem.HID;
using static UnityEngine.EventSystems.StandaloneInputModule;
using System;
using Random = UnityEngine.Random;
using Unity.VisualScripting;
using System.Threading;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public abstract class Monster : MonoBehaviour
{
    public float CurHp { get { return curHp; } private set { curHp = value; Hp?.Invoke(curHp); } }
    public event Action<float> Hp;

    [Header("몬스터 스탯")]
    [SerializeField] protected float maxHp;
    [SerializeField] protected float curHp;
    [SerializeField] protected float damage;
    [SerializeField] protected float def;
    [SerializeField] protected float monsterSpeed;

    [Header("몬스터 탐지 거리")]
    [SerializeField] protected Vector2 detectEnemyRange;

    [Header("몬스터 공격 거리")]
    [SerializeField] protected Vector2 attackEnemyRange;

    [Header("몬스터 드랍 리스트")] 
    public List<Item> dropList;

    public int randValue;
    protected bool isDead;

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
        isDead = false;
    }

    protected void Start()
    {
        curHp = maxHp;
        Hp += HpChange;
    }

    private void FixedUpdate()
    {
        if (isDead) return;
        
        DetectEnemy();
    }

  
    protected virtual void DetectEnemy()
    {
        Collider2D collider = Physics2D.OverlapBox(transform.position, detectEnemyRange, 0, LayerMask.GetMask("Player"));
        if (collider != null)
        {
            Move(collider); // 따라가기 
        }
        else
        {
            animator.SetTrigger("Idle");
        }
    }

    private void Move(Collider2D player)
    {
        // 움직이는 코드
        Vector3 playerPos = player.transform.position - transform.position;
        playerPos.Normalize();

        transform.position += playerPos * monsterSpeed * Time.deltaTime;

        // 공격 범위 안에 플레이어 찾는 코드
        Collider2D collider = Physics2D.OverlapBox(transform.position, attackEnemyRange, 0, LayerMask.GetMask("Player"));
        if (collider != null)
        {
            monsterSpeed = 0;
            Attack();
        }
        else
        {
            animator.SetTrigger("Jump");
            monsterSpeed = 0.5f;
        }
        
    }

    public void MonsterTakeHit(float damage)
    {
        if (isDead) return;

        CurHp -= damage;

        if (curHp <= 0)
        {
            Die();
        }


    }

    private void HpChange(float hp)
    {
        UIManager.instance.monsterHpImage.fillAmount = hp;
        Debug.Log("hp감소");
    }

    public void HitDamage()
    {
        player.GetComponent<Player>().PlayerTakeHit(damage);
    }

    protected virtual void Die() 
    {
        isDead = true;

        animator.SetTrigger("Die");
        Destroy(gameObject, 0.6f);
    }
    protected virtual void DropGold(Item item)
    {
        Debug.Log("돈떨꿈");
        int rand = Random.Range(0, 100);
        item.price = rand;
    }

    protected virtual void DropItem() 
    {
        Debug.Log("아이템떨꿈");
        int rand = Random.Range(0, dropList.Count);
        randValue = rand;
        if (dropList[rand].name == "Coin")
            DropGold(dropList[rand]);
        
            Instantiate(dropList[0].prefab, transform.position, Quaternion.identity);
    }

    protected virtual void Attack()
    {
        animator.SetTrigger("attack");
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = UnityEngine.Color.yellow;
        Gizmos.DrawWireCube(transform.position, detectEnemyRange);

        Gizmos.color = UnityEngine.Color.red;
        Gizmos.DrawWireCube(transform.position, attackEnemyRange);
    }
}
