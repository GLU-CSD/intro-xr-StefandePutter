using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class PlayerGrenade : PlayerProjectile
{
    [SerializeField] private float explosionRadius = 5f;
    internal override void Trigger(GameObject collision)
    {
        // Vind alle objecten in de buurt van de explosie
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider nearbyObject in colliders)
        {
            if (nearbyObject.CompareTag("Enemy"))
            {
                // Get Health for enemies
                IDamageable healthScript = nearbyObject.GetComponent<IDamageable>();
                if (healthScript != null)
                {
                    healthScript.TakeDamage(damage);
                }
            }
        }
        Destroy(gameObject);
    }
}
