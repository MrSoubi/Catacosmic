using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "SSO_DisasterStats", menuName = "Catacosmic/RSO/Disaster Stats")]
public class SSO_DisasterStats : ScriptableObject
{
    [Title("Parameters")]
    [SerializeField] private float radius;
    [SerializeField] private float speed;
    [SerializeField] private float strength;
    [SerializeField, SuffixLabel("%")] private float criticChance;
    [SerializeField] private float criticMutliplier;
    [SerializeField, SuffixLabel("s")] private float attackDelay;
    [SerializeField] private float resitanceMulitiplier;

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

        if (strength < 0)
        {
            strength = 0;
        }

        if (criticChance < 0)
        {
            criticChance = 0;
        }

        if (criticMutliplier < 1)
        {
            criticMutliplier = 1;
        }

        if (attackDelay < 0)
        {
            attackDelay = 0;
        }

        if (resitanceMulitiplier < 0)
        {
            resitanceMulitiplier = 0;
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

    public float CriticMutliplier
    {
        get => criticMutliplier;
        set => criticMutliplier = Mathf.Max(value, 1);
    }

    public float AttackDelay
    {
        get => attackDelay;
        set => attackDelay = Mathf.Max(value, 0);
    }

    public float ResitanceMulitiplier
    {
        get => resitanceMulitiplier;
        set => resitanceMulitiplier = Mathf.Max(value, 0);
    }
}
