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
    public float Damage { get { return damage; } private set { damage = value; damageAction?.Invoke(damage); } }
    public event Action<float> damageAction;
    public float Def { get { return def; } private set { def = value; defAction?.Invoke(def); } }
    public event Action<float> defAction;
    public float AttackSpeed { get { return attackSpeed; } private set { attackSpeed = value; attackSpeedAction?.Invoke(attackSpeed); } }
    public event Action<float> attackSpeedAction;


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
    
    [Header("플레이어 인벤토리")]
    [SerializeField] private Inventory inventory;

    private Rigidbody2D rigid;
    private PlayerInput playerInput;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private float attackcool;

    [SerializeField] protected GameObject helmet;
    [SerializeField] protected GameObject weapon;
    [SerializeField] protected GameObject shoes;
    [SerializeField] protected GameObject armor;


    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        pickupRange = new Vector2(1, 1);
        attackSpeed = 0.5f;
        curHp = maxHp;
        Hp += HpChange;
        damageAction += DamageChange;
        attackSpeedAction += AttackSpeedChange;
        defAction += DefenceChange;
        animator.SetFloat("AttackSpeed", attackSpeed);
    }


    protected void Start()
    {
        UIManager.instance.maxHp.text = maxHp.ToString();
        UIManager.instance.damage.text = damage.ToString();
        UIManager.instance.attackSpeed.text = attackSpeed.ToString();
        UIManager.instance.def.text = def.ToString();

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


        // StartCoroutine(AutoPickUp()); 티스토리작성 코루틴
    }

    IEnumerator AutoPickUp() //티스토리작성 코루틴
    {
        while (true)
        {
            ItemPickUp();
            yield return new WaitForSeconds(1);
            Debug.Log("코루틴감지");
        }

    }

    private void FixedUpdate()
    {
        DetectEnemy();
        //ItemPickUp();

        if (inPutMove.x >= 0)
            spriteRenderer.flipX = false;
        else
            spriteRenderer.flipX = true;

        Vector2 vecMove = inPutMove * moveSpeed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + vecMove);
    }
    private void Update()
    {
        attackcool += Time.deltaTime;
    }

    public IEnumerator AttackSpeedup(float speed)
    {
        float orign = AttackSpeed;
        AttackSpeed += speed;
        Debug.Log(AttackSpeed);
        yield return new WaitForSeconds(30);
        AttackSpeed = orign;
        Debug.Log(AttackSpeed);

    }

    public IEnumerator AttackDamageUp(float power)
    {
        float orign = Damage;
        Damage += power;
        Debug.Log(Damage);
        yield return new WaitForSeconds(30);
        Damage = orign;
        Debug.Log(Damage);
    }

    private void HpChange(float hp)
    {
        UIManager.instance.playerHpImage.fillAmount = hp;
        Debug.Log("hp변화");
    }

    private void DamageChange(float damage)
    {
        UIManager.instance.damage.text = damage.ToString();
        
    }

    private void DefenceChange(float def)
    {

        UIManager.instance.def.text = def.ToString();
    }

    private void AttackSpeedChange(float speed)
    {
        UIManager.instance.attackSpeed.text = speed.ToString();
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
            
            if (attackcool > attackSpeed)
            {
                Attack(collider);
                attackcool = 0;
            }
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

    protected virtual void Attack(Collider2D collider) // 굳이 상속x // 몬스터 동일
    {
        Debug.Log("공격ㄱ");
        collider.GetComponent<Monster>().MonsterTakeHit(damage);
        animator.SetBool("attack", true);
    }

    protected virtual void Die() // 굳이 상속x // 몬스터 동일
    {
        animator.SetBool("Idle", false);
        animator.SetBool("attack", false);
        animator.SetBool("Walk", false);

        // 플레이어 사망
        animator.SetBool("Die", true);
    }

   

    private void ItemPickUp() // 굳이 상속x  아이템 픽업 변경
    {
        Collider2D collider = Physics2D.OverlapBox(transform.position, pickupRange, 0, LayerMask.GetMask("Item"));
        if (collider != null)
        {
            PrefabItem item = collider.GetComponent<PrefabItem>();

            if (item.name != "Coin" && inventory.CurKg < inventory.MaxKg)
            {
                Debug.Log("가벼움");
                Debug.Log(item.name);
                Debug.Log("아이템 감지");
                Debug.Log(item.kg);
                inventory.InventoryAddItem(item);
                return;
            }

            UIManager.instance.inventoryCoin.text = item.price.ToString();// 코인 액수 전달
            Debug.Log(item.price);
            Debug.Log("돈 증가");
        }
    }

    public void Defence(float defence)
    {
        Def += defence;
    }

    public void Heal(float hp)
    {
        CurHp += hp;
    }

    public void AttackSpeedUp(float speed)
    {
        AttackSpeed += speed;
    }

    public void DamageUp(float damageup)
    {
        Damage += damageup;
    }

    public void UnEquipWeapon(float damageup)
    {
        Damage -= damageup;
    }

    public void UnEquipArmor(float defence)
    {
        Def -= defence;
    }

    public void EquipHelmet()
    { 
        helmet.SetActive(true);
    }

    public void EquipWeapon()
    {
        weapon.SetActive(true);
    }
    public void EquipShoes()
    {
        shoes.SetActive(true);
    }
    public void EquipArmor()
    {
        armor.SetActive(true);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = UnityEngine.Color.red;
        Gizmos.DrawWireCube(transform.position, boxRange);

        Gizmos.color = UnityEngine.Color.green;
        Gizmos.DrawWireCube(transform.position, pickupRange);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 아이템 픽업
        PrefabItem item = collision.GetComponent<PrefabItem>();

        if (item.name != "Coin" && inventory.CurKg < inventory.MaxKg)
        {
            inventory.InventoryAddItem(item);
            return;
        }

        UIManager.instance.inventoryCoin.text = item.price.ToString();// 코인 액수 전달
    }
}
