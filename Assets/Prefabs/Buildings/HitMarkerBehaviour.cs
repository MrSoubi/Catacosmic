using UnityEngine;
using DG.Tweening;
using TMPro;

public class HitMarkerBehaviour : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    public void Initialize(float reward, Color color)
    {
        text.text = Mathf.Ceil(reward).ToString();
        text.color = color;
        transform.DOMove(transform.position + Vector3.up, 1).OnComplete(() => Destroy(gameObject));
    }
}
