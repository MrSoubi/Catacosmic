using System;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "RSE_DisasterAttack", menuName = "Catacosmic/RSE/DisasterAttack")]
public class RSE_DisasterAttack : ScriptableObject
{
    public Action Fire;

    [Button]
    public void FireEvent()
    {
        Fire?.Invoke();
    }
}