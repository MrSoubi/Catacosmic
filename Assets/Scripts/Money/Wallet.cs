using Sirenix.OdinInspector;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    public RSO_PlayerMoney money;

    private void Start()
    {
        money.Value = 0;
    }

    [Button]
    void Add(int amount)
    {
        money.Value.Add(amount);
    }

    [Button]
    void Remove(int amount)
    {
        money.Value.Subtract(amount);
    }

    [Button]
    void Print()
    {
        Debug.Log(money.Value.ToString());
    }
}
