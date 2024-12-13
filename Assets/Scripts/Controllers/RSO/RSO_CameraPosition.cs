using System;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "RSO_CameraPosition", menuName = "Catacosmic/RSO/CameraPosition")]
public class RSO_CameraPosition : ScriptableObject
{
    public Action<Vector2> onValueChanged;

    [Title("Parameters")]
    [ShowInInspector]
    private Vector2 _value;

    public Vector2 Value
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
