using UnityEngine;
using UnityEngine.Events;

public class BuildingBehaviour : MonoBehaviour
{
    public float health;
    public float reward;
    public float tornadoMultiplicator;
    public float tsunamiMultiplicator;
    public float earthquakeMultiplicator;

    public GameObject hitMarkerPrefab;

    public RSE_MoneyGain moneyGain;
    public RSE_BuildingDestroyed buildingDestroyed;

    public Hurtbox hurtbox;
    public Health healthComponent;

    private void OnEnable()
    {
        healthComponent.onDeath += Destroy;
        healthComponent.onTookDamage += DisplayDamage;
    }

    private void OnDisable()
    {
        healthComponent.onDeath += Destroy;
        healthComponent.onTookDamage += DisplayDamage;
    }

    private void Start()
    {
        healthComponent.SetHealth(health);
        hurtbox.Initialize(tsunamiMultiplicator, tornadoMultiplicator, earthquakeMultiplicator);
    }

    private void Destroy()
    {
        moneyGain.Fire?.Invoke(reward);
        LaunchHitMarker(reward, Color.yellow);
        //buildingDestroyed.Fire();
        Destroy(gameObject);
    }

    private void DisplayDamage(float damage)
    {
        LaunchHitMarker(damage, Color.red);
    }

    private void LaunchHitMarker(float value, Color color)
    {
        var hitMarker = Instantiate(hitMarkerPrefab, transform.position, Quaternion.identity);
        hitMarker.GetComponent<HitMarkerBehaviour>().Initialize(value, color);
    }
}
