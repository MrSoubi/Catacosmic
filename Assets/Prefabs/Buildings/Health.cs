using System;
using System.Numerics;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    double health;

    public Action onDeath;
    public Action<double> onHealthChanged;
    public Action<double> onTookDamage;

    public void SetHealth(double maxHealth)
    {
        health = maxHealth;
    }

    public void TakeDamage(double damage)
    {
        health -= damage;

        health = health < 0 ? 0 : health;

        Debug.Log(health);

        onHealthChanged?.Invoke(health);
        onTookDamage?.Invoke(damage);

        if (health <= 0)
        {
            onDeath?.Invoke();
        }
    }
}
