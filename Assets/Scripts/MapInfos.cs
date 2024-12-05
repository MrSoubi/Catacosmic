using UnityEngine;

[CreateAssetMenu(fileName = "MapInfos", menuName = "Catacosmic/Map Infos")]
public class MapInfos : ScriptableObject
{
    public BoxCollider2D Collider;
    [SerializeField] private Vector3 playerTransform;
    [SerializeField] private Vector3 cameraTransform;
    [SerializeField] private Bounds mapboxCollider;

    public Vector3 PlayerTransform
    {
        get => playerTransform;
        set => playerTransform = value;
    }

    public Vector3 CameraTransform
    {
        get => cameraTransform;
        set => cameraTransform = value;
    }

    public Bounds MapBoxCollider
    {
        get => mapboxCollider;
        set => mapboxCollider = value;
    }
}
