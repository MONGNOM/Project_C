using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class DataManager : MonoBehaviour
{
    public Player player;
    public static DataManager instance;

    string path;
    string filename = "Save";
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        { 
            Destroy(instance);
        }

        path = Application.persistentDataPath + "/";
    }

 

    private void OnApplicationQuit()
    {
        SaveData();
    }

    public void SaveData()
    {
        // ���� Ż��� �ڵ� ���� (��ȯ��/���� ����)
        string data  = JsonUtility.ToJson(player);
        File.WriteAllText(path + filename, data);
        Debug.Log("Save");
    }

    public void LoadData()
    {
        // �����ӽ� ���
        string data = File.ReadAllText(path + filename);
        JsonUtility.FromJsonOverwrite(data, player);
        Debug.Log("Load");
    }
}
