using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "SSO_DisasterStats", menuName = "Catacosmic/SSO/Disaster Stats")]
public class SSO_DisasterStats : ScriptableObject
{
    [Title("Parameters")]
    [SerializeField] private int size;
    [SerializeField] private float velocity;
    [SerializeField] private float strength;
    [SerializeField, SuffixLabel("%")] private float criticChance;
    [SerializeField] private float criticMultiplier;
    [SerializeField, SuffixLabel("s")] private float attackSpeed;

    private void OnValidate()
    {
        if (size < 1)
        {
            size = 1;
        }

        if (velocity < 0)
        {
            velocity = 0;
        }

        if (strength < 0)
        {
            strength = 0;
        }

        if (criticChance < 0)
        {
            criticChance = 0;
        }

        if (criticMultiplier < 1)
        {
            criticMultiplier = 1;
        }

        if (attackSpeed < 0)
        {
            attackSpeed = 0;
        }
    }

    public int Size
    {
        get => size;
        set => size = Mathf.Max(value, 1);
    }

    public float Velocity
    {
        get => velocity;
        set => velocity = Mathf.Max(value, 0);
    }

    public float Strength
    {
        get => strength;
        set => strength = Mathf.Max(value, 0);
    }

    public float CriticChance
    {
        get => criticChance;
        set => criticChance = Mathf.Max(value, 0);
    }

    public float CriticMultiplier
    {
        get => criticMultiplier;
        set => criticMultiplier = Mathf.Max(value, 1);
    }

    public float AttackSpeed
    {
        get => attackSpeed;
        set => attackSpeed = Mathf.Max(value, 0);
    }
}