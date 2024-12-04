using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Inventory : MonoBehaviour
{
    public Slot[] slot;

    private float maxKg;
    private float curKg;

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
        Debug.Log("kg증가");
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
            if (slot[i].item == null && item.name != "Coin")
            {
                slot[i].AddItem(item);
                AddWeight(item.kg);
                Debug.Log("인벤토리 아이템 추가");
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

    public void reduceWeight(float reduceweight)
    {
        CurKg -= reduceweight;
        // 물건을 버리거나 상점에 판매시
    }

}


