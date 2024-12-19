using Sirenix.OdinInspector;
using System.Numerics;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    public RSO_PlayerMoney money;
    public RSE_MoneyGain moneyGain;

    private void OnEnable()
    {
        moneyGain.Fire += Add;
    }

    private void OnDisable()
    {
        moneyGain.Fire -= Add;
    }

    private void Start()
    {
        money.Value = 0;
    }

    [Button]
    public void Add(double amount)
    {
        print(amount);
        money.Value += amount;
    }

    [Button]
    public void Remove(double amount)
    {
        money.Value -= amount;
    }

    [Button]
    void Print()
    {
        Debug.Log(money.Value.ToString());
    }
}
