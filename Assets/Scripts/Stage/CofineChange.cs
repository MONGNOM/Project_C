using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CofineChange : MonoBehaviour
{

   public CinemachineConfiner2D Confiner;
   public PolygonCollider2D polygon;

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ChangeCofine();
    }

    public void ChangeCofine()
    {
        polygon = FindAnyObjectByType<PolygonCollider2D>();
        Confiner.m_BoundingShape2D = polygon;
    }
}
