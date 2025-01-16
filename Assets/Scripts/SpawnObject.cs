using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnObject : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private List<GameObject> towers = new List<GameObject>();
    //[SerializeField] private Button m_YourFirstButton, m_YourSecondButton, m_YourThirdButton;

    public void SpawnTower()
    {
        GameObject tower = towers[0];
        Instantiate(tower,spawnPoint.position,Quaternion.identity);
        Debug.Log("hello");
    }
}
