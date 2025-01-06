using UnityEngine;
using UnityEngine.Events;

public class BuildingBehaviour : MonoBehaviour
{
    public double health;
    public double reward;
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

        // Permet de g�rer l'affichage des b�timents qui se superposent. Un b�timent plus au Sud qu'un autre s'affichera par dessus ce dernier.
        transform.position = transform.position + new Vector3(0, 0, transform.position.y);
    }

    private void Destroy()
    {
        moneyGain.Fire?.Invoke(reward);
        LaunchHitMarker(reward, Color.yellow);
        buildingDestroyed.Fire();
        Destroy(gameObject);
    }

    private void DisplayDamage(double damage)
    {
        LaunchHitMarker(damage, Color.red);
    }

    private void LaunchHitMarker(double value, Color color)
    {
        var hitMarker = Instantiate(hitMarkerPrefab, transform.position, Quaternion.identity);
        hitMarker.GetComponent<HitMarkerBehaviour>().Initialize(value, color);
    }
}
