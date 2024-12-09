using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Object/Item Data")]
public class Item : ScriptableObject
{
    public enum ItemType { Helmet, Weapon, Armor, Shoes, use, stuff }
    public ItemType itemtype;
    public enum UseType { None, Potion, Scroll }
    public UseType usetype;
    public enum PotionType { None, Heal, AtkSpeedUp , AttackUp }
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
    public GameObject prefab;
}
