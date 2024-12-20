using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageStartButton : MonoBehaviour
{
    StageController controller;
    public Button startButton;
    private void Awake()
    {
       startButton = GetComponentInChildren<Button>();
    }

    void Start()
    {
       controller = FindAnyObjectByType<StageController>();
       startButton.onClick.AddListener(controller.ChangeScene);
    }

}
