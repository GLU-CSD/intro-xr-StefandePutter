using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] internal float maxHealth = 100f;
    // public so you can check if its dead
    public float currentHealth;
    [SerializeField] internal Image healthbarFill;

    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthBar();

        if (gameObject.CompareTag("Base"))
        {
            // end of game
            return;
        }

        if (currentHealth <= 0)
        {
            NavMeshAgent agent = gameObject.GetComponent<NavMeshAgent>();
            if (agent != null)
            {
                agent.enabled = false;
            }
            Destroy(gameObject, 1f);
        }
    }

    public void RestoreHealth(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthBar();
    }

    internal void UpdateHealthBar()
    {
        healthbarFill.fillAmount = currentHealth/maxHealth;
    }
}
