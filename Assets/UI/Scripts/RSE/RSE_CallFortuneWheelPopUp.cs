using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "RSE_CallFortuneWheelPopUp", menuName = "Data/RSE/CallFortuneWheelPopUp")]
public class RSE_CallFortuneWheelPopUp : ScriptableObject
{
    public Action Fire;

    [Button]
    private void FireEvent()
    {
        Fire?.Invoke();
    }
}