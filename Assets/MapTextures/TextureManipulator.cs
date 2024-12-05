using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Splines.Interpolators;

public class TextureManipulator : MonoBehaviour
{
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
        Vector2 position = Camera.main.ScreenToWorldPoint(screenPosition);
        Debug.Log("Screen position: " + position);

        Vector2 spriteSize = new (spriteRenderer.bounds.size.x, spriteRenderer.bounds.size.y);
        Debug.Log("Sprite size: " +  spriteSize);

        Vector4 temp = spriteRenderer.worldToLocalMatrix * position;
        Vector2 positionOnLocalSpace = new(temp.x, temp.y);
        positionOnLocalSpace += (spriteSize / 2);
        Debug.Log("Position on local space: " + positionOnLocalSpace);


        Vector2 pixelTarget = new (positionOnLocalSpace.x / spriteRenderer.bounds.size.x, positionOnLocalSpace.y / spriteRenderer.bounds.size.y);
        Vector2Int pixelTargetInt = new (Mathf.FloorToInt(pixelTarget.x * 1024), 1024 - Mathf.FloorToInt(pixelTarget.y * 1024));
        Debug.Log("Pixel target: " + pixelTargetInt);

        Brush(pixelTargetInt, 10, Color.green);
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
}
