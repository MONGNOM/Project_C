using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [Header("�÷��̾� UI")]
    public Image playerHpImage;

    [Header("���� UI")]
    public Image monsterHpImage;

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
}
