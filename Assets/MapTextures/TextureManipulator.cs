using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Splines.Interpolators;
using Sirenix.OdinInspector;
using UnityEditor;

public class TextureManipulator : MonoBehaviour
{
    [MinValue(0)]
    public float test;
    public Texture2D texture;
    public SpriteRenderer spriteRenderer;

    public RSO_PointerPosition position;

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

        Vector2 pixelTarget = new (localPosition.x * spriteRenderer.bounds.extents.x, localPosition.y * spriteRenderer.bounds.extents.y);

        Vector2Int pixelTargetInt = new (Mathf.FloorToInt(pixelTarget.x * 1024), Mathf.FloorToInt(pixelTarget.y * 1024));

        Brush(pixelTargetInt, 10, Color.green);
    }

    Vector2 GetLocalPositionFromScreenPosition(Vector2 screenPosition)
    {
        Vector2 screenPositionRatio;

        screenPositionRatio.x = (2 * screenPosition.x / Camera.main.pixelWidth) - 1;
        screenPositionRatio.y = (2 * screenPosition.y / Camera.main.pixelHeight) - 1;



        Debug.Log(Screen.width + " / " + Screen.height);

        return screenPositionRatio;



/*        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);

        

        Vector2 localPosition = spriteRenderer.worldToLocalMatrix * worldPosition;

        localPosition += spriteRenderer.size / 2;

        Debug.Log(screenPosition + " -> " + worldPosition + " -> " + localPosition);

        return localPosition;*/
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
