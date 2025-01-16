using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TowerGrenade : MonoBehaviour
{
    [SerializeField] public GameObject towerPrefab;

    void OnCollisionEnter(Collision collision)
    {
        // Controleer of de grenade het terrein raakt
        if (collision.gameObject.CompareTag("Terrain"))
        {
            Vector3 spawnPos = new Vector3(transform.position.x,0,transform.position.z);
            Instantiate(towerPrefab, spawnPos, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
