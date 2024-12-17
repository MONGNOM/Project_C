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

    [Header("�κ��丮 ���αݾ�")]
    public TextMeshProUGUI inventoryCoin;

    [Header("Stage ui")]
    public GameObject stageCanvas;

    [Header("Main ui")]
    public GameObject mainCanvas;
    
    [Header("�κ��丮")]
    public Inventory inven;


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
    }

    public void MainUI()
    {
        stageCanvas.SetActive(false);
        mainCanvas.SetActive(true);
    }

    public void StageUI()
    {
        mainCanvas.SetActive(false);
        stageCanvas.SetActive(true);
    }
}
