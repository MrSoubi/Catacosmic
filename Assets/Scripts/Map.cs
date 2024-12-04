using UnityEngine;

public class Map : MonoBehaviour
{
    [Header("ScriptableObjects")]
    [SerializeField] private MapInfos mapInfos;

    private void Awake()
    {
        mapInfos.MapBoxCollider = GetComponent<BoxCollider2D>().bounds;
    }
}
