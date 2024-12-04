using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class PrefabItem : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public enum ItemType { Weapon, Armor, use, stuff }
    public ItemType itemtype;
    public Sprite icon;
    public new string name;
    public float damage;
    public float attackspeed;
    public float def;
    public float kg;
    public int price;
    public string description;
    public Monster monster;
    private Inventory inventory;


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        monster = FindAnyObjectByType<Monster>();
        inventory = FindAnyObjectByType<Inventory>();
    }

    void Start()
    {
        ItemSet(monster);
    }

    void ItemSet(Monster monster)
    {
        spriteRenderer.sprite = monster.dropList[monster.randValue].icon;
        icon                  = monster.dropList[monster.randValue].icon;
        itemtype              = (ItemType)monster.dropList[monster.randValue].itemtype;
        name                  = monster.dropList[monster.randValue].name;
        damage                = monster.dropList[monster.randValue].damage;
        attackspeed           = monster.dropList[monster.randValue].attackspeed;
        def                   = monster.dropList[monster.randValue].def;
        kg                    = monster.dropList[monster.randValue].kg;
        price                 = monster.dropList[monster.randValue].price;
        description           = monster.dropList[monster.randValue].description;
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
