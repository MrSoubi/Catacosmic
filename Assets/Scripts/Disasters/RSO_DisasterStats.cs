using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "DisasterStats", menuName = "Catacosmic/Disaster Stats")]
public class RSO_DisasterStats : ScriptableObject
{
    [Title("Parameters")]
    [SerializeField] private float force;
    [SerializeField] private float speed;
    [SerializeField] private float radius;
    [SerializeField, SuffixLabel("%")] private float criticChance;
    [SerializeField] private float criticMutliplier;

    private void OnValidate()
    {
        if(force < 0)
        {
            force = 0;
        }

        if (speed < 0)
        {
            speed = 0;
        }

        if (radius < 0.5f)
        {
            radius = 0.5f;
        }

        if (criticChance < 0)
        {
            criticChance = 0;
        }

        if (criticMutliplier < 1)
        {
            criticMutliplier = 1;
        }
    }

    public float Force
    {
        get => force;
        set => force = Mathf.Max(value, 0);
    }

    public float Speed
    {
        get => speed;
        set => speed = Mathf.Max(value, 0);
    }

    public float Radius
    {
        get => radius;
        set => radius = Mathf.Max(value, 0.5f);
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
}
