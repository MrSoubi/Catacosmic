using Sirenix.OdinInspector;
using UnityEngine;

public class BigNumberTest : MonoBehaviour
{
    BigFloatNumerics.BigNumber bigNumber = 0;

    [Button]
    void Add(int value)
    {
        bigNumber.Add(value);
    }

    [Button]
    void Multiply(int value)
    {
        bigNumber.Multiply(value);
    }

    [Button]
    void PrintFriendly()
    {
        Debug.Log(bigNumber.ToHumanFriendlyString());
    }

    [Button]
    void Print()
    {
        Debug.Log(bigNumber.ToString());
    }
}
