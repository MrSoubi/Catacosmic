using System;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "RSO_PlayerMoney", menuName = "Catacosmic/RSO/PlayerMoney")]
public class RSO_PlayerMoney : ScriptableObject
{
    public Action<BigFloatNumerics.BigNumber> onValueChanged;

    [Title("Parameters")]
    [ShowInInspector]
    private BigFloatNumerics.BigNumber _value;

    public BigFloatNumerics.BigNumber Value
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