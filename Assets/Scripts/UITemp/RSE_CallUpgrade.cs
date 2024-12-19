using System;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "RSE_CallUpgrade", menuName = "Catacosmic/UI/RSE/CallUpgrade")]
public class RSE_CallUpgrade : ScriptableObject
{
    public Action Fire;

    [Button]
    private void FireEvent()
    {
        Fire?.Invoke();
    }
}
