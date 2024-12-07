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

    public RSO_PointerPositionOnScreen position;

    private void OnEnable()
    {
        position.onValueChanged += Paint;
    }

    private void OnDisable()
    {
        position.onValueChanged -= Paint;
    }

    void Paint(Vector2 screenPosition)
    {
        Vector2 localPosition = GetLocalPositionFromScreenPosition(screenPosition);

        Vector2 pixelTarget = localPosition;

        pixelTarget.x = (pixelTarget.x + spriteRenderer.bounds.extents.x) / spriteRenderer.bounds.size.x;
        pixelTarget.y = (pixelTarget.y + spriteRenderer.bounds.extents.y) / spriteRenderer.bounds.size.y;

        Debug.Log(pixelTarget);
        Vector2Int pixelTargetInt = new (Mathf.FloorToInt(pixelTarget.x * 1024), Mathf.FloorToInt(pixelTarget.y * 1024));

        Brush(pixelTargetInt, 10, Color.green);
    }

    Vector2 GetLocalPositionFromScreenPosition(Vector2 screenPosition)
    {
        Vector3 viewportPosition = new Vector3(screenPosition.x / Camera.main.pixelWidth, (Camera.main.pixelHeight - screenPosition.y) / Camera.main.pixelHeight, 1);

        Vector2 worldPosition = Camera.main.ViewportToWorldPoint(viewportPosition);

        Vector2 localPosition = spriteRenderer.worldToLocalMatrix * worldPosition;

        return localPosition;
    }

    void Brush(Vector2Int pixelPosition, int size, Color color)
    {
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                texture.SetPixel(pixelPosition.x + i, pixelPosition.y + j, color);
            }
        }

        texture.Apply();
    }

    public Color color = Color.red;
    IEnumerator ChangeColor()
    {
        int counter = 1000;

        LerpColor lerpColor = new LerpColor();

        while (counter > 0)
        {
            for (int i = 0; i < texture.width; i++)
            {
                for (int j = 0; j < texture.height; j++)
                {
                    texture.SetPixel(i, j, color);
                }
            }

            texture.Apply();

            yield return new WaitForEndOfFrame();

            counter--;
            color = lerpColor.Interpolate(Color.red, Color.blue, 1 - counter / 1000f);

            Debug.Log(color);
        }

        yield return null;
    }

    [Button]
    void EraseTexture()
    {
        for (int i = 0; i < texture.width; i++)
        {
            for (int j = 0; j < texture.height; j++)
            {
                texture.SetPixel(i, j, Color.white);
            }
        }

        texture.Apply();
    }
}
