using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject monster;
    public void SpawnMonster()
    {
        Instantiate(monster, transform.position, Quaternion.identity);
    }
}
