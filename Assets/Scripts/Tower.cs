using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private GameObject firePoint;
    [SerializeField] private GameObject teleportPoint;
    
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
       if (gameManager.gameActive)
        {
            teleportPoint.SetActive(true);
        }
    }
}
