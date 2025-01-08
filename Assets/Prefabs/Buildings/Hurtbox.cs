using UnityEngine;

public class Hurtbox : MonoBehaviour
{
    [SerializeField] Health health;

    float tsunamiMultiplicator;
    float tornadoMultiplicator;
    float earthquakeMultiplicator;

    public void Initialize(float tsunamiMultiplicator, float tornadoMultiplicator, float earthquakeMultiplicator)
    {
        this.tornadoMultiplicator = tornadoMultiplicator;
        this.tsunamiMultiplicator = tsunamiMultiplicator;
        this.earthquakeMultiplicator = earthquakeMultiplicator;
    }

    public void TakeDamage(float strength, Attack attack)
    {
        switch (attack)
        {
            case Attack.TORNADO:
                strength *= tornadoMultiplicator;
                break;
            case Attack.TSUNAMI:
                strength *= tsunamiMultiplicator;
                break;
            case Attack.EARTHQUAKE:
                strength *= earthquakeMultiplicator;
                break;
            default:
                break;
        }

        health.TakeDamage(strength);
    }
}
