using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "RSO_CurrentMapBounds", menuName = "Catacosmic/RSO/Current Map Bounds")]
public class RSO_CurrentMapBounds : ScriptableObject
{
    [Title("Parameters")]
    [SerializeField] private Bounds mapBounds;

    public Bounds MapBounds
    {
        get => mapBounds;
        set => mapBounds = value;
    }
}
