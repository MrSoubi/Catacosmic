using Sirenix.OdinInspector;
using UnityEngine;

public class Map : MonoBehaviour
{
    [Title("ScriptableObjects")]
    [SerializeField] private RSO_MapInfos mapInfos;

    private void Awake()
    {
        mapInfos.MapBounds = GetComponent<SpriteRenderer>().bounds;
    }
}
