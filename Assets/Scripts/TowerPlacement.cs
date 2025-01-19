using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

public class TowerPlacement : MonoBehaviour
{
    [SerializeField] private XRRayInteractor m_RayInteractor;
    [SerializeField] private GameObject tower;
    private GameObject placedTower;
    private bool placing = true;

    // Start is called before the first frame update
    void Start()
    {
        if (m_RayInteractor == null) 
        {
            LogNotSet("Ray", gameObject.name);

        }
        if (tower == null)
        {
            LogNotSet("Tower", gameObject.name);

        }
    }

    // Update is called once per frame
    void Update()
    {
        // when placing tower
        if (!placing)
        {
            return;
        }

        m_RayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit);
        
        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Terrain")) {
                if (placedTower == null)
                { 
                    placedTower = Instantiate(tower, hit.point, Quaternion.identity);
                } else
                {
                    placedTower.transform.position = hit.point;
                }
            }
        }
    }

    private static void LogNotSet(string varName, string gameObjectName)
    {
        Debug.Log(varName + " not set in PlaceTower script on object: " + gameObjectName);
    }
}
