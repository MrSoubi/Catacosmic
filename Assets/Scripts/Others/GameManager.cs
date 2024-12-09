using Sirenix.OdinInspector;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Title("ScriptableObjects")]
    [SerializeField] private RSO_MapInfos mapInfos;

    [Title("Default Disaster")]
    [SerializeField] private GameObject prefab;

    private GameObject currentDisaster;

    private void OnDisable()
    {
        mapInfos.PlayerSize = Vector3.zero;
        mapInfos.PlayerTransform = Vector3.zero;
        mapInfos.CameraTransform = new Vector3(0, 0, -10);
        mapInfos.MapBounds = new Bounds(Vector3.zero, Vector3.zero);
    }

    private void Start()
    {
        SpawnDisater(prefab);
    }

    /// <summary>
    /// Destroy Disaster & Spawn the new Disaster
    /// </summary>
    /// <param name="disaster"></param>
    private void SpawnDisater(GameObject disaster)
    {
        if(currentDisaster != null)
        {
            Destroy(currentDisaster);
        }

        currentDisaster = Instantiate(disaster, transform.position, Quaternion.identity, null);
    }
}
