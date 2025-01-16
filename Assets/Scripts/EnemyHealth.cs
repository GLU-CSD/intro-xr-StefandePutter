using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
    // public so towers can check if they are alive
    [HideInInspector] public float ghostHealth; // health - damage it is going to recieve
    [HideInInspector] public bool isDead; // if incoming damage is enough to kill enemy isDead

    private void Start()
    {
        currentHealth = maxHealth;
        ghostHealth = maxHealth;
        isDead = false;
        UpdateHealthBar();
    }

    public void GhostDamage(float amount)
    {
        ghostHealth -= amount;
        if (ghostHealth <= 0)
        {
            isDead = true;
        }
    }
}
