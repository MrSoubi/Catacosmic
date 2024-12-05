using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "RSO_PointerPosition", menuName = "Data/RSO/PointerPosition")]
public class RSO_PointerPosition : ScriptableObject
{
    public Action<Vector2> onValueChanged;

    [ShowInInspector]
    private Vector2 _value;

    public Vector2 position;

    public Vector2 Value
    {
        get => _value;
        set
        {
            if (_value.x == value.x && _value.y == Screen.height - value.y) return;

            _value.x = value.x;
            _value.y = Screen.height - value.y;

            position = _value;

            onValueChanged?.Invoke(_value);
        }
    }
}