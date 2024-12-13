using System;
using Sirenix.OdinInspector;
using UnityEngine;
using System.Numerics;

[CreateAssetMenu(fileName = "RSO_PlayerMoney", menuName = "Catacosmic/RSO/PlayerMoney")]
public class RSO_PlayerMoney : ScriptableObject
{
    public Action<BigInteger> onValueChanged;

    [Title("Parameters")]
    [ShowInInspector]
    private BigInteger _value;

    public BigInteger Value
    {
        get => _value;
        set
        {
            if (_value == value) return;

            _value = value;

            onValueChanged?.Invoke(_value);
        }
    }

    [Button]
    void Print()
    {
        Debug.Log(_value.ToString());
    }

    [Button]
    void Set(int amount)
    {
        Value = amount;
    }
}