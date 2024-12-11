using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "RSO_DisasterStats", menuName = "Catacosmic/RSO/Disaster Stats")]
public class RSO_DisasterStats : ScriptableObject
{
    [Title("Parameters")]
    [SerializeField] private float radius;
    [SerializeField] private float speed;
    [SerializeField] private float damage;
    [SerializeField, SuffixLabel("%")] private float criticChance;
    [SerializeField] private float criticMutliplier;
    [SerializeField, SuffixLabel("s")] private float attackSpeed;

    private void OnValidate()
    {
        if (radius < 0.5f)
        {
            radius = 0.5f;
        }

        if (speed < 0)
        {
            speed = 0;
        }

        if (damage < 0)
        {
            damage = 0;
        }

        if (criticChance < 0)
        {
            criticChance = 0;
        }

        if (criticMutliplier < 1)
        {
            criticMutliplier = 1;
        }

        if (attackSpeed < 0)
        {
            attackSpeed = 0;
        }
    }

    public float Radius
    {
        get => radius;
        set => radius = Mathf.Max(value, 0.5f);
    }

    public float Speed
    {
        get => speed;
        set => speed = Mathf.Max(value, 0);
    }

    public float Force
    {
        get => damage;
        set => damage = Mathf.Max(value, 0);
    }

    public float CriticChance
    {
        get => criticChance;
        set => criticChance = Mathf.Max(value, 0);
    }

    public float CriticMutliplier
    {
        get => criticMutliplier;
        set => criticMutliplier = Mathf.Max(value, 1);
    }

    public float AttackSpeed
    {
        get => attackSpeed;
        set => attackSpeed = Mathf.Max(value, 0);
    }
}
