using System;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "RSE_CallInvoke10", menuName = "Catacosmic/UI/RSE/CallInvoke10")]
public class RSE_CallInvoke10 : ScriptableObject
{
    public Action Fire;

    [Button]
    private void FireEvent()
    {
        Fire?.Invoke();
    }
}
