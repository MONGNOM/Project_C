using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Object/Item Data")]
public class Item : ScriptableObject
{
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
    public GameObject prefab;
}
