using Sirenix.OdinInspector;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Title("ScriptableObjects")]
    [SerializeField] private RSO_MapInfos mapInfos;
    [SerializeField] private RSO_DisasterStats disasterStats;

    private void OnDisable()
    {
        mapInfos.BoxCollider2DRef = null;
        mapInfos.PlayerRef = null;
        mapInfos.PlayerTransform = Vector3.zero;
        mapInfos.CameraTransform = new Vector3(0, 0, -10);
        mapInfos.MapBoxCollider = new Bounds(Vector3.zero, Vector3.zero);
    }
}
