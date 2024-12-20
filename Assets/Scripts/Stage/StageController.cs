using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class StageController : MonoBehaviour
{
    public static StageController instance;

    public string[] stage; // 2�߹迭? �� �� �������� ������ ������������ �������� ����
    public int stageIndex;

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
        stageIndex = 0;
    }

    public void ChangeScene()
    {
        if (stageIndex < stage.Length)
        {
            SceneManager.LoadScene(stage[stageIndex]);
            Debug.Log(stage[stageIndex]);
            stageIndex++;
        }
        else
        {
            stageIndex = 0;
            SceneManager.LoadScene("Main");
        }
        // �������� ���� string ������ �����;���
        // ��Ż �̿�� ������ || ���� Ȥ�� ��ȯ���� ���� ��
    }


}
