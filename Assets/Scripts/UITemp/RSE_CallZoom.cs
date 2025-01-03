using System;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "RSE_CallZoom", menuName = "Catacosmic/UI/RSE/CallZoom")]
public class RSE_CallZoom : ScriptableObject
{
    public Action Fire;

    [Button]
    private void FireEvent()
    {
        Fire?.Invoke();
    }
}
