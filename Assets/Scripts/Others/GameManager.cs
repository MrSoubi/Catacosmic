using Sirenix.OdinInspector;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Title("Output Data")]
    //[SerializeField] private RSO_CurrentMapBounds currentMapBounds;
    //[SerializeField] private RSO_CurrentPlayerSize currentPlayerSize;

    [Title("Default Disaster")]
    [SerializeField] private GameObject prefab;

    private GameObject currentDisaster;

    private void OnDisable()
    {
        //currentMapBounds.MapBounds = new Bounds(Vector3.zero, Vector3.zero);
        //currentPlayerSize.Value = Vector3.zero;
    }

    private void Start()
    {
        SpawnDisater(prefab);
    }

    /// <summary>
    /// Destroy the Current Disaster & Spawn the new Disaster
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
