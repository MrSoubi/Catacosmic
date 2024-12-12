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

        for (int i = - size / 2; i < size / 2; i++)
        {
            for (int j =  - size / 2; j < size / 2; j++)
            {
                if (texture.GetPixel(pixelPosition.x + i, pixelPosition.y + j) == Color.black)
                {
                    playerMoney.Value.Add(mapStrength);

                    color = Color.white;

                    texture.SetPixel(pixelPosition.x + i, pixelPosition.y + j, color);
                }

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
