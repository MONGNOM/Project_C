using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Net.Sockets;


public class Description : MonoBehaviour
{
    Animator animator;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemDescription;
    public Slot slotitem;
    public Inventory inventory;

    public PrefabItem helmet;
    public PrefabItem armor;
    public PrefabItem shoes;
    public PrefabItem weapon;
    public PrefabItem shield;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        inventory = FindAnyObjectByType<Inventory>();
    }

    public void DescriptionActive(Slot slotItem)
    {
        itemName.text = slotItem.name;
        itemDescription.text = slotItem.description;
        animator.SetTrigger("Show");
    }

    public void SetCurrentItem(Slot slot)
    {
        slotitem = slot;
    }

    public void UnDescriptionActive()
    {
        animator.SetTrigger("Hide");
    }

    public void DropButton()
    {
        if (inventory.CurKg <= 0)
            return;

        inventory.DropWeight(slotitem.kg);
        slotitem.DropItem();
    }

    public void UseButton()
    {
        if (inventory.CurKg <= 0 || Slot.ItemType.stuff == slotitem.itemtype)
            return;


        switch (slotitem.itemtype)
        { 
            case Slot.ItemType.use:
                inventory.DropWeight(slotitem.kg);
                slotitem.UseItem();
                break;
            case Slot.ItemType.Helmet:
                if (helmet.name != "")
                {
                    inventory.UnEquip(helmet);
                }
                helmet.InventoryItemSet(slotitem); //장비 데이터 덮는 코드
                slotitem.UseItem();
                break;
            case Slot.ItemType.Weapon:
                if (weapon.name != "")
                {
                    inventory.UnEquip(weapon);
                }
                weapon.InventoryItemSet(slotitem);
                slotitem.UseItem();
                break;
            case Slot.ItemType.Shoes:
                if (shoes.name != "")
                {
                    inventory.UnEquip(shoes);
                }
                shoes.InventoryItemSet(slotitem);
                slotitem.UseItem();
                break;
            case Slot.ItemType.Armor:
                if (armor.name != "")
                {
                    inventory.UnEquip(armor);
                }
                armor.InventoryItemSet(slotitem);
                slotitem.UseItem();
                break;
            default:
                break;
        }
    }





}
