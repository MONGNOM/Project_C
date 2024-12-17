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
        if (instance != null)
        {
            Destroy(this);
        }
        else
        { 
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        path = Application.persistentDataPath + "/";
    }

 

    private void OnApplicationQuit()
    {
        SaveData();
    }

    public void SaveData()
    {
        // 던전 탈출시 자동 적용 (귀환서/던전 공략)
        string data  = JsonUtility.ToJson(player);
        File.WriteAllText(path + filename, data);
        Debug.Log("Save");
    }

    public void LoadData()
    {
        // 재접속시 사용
        string data = File.ReadAllText(path + filename);
        JsonUtility.FromJsonOverwrite(data, player);
        Debug.Log("Load");
    }
}
