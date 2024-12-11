using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "RSE_DisasterAttack", menuName = "Data/RSE/DisasterAttack")]
public class RSE_DisasterAttack : ScriptableObject
{
    public Action Fire;

    [Button]
    public void FireEvent()
    {
        Fire?.Invoke();
    }
}