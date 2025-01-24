using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    [HideInInspector] public float damage;

    private void OnCollisionEnter(Collision collision)
    {
        Trigger(collision.gameObject);
    }

    internal virtual void Trigger(GameObject collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<IDamageable>().TakeDamage(damage);
        }
        Destroy(gameObject,1);
    }
}
