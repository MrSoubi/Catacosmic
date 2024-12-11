using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Splines.Interpolators;
using Sirenix.OdinInspector;
using UnityEditor;

public class TextureManipulator : MonoBehaviour
{
    public Texture2D texture;
    public SpriteRenderer spriteRenderer;

    public RSO_DisasterPosition position;

    private void OnEnable()
    {
        position.onValueChanged += Paint;
    }

    private void OnDisable()
    {
        position.onValueChanged -= Paint;
    }

    void Paint(Vector2 worldPosition)
    {
        Vector2 pixelTarget = worldPosition;

        pixelTarget.x = (worldPosition.x + spriteRenderer.bounds.extents.x) / spriteRenderer.bounds.size.x;
        pixelTarget.y = (worldPosition.y + spriteRenderer.bounds.extents.y) / spriteRenderer.bounds.size.y;

        Vector2Int pixelTargetInt = new (Mathf.FloorToInt(pixelTarget.x * 1024), Mathf.FloorToInt(pixelTarget.y * 1024));

        Brush(pixelTargetInt, 5);
    }

    float brushStrength = 1f;
    float mapStrength = 50f;
    void Brush(Vector2Int pixelPosition, int size)
    {
        Color color;

        for (int i = -size / 2; i < size / 2; i++)
        {
            for (int j = -size / 2; j < size / 2; j++)
            {
                string debug;
                float pixelLife = 1 - texture.GetPixel(pixelPosition.x + i, pixelPosition.y + j).grayscale;
                debug = "life: " + pixelLife;
                float damage = brushStrength / mapStrength;
                debug += ", damage: " + damage;
                float newPixelLife = 1 - Mathf.Max(pixelLife - damage, 0);
                debug += ", life: " + newPixelLife;

                Debug.Log(debug);

                color = new Color(newPixelLife, newPixelLife, newPixelLife);

                texture.SetPixel(pixelPosition.x + i, pixelPosition.y + j, color);
            }
        }

        texture.Apply();
    }

    [Button]
    void EraseTexture()
    {
        for (int i = 0; i < texture.width; i++)
        {
            for (int j = 0; j < texture.height; j++)
            {
                texture.SetPixel(i, j, Color.black);
            }
        }

        texture.Apply();
    }
}
