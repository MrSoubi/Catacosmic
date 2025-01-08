using System;
using System.Numerics;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    float health;

    public Action onDeath;
    public Action<float> onHealthChanged;
    public Action<float> onTookDamage;

    public void SetHealth(float maxHealth)
    {
        health = maxHealth;
    }

    public void TakeDamage(float damage)
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
