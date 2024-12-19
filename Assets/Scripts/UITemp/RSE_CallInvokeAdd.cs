using System;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "RSE_CallInvokeAdd", menuName = "Catacosmic/UI/RSE/CallInvokeAdd")]
public class RSE_CallInvokeAdd : ScriptableObject
{
    public Action Fire;

    [Button]
    private void FireEvent()
    {
        Fire?.Invoke();
    }
}
