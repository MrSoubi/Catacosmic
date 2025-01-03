using System;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "RSE_CallDeZoom", menuName = "Catacosmic/UI/RSE/CallDeZoom")]
public class RSE_CallDeZoom : ScriptableObject
{
    public Action Fire;

    [Button]
    private void FireEvent()
    {
        Fire?.Invoke();
    }
}
