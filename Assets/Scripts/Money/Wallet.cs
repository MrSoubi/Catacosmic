using Sirenix.OdinInspector;
using System.Numerics;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    public RSO_PlayerMoney money;

    private void Start()
    {
        money.Value = 0;
    }

    [Button]
    void Add(BigInteger amount)
    {
        money.Value += amount;
    }

    [Button]
    void Remove(BigInteger amount)
    {
        money.Value -= amount;
    }

    [Button]
    void Print()
    {
        Debug.Log(money.Value.ToString());
    }
}
