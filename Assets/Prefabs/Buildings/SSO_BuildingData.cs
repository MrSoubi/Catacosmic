using UnityEngine;

[CreateAssetMenu(fileName = "Building Data", menuName = "Catacosmic/Building")]
public class SSO_BuildingData : ScriptableObject
{
    public Texture2D texture;
    public double health;
    public double reward;
    public float tornadoMultiplicator;
    public float tsunamiMultiplicator;
    public float earthquakeMultiplicator;
}
