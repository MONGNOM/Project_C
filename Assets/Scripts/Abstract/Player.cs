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
    
    [Header("�÷��̾� �κ��丮")]
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
            // Gamepad�� ����Ǿ� �ִٸ� Control Scheme�� Gamepad�� ����
            playerInput.SwitchCurrentControlScheme("Gamepad", Gamepad.current);
            Debug.Log("��Ʈ�ѷ� ���Ἲ��");
        }
        else
        {
            Debug.LogWarning("��Ʈ�ѷ� �������");
        }


        // StartCoroutine(AutoPickUp()); Ƽ���丮�ۼ� �ڷ�ƾ
    }

    IEnumerator AutoPickUp() //Ƽ���丮�ۼ� �ڷ�ƾ
    {
        while (true)
        {
            ItemPickUp();
            yield return new WaitForSeconds(1);
            Debug.Log("�ڷ�ƾ����");
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
        Debug.Log("hp��ȭ");
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

    protected virtual void Attack(Collider2D collider) // ���� ���x // ���� ����
    {
        Debug.Log("���ݤ�");
        collider.GetComponent<Monster>().MonsterTakeHit(damage);
        animator.SetBool("attack", true);
    }

    protected virtual void Die() // ���� ���x // ���� ����
    {
        animator.SetBool("Idle", false);
        animator.SetBool("attack", false);
        animator.SetBool("Walk", false);

        // �÷��̾� ���
        animator.SetBool("Die", true);
    }

   

    private void ItemPickUp() // ���� ���x  ������ �Ⱦ� ����
    {
        Collider2D collider = Physics2D.OverlapBox(transform.position, pickupRange, 0, LayerMask.GetMask("Item"));
        if (collider != null)
        {
            PrefabItem item = collider.GetComponent<PrefabItem>();

            if (item.name != "Coin" && inventory.CurKg < inventory.MaxKg)
            {
                Debug.Log("������");
                Debug.Log(item.name);
                Debug.Log("������ ����");
                Debug.Log(item.kg);
                inventory.InventoryAddItem(item);
                return;
            }

            UIManager.instance.inventoryCoin.text = item.price.ToString();// ���� �׼� ����
            Debug.Log(item.price);
            Debug.Log("�� ����");
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
        // ������ �Ⱦ�
        PrefabItem item = collision.GetComponent<PrefabItem>();

        if (item.name != "Coin" && inventory.CurKg < inventory.MaxKg)
        {
            inventory.InventoryAddItem(item);
            return;
        }

        UIManager.instance.inventoryCoin.text = item.price.ToString();// ���� �׼� ����
    }
}
