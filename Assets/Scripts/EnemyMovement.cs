using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private NavMeshAgent agent;
    private Health health;
    private Transform playerTransform;

    void Start()
    {
        // Vind de NavMeshAgent component
        agent = GetComponent<NavMeshAgent>();
        health = gameObject.GetComponent<Health>();

        // Zoek de XR Rig met de tag "Player"
        GameObject player = GameObject.FindGameObjectWithTag("Base");
        if (player != null)
        {
            playerTransform = player.transform;
        }
    }

    void Update()
    {
        // Beweeg naar de positie van de speler als deze is gevonden
        if (playerTransform != null && !health.isDead)
        {
            agent.SetDestination(playerTransform.position);
        }
    }
}
