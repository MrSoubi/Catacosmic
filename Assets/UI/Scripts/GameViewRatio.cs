using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "GameViewRatio", menuName = "Data/UI/GameViewRatio")]
public class GameViewRatio : ScriptableObject
{
    public float ratio;

    private int screenHeight = 800;

    public float gameRatio, menuRatio;

    [Button]
    void SetRatio()
    {
        gameRatio = screenHeight * ratio;
        menuRatio = screenHeight * (1 - ratio);
    }
}