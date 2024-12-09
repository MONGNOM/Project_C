using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class Description : MonoBehaviour
{
    Animator animator;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemDescription;
    public Slot slotitem;
    public Inventory inventory;

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

        inventory.DropWeight(slotitem.kg);
        slotitem.UseItem();
    }





}
