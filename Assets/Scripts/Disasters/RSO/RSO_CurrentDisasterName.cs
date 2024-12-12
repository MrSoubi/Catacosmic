using System;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "RSO_CurrentDisasterName", menuName = "Catacosmic/RSO/Current Disaster Name")]
public class RSO_CurrentDisasterName : ScriptableObject
{
    public Action<string> onValueChanged;

    [Title("Parameters")]
    [ShowInInspector]
    private string _value;

    public string Value
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
