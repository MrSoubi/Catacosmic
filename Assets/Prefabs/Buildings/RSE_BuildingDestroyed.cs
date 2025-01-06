using System;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "RSE_BuildingDestroyed", menuName = "Catacosmic/RSE/BuildingDestroyed")]
public class RSE_BuildingDestroyed : ScriptableObject
{
    public Action Fire;

    [Button]
    private void FireEvent()
    {
        Fire?.Invoke();
    }
}