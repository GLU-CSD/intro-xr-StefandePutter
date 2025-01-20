using OpenCover.Framework.Model;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

public class TowerPlacement : MonoBehaviour
{
    [SerializeField] private XRRayInteractor m_RayInteractor;
    [SerializeField] private Material unsetMat;
    [SerializeField] private GameObject tower;
    private GameObject placedTower;
    private Material TowerMat;
    private Vector3 oldPos;
    private bool placingTower;
    private bool placingNewTower;

    // Update is called once per frame
    void Update()
    {
        // when placing tower
        if (!placingTower)
        {
            return;
        }

        m_RayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit);

        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Terrain")) {
                placedTower.transform.position = hit.point;
                
            }
        }
    }

    public void SetTower(GameObject go)
    {
        placingNewTower = true;

        placedTower = Instantiate(go);

        TowerMat = placedTower.GetComponent<MeshRenderer>().material;

        int LayerIgnoreRaycast = LayerMask.NameToLayer("Ignore Raycast");
        placedTower.layer = LayerIgnoreRaycast; // set layer to ignore raycast

        placedTower.GetComponent<MeshRenderer>().material = unsetMat;

        placingTower = true;
    }

    public void Replace(SelectExitEventArgs args)
    {
        GameObject go = args.interactableObject.transform.gameObject;
        if (go.CompareTag("Tower"))
        {
            // m_RayInteractor = args.interactorObject.transform.gameObject.GetComponent<XRRayInteractor>();

            oldPos = go.transform.position; // setting old pos for if replacing gets cancelled

            placedTower = go;

            TowerMat = placedTower.GetComponent<MeshRenderer>().material;

            int LayerIgnoreRaycast = LayerMask.NameToLayer("Ignore Raycast");
            placedTower.layer = LayerIgnoreRaycast; // set layer to ignore raycast

            placedTower.GetComponent<MeshRenderer>().material = unsetMat;
            
            placingTower = true;
        }
    }

    public void PlaceTower(SelectExitEventArgs args)
    {
        if (placingTower)
        {
            Vector3 position;

            GameObject go = args.interactableObject.transform.gameObject;
            if (go.CompareTag("Terrain") || placingNewTower)
            {
                position = placedTower.transform.position;
            } else if (go.CompareTag("Tower"))
            {
                position = oldPos;
            } else
            {
                return;
            }

            placedTower.layer = 8; // set layer to tower

            placedTower.GetComponent<MeshRenderer>().material = TowerMat;
            Instantiate(placedTower, position, placedTower.transform.rotation);
            Destroy(placedTower);

            placedTower = null;
            placingTower = false;
            placingNewTower = false;
        }
    }
}
