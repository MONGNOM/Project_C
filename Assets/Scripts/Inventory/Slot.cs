using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public PrefabItem item;
    public enum ItemType { Helmet, Weapon, Armor, Shoes, use, stuff }
    public ItemType itemtype;
    public enum UseType { None, Potion, Scroll }
    public UseType usetype;
    public enum PotionType { None, Heal, AtkSpeedUp, AttackUp }
    public PotionType potiontype;

    public Image icon;
    public new string name;
    public float damage;
    public float attackspeed;
    public float def;
    public float kg;
    public float heal;
    public int price;
    public string description;

    public int countItem;

    public GameObject countImage;

    [Header("아이템 갯수")]
    public TextMeshProUGUI slotItemCount;
    public Button button;
    public Description des;
    public Inventory inventory;
    private Player player;

   

    private void Awake()
    {
        countItem = 1;
        CountItemText();
        icon = transform.GetChild(0).GetComponent<Image>();
        button = GetComponentInChildren<Button>();
        countImage.gameObject.SetActive(false);
        button.gameObject.SetActive(false);
        des = FindAnyObjectByType<Description>();
        inventory = FindAnyObjectByType<Inventory>();
        player = FindAnyObjectByType<Player>();
    }

    private void Start()
    {
        button.onClick.AddListener(ItemButtonClick);
    }

    public void ItemButtonClick()
    {
        // 클릭할떄 해당 아이템 이름 및 설명 가져오기
        //drop에서 갯수가져가야지 
        //직접 붙여넣어서 변경 할 필요있어보임
        des.DescriptionActive(this);
        des.SetCurrentItem(this);
        //drop아이템을여기서 줘야하나
    }

    public void CountItemText()
    {
        slotItemCount.text = countItem.ToString();
    }
    public void additionItemCount()
    {
        countItem += 1;
        CountItemText();
    }

    public void subtractingItemCount()
    {
        countItem -= 1;
        CountItemText();
    }
   

    public void UseItem()
    {
        if (itemtype == ItemType.stuff)
            return;

        if (countItem > 1)
        {
            subtractingItemCount();
            ItemUse();
            return;
        }

        //아이템사용함수 추가
        ItemUse();
        DropItem();
        // 아이템 사용하면 카운터가 1개에서 사용하면 삭제 및 효과 발동 혹은 1이상일시 카운트 내리고 효과 적용
        // 아이템의 정보 가져오기
    }

    public void AddItem(PrefabItem slotitem)
    {
        icon.sprite = slotitem.icon;
        itemtype    = (ItemType)slotitem.itemtype;
        usetype     = (UseType)slotitem.usetype;
        potiontype  = (PotionType)slotitem.potiontype;
        name        = slotitem.name;
        damage      = slotitem.damage;
        attackspeed = slotitem.attackspeed;
        heal        = slotitem.heal;
        def         = slotitem.def;
        kg          = slotitem.kg;
        price       = slotitem.price;
        description = slotitem.description;
        button.gameObject.SetActive(true);
        countImage.SetActive(true);
    }

    public void DropItem()
    {
        if (countItem > 1)
        {
            subtractingItemCount();
            return;
        }

        icon.sprite = null;
        name        =  "";
        damage      =  0;
        attackspeed =  0;
        def         =  0;
        kg          =  0;
        price       =  0;
        description = null;
        button.gameObject.SetActive(false);
        countImage.SetActive(false);
    }

    public void ItemUse()
    {
        switch (itemtype)
        {
            case ItemType.Weapon:
                InvenWeapon();
                break;
            case ItemType.Helmet:
                InvenHelmet();
                break;
            case ItemType.use:
                InvenUseItem();
                break;
            case ItemType.Armor:
                InvenArmor();
                break;
            case ItemType.Shoes:
                InvenShoes();
                break;
            default:
                break;
        }
    }

    public void UsePoion()
    {
        switch (potiontype)
        {
            case PotionType.Heal:
                UseHealPotion();
                break;
            case PotionType.AttackUp:
                DamageUp();
                break;
            case PotionType.AtkSpeedUp:
                AttackSpeedUp();
                break;

        }
    }

    // 장비창 슬롯 따로 구현 해서 slot에 데이터를 equipSlot에 데이터 전달 해서 구현
    // 버튼 클릭시 장비 해제 (데이터를 바탕으로 인벤 아이템 추가)
    // 장비창에 아이템 장착 되어있을시 이름 동일이면 무시 다르면 교체(장착중인거 인벤추가).3

    public void InvenWeapon()
    {
        //item.InventoryItemSet(this);
        inventory.weapon.gameObject.SetActive(true);
        inventory.weapon.image.sprite = icon.sprite;
        player.DamageUp(damage);
        player.EquipWeapon();
        Debug.Log("무기 착용");
    }

    public void InvenHelmet()
    {
        inventory.helmet.gameObject.SetActive(true);
        inventory.helmet.image.sprite = icon.sprite;
        player.Defence(def);
        player.EquipHelmet();
        Debug.Log("투구 착용");
    }

    public void InvenArmor()
    {
        inventory.armor.gameObject.SetActive(true);
        inventory.armor.image.sprite = icon.sprite;
        player.Defence(def);
        player.EquipArmor();
        Debug.Log("방어구 착용");
    }

    public void InvenShoes()
    {
        inventory.shoes.gameObject.SetActive(true);
        inventory.shoes.image.sprite = icon.sprite;
        player.Defence(def);
        player.EquipShoes();
        Debug.Log("신발 착용");
    }

    public void InvenUseItem()
    {
        switch (usetype)
        {
            case UseType.Potion:
                UsePoion();
                break;
            case UseType.Scroll:
                BackHome();
                break;
        }
    }

    public void BackHome()
    { 
        Debug.Log("귀환!");
    }

    public void UseHealPotion()
    {
        player.Heal(heal);
    }

    public void DamageUp()
    {
        StartCoroutine(player.AttackDamageUp(damage));
    }

    public void AttackSpeedUp()
    {
        StartCoroutine(player.AttackSpeedup(attackspeed));
    }

    


}
