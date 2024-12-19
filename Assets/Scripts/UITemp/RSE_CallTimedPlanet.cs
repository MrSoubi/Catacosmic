using System;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "RSE_CallTimedPlanet", menuName = "Catacosmic/UI/RSE/CallTimedPlanet")]
public class RSE_CallTimedPlanet : ScriptableObject
{
    public Action Fire;

    [Button]
    private void FireEvent()
    {
        Fire?.Invoke();
    }
}
