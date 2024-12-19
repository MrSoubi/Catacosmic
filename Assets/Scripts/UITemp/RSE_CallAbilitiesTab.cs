using System;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "RSE_AbilitiesTab", menuName = "Catacosmic/UI/RSE/CallAbilitiesTab")]
public class RSE_CallAbilitiesTab : ScriptableObject
{
    public Action Fire;

    [Button]
    private void FireEvent()
    {
        Fire?.Invoke();
    }
}
