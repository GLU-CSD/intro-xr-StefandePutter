using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [HideInInspector] public float damage;
    private Transform target;

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 direction = (target.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;

        transform.LookAt(target.position, Vector3.up);
        transform.Rotate(-90,0,0);

        if (Vector3.Distance(transform.position, target.position) < 0.4f)
        {
            Explode();
        }
    }

    // vertual so it can be overwritten by other towers
    internal virtual void Explode()
    {
        // Zoek Health component van target met GetComponent
        // Als Health script gevonden is, gebruik TakeDamage functie
        // Gebruik damage variable
        // Instantiate eventuele effecten
        target.GetComponent<IDamageable>().TakeDamage(damage);
        Destroy(gameObject);
    }
}
