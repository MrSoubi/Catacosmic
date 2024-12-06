using Sirenix.OdinInspector;
using UnityEngine;

public class Disaster : MonoBehaviour
{
    [Title("ScriptableObjects")]
    [SerializeField] private RSO_DisasterStats disasterStats;

    public RSO_DisasterStats DisasterStats
    {
        get => disasterStats;
    }
}
