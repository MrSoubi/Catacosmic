using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "DisasterStats", menuName = "Catacosmic/Disaster Stats")]
public class RSO_DisasterStats : ScriptableObject
{
    [Title("Parameters")]
    [SerializeField] private float force;
    [SerializeField] private float speed;
    [SerializeField] private float radius;

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

        if (radius < 0)
        {
            radius = 0;
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
        set => radius = Mathf.Max(value, 0);
    }
}
