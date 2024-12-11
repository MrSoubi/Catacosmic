using System;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "RSO_CurrentDisasterSize", menuName = "Catacosmic/RSO/CurrentDisaster Size")]
public class RSO_CurrentDisasterSize : ScriptableObject
{
    public Action<int> onValueChanged;

    [Title("Parameters")]
    [ShowInInspector]
    private int _value;

    public int Value
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