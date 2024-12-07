using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "MapInfos", menuName = "Catacosmic/Map Infos")]
public class RSO_MapInfos : ScriptableObject
{
    [Title("Parameters")]
    [SerializeField] private Vector3 playerTransform;
    [SerializeField] private Vector3 playerSize;
    [SerializeField] private Vector3 cameraTransform;
    [SerializeField] private Bounds mapBounds;

    public Vector3 PlayerTransform
    {
        get => playerTransform;
        set => playerTransform = value;
    }

    public Vector3 PlayerSize
    {
        get => playerSize;
        set => playerSize = value;
    }

    public Vector3 CameraTransform
    {
        get => cameraTransform;
        set => cameraTransform = value;
    }

    public Bounds MapBounds
    {
        get => mapBounds;
        set => mapBounds = value;
    }
}
