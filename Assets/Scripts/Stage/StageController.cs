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
    }

}
