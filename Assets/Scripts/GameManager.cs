using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("ScriptableObjects")]
    [SerializeField] private MapInfos mapInfos;

    private void OnDisable()
    {
        mapInfos.PlayerTransform = Vector3.zero;
        mapInfos.CameraTransform = new Vector3(0, 0, -10);
        mapInfos.MapBoxCollider = new Bounds(Vector3.zero, Vector3.zero);
    }
}
