using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ExplodeOnImpact : Projectile
{
    [SerializeField] private float explosionForce = 500f;      // Kracht van de explosie
    [SerializeField] private float explosionRadius = 5f;       // Radius van de explosie

    void Explode()
    {
        // Vind alle objecten in de buurt van de explosie
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider nearbyObject in colliders)
        {
            if (nearbyObject.CompareTag("Enemy"))
            {
                // Get Health for enemies
                Health healthScript = nearbyObject.GetComponent<Health>();
                if (healthScript != null)
                {
                    healthScript.TakeDamage(damage);
                }

                // Add explosion force
                Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
                }
            }            
        }
        Destroy(gameObject);
    }
}
