using System;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "RSE_CallEquipmentTab", menuName = "Catacosmic/UI/RSE/CallEquipmentTab")]
public class RSE_CallEquipmentTab : ScriptableObject
{
    public Action Fire;

    [Button]
    private void FireEvent()
    {
        Fire?.Invoke();
    }
}
