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
    public RSO_CurrentDisasterSize disasterSize;
    public RSO_CurrentDisasterStrength disasterStrength;
    public RSE_DisasterAttack disasterAttack;

    public RSO_PlayerMoney playerMoney;

    public Brush brush;

    private void OnEnable()
    {
        disasterAttack.Fire += Paint;
    }

    private void OnDisable()
    {
        disasterAttack.Fire -= Paint;
    }

    void Paint()
    {
        Vector2 worldPosition = position.Value;
        Vector2 pixelTarget;

        pixelTarget.x = (worldPosition.x + spriteRenderer.bounds.extents.x) / spriteRenderer.bounds.size.x;
        pixelTarget.y = (worldPosition.y + spriteRenderer.bounds.extents.y) / spriteRenderer.bounds.size.y;

        Vector2Int pixelTargetInt = new (Mathf.FloorToInt(pixelTarget.x * 1024), Mathf.FloorToInt(pixelTarget.y * 1024));

        ApplyBrush(pixelTargetInt);
    }

    float mapStrength = 50f;
    void ApplyBrush(Vector2Int pixelPosition)
    {
        Color color;
        int size = disasterSize.Value;
        int brushWidth = brush.damageValues.GetLength(0) * size;
        int brushHeight = brush.damageValues.GetLength(1) * size;

        for (int i = - brushWidth / 2; i < brushWidth / 2; i++)
        {
            for (int j =  - brushHeight / 2; j < brushHeight / 2; j++)
            {
                float pixelLife = 1 - texture.GetPixel(pixelPosition.x + i, pixelPosition.y + j).grayscale;
                float newPixelLife = 1 - Mathf.Max(pixelLife - brush.damageValues[i / size,j / size], 0);

                int money = (int)((pixelLife - newPixelLife) * mapStrength);
                playerMoney.Value.Add(money);
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
