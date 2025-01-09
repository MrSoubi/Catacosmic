using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "SSO_DisasterData", menuName = "Catacosmic/SSO/DisasterData")]
public class SSO_DisasterData : ScriptableObject
{
    [Title("Parameters")]
    [SerializeField] private string name;
    [SerializeField] private Sprite sprite;
    [SerializeField] private float size;
    [SerializeField] private float velocity;
    [SerializeField] private float strength;
    [SerializeField, SuffixLabel("%")] private float criticChance;
    [SerializeField] private float criticMultiplier;
    [SerializeField, SuffixLabel("s")] private float attackSpeed;

    private void OnValidate()
    {
        if (size < 0.01f)
        {
            size = 0.01f;
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

    public string Name
    {
        get => name;
        set => name = value;
    }

    public Sprite Sprite
    {
        get => sprite;
        set => sprite = value;
    }

    public float Size
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
        set
        {
            if (value < 0)
            {
                strength = 0;
            }
        }
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
