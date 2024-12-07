using Sirenix.OdinInspector;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Title("ScriptableObjects")]
    [SerializeField] private RSO_MapInfos mapInfos;

    private void OnDisable()
    {
        mapInfos.PlayerSize = Vector3.zero;
        mapInfos.PlayerTransform = Vector3.zero;
        mapInfos.CameraTransform = new Vector3(0, 0, -10);
        mapInfos.MapBounds = new Bounds(Vector3.zero, Vector3.zero);
    }
}
