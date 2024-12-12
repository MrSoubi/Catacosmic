using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Brush", menuName = "Catacosmic/Disasters/Brush")]
public class Brush : ScriptableObject
{
    public float[,] damageValues;

    [SerializeField] Texture2D brushTexture;

    [Button]
    void SetDamageValues()
    {
        damageValues = new float[brushTexture.width, brushTexture.height];

        for (int i = 0; i < brushTexture.width; i++)
        {
            for (int j = 0; j < brushTexture.height; j++)
            {
                damageValues[i, j] = 1 - brushTexture.GetPixel(i, j).grayscale;
                j++;
            }
            i++;
        }
    }
}
