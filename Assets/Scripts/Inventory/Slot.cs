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

    [Header("아이템 갯수")]
    public TextMeshProUGUI slotItemCount;
    public Button button;
    public Description des;

    private void Awake()
    {
        countItem = 1;
        CountItemText();
        icon = transform.GetChild(0).GetComponent<Image>();
        button = GetComponentInChildren<Button>();
        countImage.gameObject.SetActive(false);
        button.gameObject.SetActive(false);
        des = FindAnyObjectByType<Description>();
    }

    private void Start()
    {
       // button.onClick.AddListener(ItemButtonClick(this)); 
    }

    public void ItemButtonClick(Slot item)
    {
        // 클릭할떄 해당 아이템 이름 및 설명 가져오기
        //drop에서 갯수가져가야지 
        des.DescriptionActive(item);
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
        button.gameObject.SetActive(true);
        countImage.SetActive(true);
    }

    public void DropItem(PrefabItem slotitem)
    {
        if (countItem > 1)
        {
            subtractingItemCount();
            return;
        }

        slotitem.icon = null;
        slotitem.name        =  "";
        slotitem.damage      =  0;
        slotitem.attackspeed =  0;
        slotitem.def         =  0;
        slotitem.kg          =  0;
        slotitem.price       =  0;
        slotitem.description = null;
        button.gameObject.SetActive(false);
        countImage.SetActive(false);
    }

    
}
