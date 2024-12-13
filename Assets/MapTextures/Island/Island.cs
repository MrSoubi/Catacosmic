using Sirenix.OdinInspector;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;

public class Island : MonoBehaviour
{
    [Title("References")]
    [SerializeField] SpriteRenderer mapRenderer;
    [SerializeField] Texture2D damageTextureTemplate;

    Texture2D damageTexture;

    private void Start()
    {
        InitializeDamageTexture();
    }

    private void InitializeDamageTexture()
    {
        damageTexture = new Texture2D(damageTextureTemplate.width, damageTextureTemplate.height);
        damageTexture.SetPixels(damageTextureTemplate.GetPixels());
        damageTexture.Apply();
    }

    [Title("Informations")]
    [SerializeField]
    [LabelText("@\"Life: \" + life.ToString(\"e3\")")]
    BigInteger life;

    [SerializeField]
    [LabelText("@\"Tornado resistance: \" + tornadoResistance.ToString(\"e3\")")]
    BigInteger tornadoResistance;
    [SerializeField]
    [LabelText("@\"Tsunami resistance: \" + tsunamiResistance.ToString(\"e3\")")]
    BigInteger tsunamiResistance;
    [SerializeField]
    [LabelText("@\"Earthquake resistance: \" + earthquakeResistance.ToString(\"e3\")")]
    BigInteger earthquakeResistance;

    [Button]
    void SetLife(int significand, int power)
    {
        life = significand;

        for (int i = 0; i < power; i++)
        {
            life *= 10;
        }
    }

    [Button]
    void SetTornadoResistance(int significand, int power)
    {
        tornadoResistance = significand;

        for (int i = 0; i < power; i++)
        {
            tornadoResistance *= 10;
        }
    }

    [Button]
    void SetTsunamiResistance(int significand, int power)
    {
        tsunamiResistance = significand;

        for (int i = 0; i < power; i++)
        {
            tsunamiResistance *= 10;
        }
    }

    [Button]
    void SetEarthquakeResistance(int significand, int power)
    {
        earthquakeResistance = significand;

        for (int i = 0; i < power; i++)
        {
            earthquakeResistance *= 10;
        }
    }
}
