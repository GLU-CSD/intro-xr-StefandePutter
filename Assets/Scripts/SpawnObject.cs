using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnObject : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject throwTower;
    [SerializeField] private List<GameObject> towers = new List<GameObject>();
    //[SerializeField] private Button m_YourFirstButton, m_YourSecondButton, m_YourThirdButton;

    public void SpawnTower()
    {
        int index = UnityEngine.Random.Range(0,towers.Count);
        GameObject tower = towers[index];
        GameObject spawnedTower = Instantiate(throwTower,spawnPoint.position,Quaternion.identity);
        
        spawnedTower.GetComponent<TowerGrenade>().towerPrefab = tower;

        Debug.Log("spawned " + tower.name + towers.Count);
    }
}
