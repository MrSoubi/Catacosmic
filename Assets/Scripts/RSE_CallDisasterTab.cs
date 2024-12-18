using System;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "RSE_CallDisasterTab", menuName = "Catacosmic/UI/RSE/CallDisasterTab")]
public class RSE_CallDisasterTab : ScriptableObject
{
    public Action Fire;

    [Button]
    private void FireEvent()
    {
        Fire?.Invoke();
    }
}
