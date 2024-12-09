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
            case ItemType.use:
                InvenUseItem();
                break;
            case ItemType.Armor:
                InvenArmor();
                break;
            default:
                break;
        }
    }

    public void InvenWeapon()
    {
        Debug.Log("무기 착용");
        //슬롯에 방어구착용 및 옷 입히기?
        // 스텟 증가
    }

    public void InvenArmor()
    {
        Debug.Log("방어구 착용");
        //슬롯에 방어구착용 및 옷 입히기?
        // 스텟 증가
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

    public void UseHealPotion()
    {
        player.Heal(heal);
    }

    public void DamageUp()
    {
        player.DamageUp(damage);
    }

    public void AttackSpeedUp()
    {
        player.AttackSpeedUp(attackspeed);
    }


}
