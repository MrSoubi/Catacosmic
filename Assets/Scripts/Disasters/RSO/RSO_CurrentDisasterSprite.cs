using System;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "RSO_CurrentDisasterSprite", menuName = "Catacosmic/RSO/Current Disaster Sprite")]
public class RSO_CurrentDisasterSprite : ScriptableObject
{
    public Action<Sprite> onValueChanged;

    [Title("Parameters")]
    [ShowInInspector]
    private Sprite _value;

    public Sprite Value
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
