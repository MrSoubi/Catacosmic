using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "RSO_MapInfos", menuName = "Catacosmic/RSO/Map Infos")]
public class RSO_MapInfos : ScriptableObject
{
    [Title("Parameters")]
    [SerializeField] private Vector3 playerSize;
    [SerializeField] private Bounds mapBounds;

    public Vector3 PlayerSize
    {
        get => playerSize;
        set => playerSize = value;
    }

    public Bounds MapBounds
    {
        get => mapBounds;
        set => mapBounds = value;
    }
}
