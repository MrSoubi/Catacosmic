using NUnit.Framework;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;


public class Hitbox : MonoBehaviour
{
    List<Hurtbox> targets;

    public RSE_DisasterAttack attackEvent;

    [SerializeField] Attack attackType;

    private void Start()
    {
        targets = new List<Hurtbox>();
    }

    private void OnEnable()
    {
        attackEvent.Fire += Attack;
    }

    private void OnDisable()
    {
        attackEvent.Fire -= Attack;
    }

    void Attack(double strength)
    {
        CleanTargets();

        foreach (Hurtbox target in targets)
        {
            target.TakeDamage(strength, attackType);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Hurtbox victim;

        if (collision.TryGetComponent<Hurtbox>(out victim))
        {
            targets.Add(victim);
        }
    }

    void CleanTargets()
    {
        for (int i = targets.Count - 1; i >= 0; i--)
        {
            if (targets[i] == null) targets.RemoveAt(i);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Hurtbox victim;

        if (collision.TryGetComponent<Hurtbox>(out victim))
        {
            targets.Remove(victim);
        }
    }
}

public enum Attack
{
    TORNADO,
    TSUNAMI,
    EARTHQUAKE
}