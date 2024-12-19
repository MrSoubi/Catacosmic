using System;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "RSE_CallInvoke1", menuName = "Catacosmic/UI/RSE/CallInvoke1")]
public class RSE_CallInvoke1 : ScriptableObject
{
    public Action Fire;

    [Button]
    private void FireEvent()
    {
        Fire?.Invoke();
    }
}
