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
        // �������� ����
        gameObject.SetActive(true);
    }

    public void HideStageUi()
    {
        //���� ���� �� ��ȯ�� ���
        gameObject.SetActive(false);
    }
}
