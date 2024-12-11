using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class PrefabItem : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public enum ItemType { Helmet, Weapon, Armor, Shoes, use, stuff }
    public ItemType itemtype;
    public enum UseType { None, Potion, Scroll }
    public UseType usetype;
    public enum PotionType { None, Heal, AtkSpeedUp, AttackUp }
    public PotionType potiontype;

    public Sprite icon;
    public new string name;
    public float damage;
    public float attackspeed;
    public float def;
    public float kg;
    public float heal;
    public int price;
    public string description;
    public Monster monster;
    private Inventory inventory;


    private void Awake()
    {
        monster = FindAnyObjectByType<Monster>();
        inventory = FindAnyObjectByType<Inventory>();
    }

   
    void InventoryItemSet(Slot slot)
    {
        icon    = slot.icon.sprite;
        itemtype = (ItemType)slot.itemtype;
        usetype = (UseType)slot.usetype;
        potiontype = (PotionType)slot.potiontype;
        name = slot.name;
        damage = slot.damage;
        attackspeed = slot.attackspeed;
        def = slot.def;
        kg = slot.kg;
        heal = slot.heal;
        price = slot.price;
        description = slot.description;
    }

    public void ItemSet(Monster monster, int value)
    {
        spriteRenderer.sprite = monster.dropList[value].icon;
        icon                  = monster.dropList[value].icon;
        itemtype              = (ItemType)monster.dropList[value].itemtype;
        usetype               = (UseType)monster.dropList[value].usetype;
        potiontype            = (PotionType)monster.dropList[value].potiontype;
        name                  = monster.dropList[value].name;
        damage                = monster.dropList[value].damage;
        attackspeed           = monster.dropList[value].attackspeed;
        def                   = monster.dropList[value].def;
        kg                    = monster.dropList[value].kg;
        heal                  = monster.dropList[value].heal;
        price                 = monster.dropList[value].price;
        description           = monster.dropList[value].description;
        Debug.Log("아이템 완성");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
        {
            if (name != "Coin" && inventory.CurKg >= inventory.MaxKg)
                return;

            Destroy(gameObject, 0.1f);
        }
    }

}
