using System;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "RSE_DisasterAttack", menuName = "Catacosmic/RSE/DisasterAttack")]
public class RSE_DisasterAttack : ScriptableObject
{
    public Action<float> Fire;

    [Button]
    public void FireEvent(float strength)
    {
        Fire?.Invoke(strength);
    }
}