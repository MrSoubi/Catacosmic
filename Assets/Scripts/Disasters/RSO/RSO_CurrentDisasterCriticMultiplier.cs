using System;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "RSO_CurrentDisasterCriticMultiplier", menuName = "Catacosmic/RSO/CurrentDisasterCriticMultiplier")]
public class RSO_CurrentDisasterCriticMultiplier : ScriptableObject
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
