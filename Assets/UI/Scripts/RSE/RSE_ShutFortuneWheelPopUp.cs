using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "RSE_ShutFortuneWheelPopUp", menuName = "Data/RSE/ShutFortuneWheelPopUp")]
public class RSE_ShutFortuneWheelPopUp : ScriptableObject
{
    public Action Fire;

    [Button]
    private void FireEvent()
    {
        Fire?.Invoke();
    }
}