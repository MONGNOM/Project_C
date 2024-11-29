using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [Header("플레이어 UI")]
    public Image playerHpImage;

    [Header("몬스터 UI")]
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
