using UnityEngine;

public class Map : MonoBehaviour
{
    [Header("ScriptableObjects")]
    [SerializeField] private MapInfos mapInfos;

    private void Awake()
    {
        mapInfos.BoxCollider2DRef = GetComponent<BoxCollider2D>();
        mapInfos.MapBoxCollider = GetComponent<BoxCollider2D>().bounds;
    }
}
