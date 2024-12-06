using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public PrefabItem item;
    public enum ItemType { Weapon, Armor, use, stuff }
    public ItemType itemtype;
    public Image icon;
    public new string name;
    public float damage;
    public float attackspeed;
    public float def;
    public float kg;
    public int price;
    public string description;

    public int countItem;

    public GameObject countImage;

    [Header("æ∆¿Ã≈€ ∞πºˆ")]
    public TextMeshProUGUI slotItemCount;

    //public GameObject prefab;

    // Start is called before the first frame update
    private void Awake()
    {
        countItem = 1;
        CountItemText();
        icon = transform.GetChild(0).GetComponent<Image>();
        countImage.gameObject.SetActive(false);
    }
    private void Start()
    {
        
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



    public void AddItem(PrefabItem slotitem)
    {
        icon.sprite = slotitem.icon;
        itemtype    = (ItemType)slotitem.itemtype;
        name        = slotitem.name;
        damage      = slotitem.damage;
        attackspeed = slotitem.attackspeed;
        def         = slotitem.def;
        kg          = slotitem.kg;
        price       = slotitem.price;
        description = slotitem.description;
        countImage.SetActive(true);
    }

    public void DropItem()
    {
        icon.sprite = null;
        itemtype    =  ItemType.stuff;
        name        =  null;
        damage      =  0;
        attackspeed =  0;
        def         =  0;
        kg          =  0;
        price       =  0;
        description = null;
        countImage.SetActive(true);
    }

    
}
