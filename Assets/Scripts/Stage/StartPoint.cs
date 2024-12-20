using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint : MonoBehaviour
{
    Player player;

    void Start()
    {
        player = FindAnyObjectByType<Player>();
        player.transform.position = transform.position;
    }

    
}
