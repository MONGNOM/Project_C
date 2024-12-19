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
            Destroy(gameObject);
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
        // ���θ޴� ���ִ��ڵ�
        gameObject.SetActive(true);
    }

    public void HideStageUi()
    {
        //���� ���� �� ��ȯ�� ���
        // ���θ޴� ���ִ��ڵ�
        gameObject.SetActive(false);
    }
}
