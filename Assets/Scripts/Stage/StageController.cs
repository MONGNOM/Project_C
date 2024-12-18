using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageController : MonoBehaviour
{
    public TextMeshProUGUI stageNumber;

    public string stageName;

    private void Awake()
    {
        stageName = stageNumber.text;
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene(stageName);


        if (StageUi.instance == null)
            return;

        StageUi.instance.ShowStageUi();

        // 다음으로 가는 string 정보를 가져와야함
        // 포탈 이용시 다음씬 || 공략 혹은 귀환서시 메인 씬
    }


}
