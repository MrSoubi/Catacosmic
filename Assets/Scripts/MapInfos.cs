using UnityEngine;

[CreateAssetMenu(fileName = "MapInfos", menuName = "Catacosmic/Map Infos")]
public class MapInfos : ScriptableObject
{
    [Header("References")]
    [SerializeField] private BoxCollider2D boxCollider2DRef;
    [SerializeField] private GameObject playerRef;

    [Header("Parameters")]
    [SerializeField] private Vector3 playerTransform;
    [SerializeField] private Vector3 cameraTransform;
    [SerializeField] private Bounds mapboxCollider;

    public BoxCollider2D BoxCollider2DRef
    {
        get => boxCollider2DRef;
        set => boxCollider2DRef = value;
    }

    public GameObject PlayerRef
    {
        get => playerRef;
        set => playerRef = value;
    }

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
