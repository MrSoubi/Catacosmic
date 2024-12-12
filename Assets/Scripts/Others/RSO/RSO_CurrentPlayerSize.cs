using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "RSO_CurrentPlayerSize", menuName = "Catacosmic/RSO/CurrentPlayerSize")]
public class RSO_CurrentPlayerSize : ScriptableObject
{
    [Title("Parameters")]
    [SerializeField] private Vector3 playerSize;

    public Vector3 PlayerSize
    {
        get => playerSize;
        set => playerSize = value;
    }
}
