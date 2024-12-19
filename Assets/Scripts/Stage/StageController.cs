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

    public string[] stage; // 2중배열? 로 총 스테이지 갯수랑 스테이지마다 던전갯수 지정
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
            StageUi.instance.ShowStageUi();
            Debug.Log(stage[stageIndex]);
            stageIndex++;
        }
        else
        {
            stageIndex = 0;
            SceneManager.LoadScene("Main");
            StageUi.instance.HideStageUi();
        }

        


        // 다음으로 가는 string 정보를 가져와야함
        // 포탈 이용시 다음씬 || 공략 혹은 귀환서시 메인 씬
    }


}
