using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "RSO_PointerPositionOnScreen", menuName = "Data/RSO/PointerPositionOnScreen")]
public class RSO_PointerPositionOnScreen : ScriptableObject
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
            if (_value == value) return;

            _value = value;

            position = _value;

            onValueChanged?.Invoke(_value);
        }
    }
}