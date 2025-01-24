using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR.Interaction.Toolkit;

public class TowerTakeover : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float launchVelocity = 1000f;
    [SerializeField] private TowerAttack TowerAttack;
    [SerializeField] private GameManager gameManager;
    private XRRayInteractor m_RayInteractor;
    private XROrigin XRRig;
    private float nextFireTime = 0f;
    private float fireRate;
    private bool takenOver = false;

    // Start is called before the first frame update
    void Start()
    {
        XRRig = GameObject.FindFirstObjectByType<XROrigin>();
        gameManager = GameObject.FindFirstObjectByType<GameManager>();
        // TowerAttack = GetComponent<TowerAttack>();
        fireRate = TowerAttack.fireRate;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameManager.gameActive)
        {
            if (takenOver)
            {
                Debug.Log("teleporting back");
                takenOver = false;
                XRRig.transform.SetPositionAndRotation(gameManager.spawnpoint.transform.position, gameManager.spawnpoint.transform.rotation);
                TowerAttack.fireRate = fireRate;
            }
            gameObject.SetActive(false);
        }

        if (takenOver && Time.time >= nextFireTime)
        {
            Transform handTransform = m_RayInteractor.gameObject.transform;
            GameObject projectile = Instantiate(projectilePrefab, handTransform.position, handTransform.rotation);
            projectile.transform.Rotate(-90,0,0);
            projectile.GetComponent<PlayerProjectile>().damage = TowerAttack.damage + 50f;
            projectile.GetComponent<Rigidbody>().AddRelativeForce(new Vector3
                                                 (0, 0, launchVelocity));
            nextFireTime = Time.time + 1f / fireRate;
        }
    }

    public void TakeOver(SelectExitEventArgs args)
    {
        m_RayInteractor = args.interactorObject.transform.gameObject.GetComponent<XRRayInteractor>();
        
        //XRRig.transform.position = args.interactableObject.transform.position;
        //XRRig.transform.rotation = args.interactableObject.transform.rotation;

        XRRig.transform.SetPositionAndRotation(args.interactableObject.transform.position, Quaternion.identity);

        TowerAttack.fireRate = 0f;


        takenOver = true;
        Debug.Log(args.interactableObject.transform.name);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
    }
}
