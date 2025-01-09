using System;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "RSO_CurrentDisasterSize", menuName = "Catacosmic/RSO/CurrentDisasterSize")]
public class RSO_CurrentDisasterSize : ScriptableObject
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

    [Button]
    public void Call()
    {
        onValueChanged?.Invoke(_value);
    }
}