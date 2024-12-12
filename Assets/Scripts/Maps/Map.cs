using Sirenix.OdinInspector;
using UnityEngine;

public class Map : MonoBehaviour
{
    [Title("Output Data")]
    [SerializeField] private RSO_CurrentMapBounds currentMapBounds;

    private void Awake()
    {
        currentMapBounds.MapBounds = GetComponent<SpriteRenderer>().bounds;
    }
}
