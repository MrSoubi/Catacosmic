using System;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "RSO_CurrentDisasterStrength", menuName = "Catacosmic/RSO/CurrentDisasterStrength")]
public class RSO_CurrentDisasterStrength : ScriptableObject
{
    public Action<double> onValueChanged;

    [Title("Parameters")]
    [ShowInInspector]
    private double _value;

    public double Value
    {
        get => _value;
        set
        {
            if (_value == value) return;

            _value = value;

            onValueChanged?.Invoke(_value);
        }
    }
}