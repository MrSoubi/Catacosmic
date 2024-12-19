using UnityEngine;
using UnityEngine.Events;

public class BuildingBehaviour : MonoBehaviour
{
    public Wallet wallet;

    public SSO_BuildingData buildingData;

    public Hurtbox hurtbox;
    public Health health;

    private void OnEnable()
    {
        health.onDeath += Destroy;
    }

    private void Start()
    {
        health.SetHealth(buildingData.health);
        hurtbox.Initialize(buildingData.tsunamiMultiplicator, buildingData.tornadoMultiplicator, buildingData.earthquakeMultiplicator);
    }

    private void Destroy()
    {
        wallet.Add(buildingData.reward);
        Destroy(gameObject);
    }
}
