using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject waveWonUi;
    //[SerializeField] private Transform spawnPoint;
    //[SerializeField] private GameObject throwTower;
    [SerializeField] private List<GameObject> towers = new List<GameObject>();
    //[SerializeField] private Button m_YourFirstButton, m_YourSecondButton, m_YourThirdButton;

    private TowerPlacement placement;

    private void Start()
    {
        placement = GetComponent<TowerPlacement>();
    }

    public void SpawnTower(GameObject tower)
    {
        // old way of doing it keeping it here to showcase

        //int index = UnityEngine.Random.Range(0,towers.Count);
        //GameObject tower = towers[index];
        //GameObject spawnedTower = Instantiate(throwTower,spawnPoint.position,Quaternion.identity);
        
        //spawnedTower.GetComponent<TowerGrenade>().towerPrefab = tower;

        placement.SetTower(tower);

        waveWonUi.SetActive(false);

        Debug.Log("spawned " + tower.name + towers.Count);
    }
}
