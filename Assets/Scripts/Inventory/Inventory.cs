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
        Debug.Log("kg����");
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
                Debug.Log("������ ������ ����");
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
        // ������ ����ϸ� ī���Ͱ� 1������ ����ϸ� ���� �� ȿ�� �ߵ� Ȥ�� 1�̻��Ͻ� ī��Ʈ ������ ȿ�� ����
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
        // ������ �����
    }

    public void reduceWeight(float reduceweight)
    {
        CurKg -= reduceweight;
        // ������ �����ų� ������ �ǸŽ�
    }

}


