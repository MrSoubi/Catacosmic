using System;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "RSE_CallInventory", menuName = "Catacosmic/UI/RSE/CallInventory")]
public class RSE_CallInventory : ScriptableObject
{
    public Action Fire;

    [Button]
    private void FireEvent()
    {
        Fire?.Invoke();
    }
}
