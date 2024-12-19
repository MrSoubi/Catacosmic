using System;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "RSE_MoneyGain", menuName = "Catacosmic/RSE/MoneyGain")]
public class RSE_MoneyGain : ScriptableObject
{
    public Action<double> Fire;

    [Button]
    private void FireEvent(double value)
    {
        Fire?.Invoke(value);
    }
}