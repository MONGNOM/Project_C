using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Inventory : MonoBehaviour
{
    public Slot[] slot;
    public int countItem;
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


    public void UseItem()
    { 
        // 아이템 사용하면 카운터가 1개에서 사용하면 삭제 및 효과 발동 혹은 1이상일시 카운트 내리고 효과 적용
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


