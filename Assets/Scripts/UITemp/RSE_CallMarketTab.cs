using System;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "RSE_CallMarketTab", menuName = "Catacosmic/UI/RSE/CallMarketTab")]
public class RSE_CallMarketTab : ScriptableObject
{
    public Action Fire;

    [Button]
    private void FireEvent()
    {
        Fire?.Invoke();
    }
}
