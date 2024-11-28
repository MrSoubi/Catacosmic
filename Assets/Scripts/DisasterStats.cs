using UnityEngine;

[CreateAssetMenu(fileName = "NewDisasterStats", menuName = "Catacosmic/Disaster Stats")]
public class DisasterStats : ScriptableObject
{
    public float size;
    public float speed;
    public float criticChances;
    public float criticMultiplier;
    public float resistance;
    public float reloadTime;
}