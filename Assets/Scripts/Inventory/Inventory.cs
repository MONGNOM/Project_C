using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class Inventory : MonoBehaviour
{
    public Slot[] slot;
    public int countItem;
    private float maxKg;
    private float curKg;

    public Button helmet;
    public Button weapon;
    public Button armor;
    public Button shoes;
    public Image unequip;

    public float MaxKg { get { return maxKg; } private set { maxKg = value; inventoryMaxweight?.Invoke(maxKg); } }
    public Action<float> inventoryMaxweight;

    public float CurKg { get { return curKg; } private set { curKg = value; inventoryCurweight?.Invoke(curKg); } }
    public Action<float> inventoryCurweight;

    private Animator ani;

    private void Awake()
    {
        ani = GetComponent<Animator>();
        MaxKg = 200;
        CurKg = 0;
        MaxWeightText(MaxKg);
        CurWeighttext(CurKg);
        WeightChange(CurKg);
    }
    private void Start()
    {
        inventoryMaxweight += MaxWeightText;
        inventoryCurweight += CurWeighttext;
        inventoryCurweight += WeightChange;
    }

    private void WeightChange(float weight)
    {
        UIManager.instance.playerKgImage.fillAmount = weight;
        Debug.Log("kg변화");
    }



    public void UnEquip(PrefabItem item)
    {
        InventoryAddItem(item);
    }


  

    public void InventoryActive()
    {
        ani.SetTrigger("Show");
        Debug.Log("Show");
    }

    public void InventoryUnActive()
    {
        ani.SetTrigger("Hide");
        Debug.Log("Hide");
    }


    public void InventoryAddItem(PrefabItem item)
    {
        for (int i = 0; i < slot.Length; i++)
        {
            if (slot[i].name == item.name)
            {
                Debug.Log("동일한 아이템 습득");
                AddWeight(item.kg);
                slot[i].additionItemCount();
                return;
            }
            else if (slot[i].name == "" && item.name != "Coin")
            {
                slot[i].AddItem(item);
                AddWeight(item.kg);
                return;
            }
        }
    }


    private void MaxWeightText(float maxKg)
    {
        UIManager.instance.maxKg.text = maxKg.ToString() + " /";
    }
    private void CurWeighttext(float curKg)
    {
        UIManager.instance.curKg.text = curKg.ToString();
    }

    public void AddWeight(float addweight)
    {
        CurKg += addweight;
        // 아이템 습득시
    }

    public void DropWeight(float reduceweight)
    {
        CurKg -= reduceweight;

        double epsilon = 0.0001;

        if (Math.Abs(curKg) < epsilon)
        {
            CurKg = 0;
        }
    }

}


