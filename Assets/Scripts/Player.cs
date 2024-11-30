using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform cam;

    public float speed;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (cam != null)
        {
            Vector2 currentPosition = rb.position;
            Vector2 targetPosition = new Vector2(cam.position.x, cam.position.y);
            Vector2 direction = (targetPosition - currentPosition).normalized;

            if (Vector2.Distance(currentPosition, targetPosition) > 0.1f)
            {
                rb.linearVelocity = direction * speed;
            }
            else
            {
                rb.linearVelocity = Vector2.zero;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawSphere(transform.position, 1f);
    }
}
