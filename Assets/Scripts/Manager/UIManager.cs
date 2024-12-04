using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [Header("플레이어 체력 UI")]
    public Image playerHpImage;

    [Header("플레이어 무게 UI")]
    public Image playerKgImage;

    [Header("플레이어 인벤토리 스탯")]
    public TextMeshProUGUI maxHp;
    public TextMeshProUGUI damage;
    public TextMeshProUGUI def;
    public TextMeshProUGUI attackSpeed;
    public TextMeshProUGUI maxKg;
    public TextMeshProUGUI curKg;

  

    [Header("몬스터 UI")]
    public Image monsterHpImage;

    [Header("인벤토리 코인금액")]
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
