using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [Header("�÷��̾� ü�� UI")]
    public Image playerHpImage;

    [Header("�÷��̾� ���� UI")]
    public Image playerKgImage;

    [Header("�÷��̾� �κ��丮 ����")]
    public TextMeshProUGUI maxHp;
    public TextMeshProUGUI damage;
    public TextMeshProUGUI def;
    public TextMeshProUGUI attackSpeed;
    public TextMeshProUGUI maxKg;
    public TextMeshProUGUI curKg;

  

    [Header("���� UI")]
    public Image monsterHpImage;

    [Header("�κ��丮 ���αݾ�")]
    public TextMeshProUGUI inventoryCoin;

    void Awake()
    {

        if (instance != null)
        {
            Destroy(instance);
        }
        else
        {
            instance = this;

            DontDestroyOnLoad(gameObject);
        }

        inventoryCoin.text = 0.ToString();
    }
}
