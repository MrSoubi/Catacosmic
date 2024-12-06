using Sirenix.OdinInspector;
using UnityEngine;

public class Map : MonoBehaviour
{
    [Title("ScriptableObjects")]
    [SerializeField] private RSO_MapInfos mapInfos;

    private void Awake()
    {
        mapInfos.BoxCollider2DRef = GetComponent<BoxCollider2D>();
        mapInfos.MapBoxCollider = GetComponent<BoxCollider2D>().bounds;
    }
}
