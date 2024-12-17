using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageUi : MonoBehaviour
{
    public static StageUi instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        else
        { 
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void ShowStageUi()
    { 
        // 스테이지 입장
        gameObject.SetActive(true);
    }

    public void HideStageUi()
    {
        //던전 공략 및 귀환서 사용
        gameObject.SetActive(false);
    }
}
