using UnityEngine;
using DG.Tweening;
using TMPro;

public class HitMarkerBehaviour : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    public void Initialize(double reward, Color color)
    {
        text.text = reward.ToString();
        text.color = color;
        transform.DOMove(transform.position + Vector3.up, 1).OnComplete(() => Destroy(gameObject));
    }
}
