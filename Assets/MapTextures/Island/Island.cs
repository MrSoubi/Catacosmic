using Sirenix.OdinInspector;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class Island : MonoBehaviour
{
    [Title("References")]
    [SerializeField] SpriteRenderer islandRenderer;
    [SerializeField] Texture2D damageTextureTemplate;
    [SerializeField] Wallet playerWallet;

    Texture2D damageTexture;

    BigInteger mapStrength = 50;

    bool isDisasterInVicinity = false;

    private void Start()
    {
        InitializeDamageTexture();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isDisasterInVicinity = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isDisasterInVicinity = false;
    }

    private void InitializeDamageTexture()
    {
        damageTexture = new Texture2D(damageTextureTemplate.width, damageTextureTemplate.height);
        damageTexture.SetPixels(damageTextureTemplate.GetPixels());
        damageTexture.Apply();
    }

    #region Inspector
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
    #endregion

    void TakeDamage(UnityEngine.Vector2 position, BigInteger attackStrength, int radius)
    {
        UnityEngine.Vector2 worldPosition = position;
        UnityEngine.Vector2 pixelTarget;

        pixelTarget.x = (worldPosition.x + islandRenderer.bounds.extents.x) / islandRenderer.bounds.size.x;
        pixelTarget.y = (worldPosition.y + islandRenderer.bounds.extents.y) / islandRenderer.bounds.size.y;

        Vector2Int pixelTargetInt = new(Mathf.FloorToInt(pixelTarget.x * 1024), Mathf.FloorToInt(pixelTarget.y * 1024));

        ApplyBrush(pixelTargetInt, radius, attackStrength);
    }

    
    void ApplyBrush(Vector2Int pixelPosition, int radius, BigInteger attackStrength)
    {
        for (int i = -radius; i < radius; i++)
        {
            for (int j = -radius / 2; j < radius / 2; j++)
            {
                Vector2Int pixelTarget = pixelPosition + new Vector2Int(i, j);
                BigInteger pixelLife = GetPixelLife(pixelTarget);

                if (pixelLife > 0)
                {
                    BigInteger damages = BigInteger.Max(0, pixelLife - attackStrength);

                    playerWallet.Add(damages);

                    pixelLife -= damages;

                    SetPixelLife(pixelTarget, pixelLife);
                }
            }
        }

        damageTexture.Apply();
    }

    BigInteger GetPixelLife(Vector2Int pixelPosition)
    {
        BigInteger result = new BigInteger(damageTexture.GetPixel(pixelPosition.x, pixelPosition.y).a) * mapStrength;
        return result;
    }

    void SetPixelLife(Vector2Int pixelPosition, BigInteger value)
    {
        Color pixelColor = damageTexture.GetPixel(pixelPosition.x, pixelPosition.y);

        pixelColor.a = 0;
    }
}
