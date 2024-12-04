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
    public string position;

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

            position = _value.x + " / " + _value.y;
        }
    }
}