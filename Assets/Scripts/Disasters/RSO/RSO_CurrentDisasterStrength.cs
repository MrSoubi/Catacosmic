using System;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "RSO_CurrentDisasterStrength", menuName = "Catacosmic/RSO/CurrentDisaster Strength")]
public class RSO_CurrentDisasterStrength : ScriptableObject
{
    public Action<float> onValueChanged;

    [Title("Parameters")]
    [ShowInInspector]
    private float _value;

    public float Value
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