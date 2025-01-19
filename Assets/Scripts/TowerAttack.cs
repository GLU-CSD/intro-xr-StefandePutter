using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAttack : MonoBehaviour
{
    [SerializeField] private float fireRate = 1f;
    [SerializeField] private float damage = 50f;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private bool fireFirstEnemy = false;
    private Transform baseTransform;

    private float nextFireTime = 0f;
    private List<Transform> enemiesInRange = new List<Transform>();

    private void Start()
    {
        if (fireFirstEnemy)
        {
            baseTransform = GameObject.FindGameObjectWithTag("Base").transform;
        } else
        {
            baseTransform = transform;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemiesInRange.Add(other.transform);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemiesInRange.Remove(other.transform);
        }
    }

    void Update()
    {
        if (Time.time >= nextFireTime)
        {
            Transform target = GetClosestEnemy();
            if (target != null)
            {
                Shoot(target);
                nextFireTime = Time.time + 1f / fireRate;
            }
        }
    }

    void Shoot(Transform target)
    {
        target.gameObject.GetComponent<EnemyHealth>().GhostDamage(damage);
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        projectile.GetComponent<Projectile>().SetTarget(target);
        projectile.GetComponent<Projectile>().damage = damage;
    }

    private Transform GetClosestEnemy()
    {
        Transform closestEnemy = null;
        float shortestDistance = Mathf.Infinity;

        // Loop door alle enemies die in de buurt zijn
        foreach (Transform enemy in enemiesInRange)
        {
            // Check dat de enemy nog bestaat (niet al dood is!)
            if (enemy != null)
            {
                //Health enemyHealth = enemy.gameObject.GetComponent<Health>();
                if (!enemy.gameObject.GetComponent<EnemyHealth>().isDead)
                {
                    // Check de afstand tussen toren en enemy
                    float distanceToEnemy = Vector3.Distance(baseTransform.position, enemy.position);
                    // Bewaar de kortste afstand om de dichtsbijzijnde enemy te vinden
                    if (distanceToEnemy < shortestDistance)
                    {
                        shortestDistance = distanceToEnemy;
                        closestEnemy = enemy;
                    }
                } else
                {
                    enemiesInRange.Remove(enemy);
                    closestEnemy = null;
                    return closestEnemy;
                }
            }
            else
            {
                // Als de enemy dood gegaan is in range van de toren, verwijder hem dan uit de lijst
                // We passen de lijst nu aan en eindigen de loop
                enemiesInRange.Remove(enemy);
                closestEnemy = null;
                return closestEnemy;
            }
        }
        return closestEnemy;
    }
}
