using System;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "RSO_CurrentPlayerSize", menuName = "Catacosmic/RSO/CurrentPlayerSize")]
public class RSO_CurrentPlayerSize : ScriptableObject
{
    public Action<Vector3> onValueChanged;

    [Title("Parameters")]
    [ShowInInspector]
    private Vector3 _value;

    public Vector3 Value
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
