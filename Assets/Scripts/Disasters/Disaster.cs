using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;

public class Disaster : MonoBehaviour
{
    [Title("ScriptableObjects")]
    [SerializeField] private RSO_MapInfos mapInfos;
    [SerializeField] private RSO_CameraPosition cameraPosition;
    [SerializeField] private RSO_DisasterPosition disasterPosition;
    [SerializeField] private SSO_DisasterStats disasterStats;

    [Title("RigidBody")]
    [SerializeField] private Rigidbody2D rb;

    [Title("SpriteRenderer")]
    [SerializeField] private SpriteRenderer sr;

    [Title("Arrow")]
    [SerializeField] private GameObject arrow;
    [SerializeField] private ArrowGenerator arrowScript;

    private bool isNeedToMove;

    private void Start()
    {
        transform.localScale = new Vector3(disasterStats.Radius, disasterStats.Radius, disasterStats.Radius);
        mapInfos.PlayerSize = sr.bounds.size / 2;

        StartCoroutine(Damage());
    }

    private void Update()
    {
        Arrow();
    }

    private void FixedUpdate()
    {
        MoveToCamera();
    }

    /// <summary>
    /// Manage the Arrow
    /// </summary>
    private void Arrow()
    {
        Vector2 targetPosition = cameraPosition.Value;
        float distance = Vector2.Distance(transform.position, targetPosition);
        arrowScript.stemLength = distance - 1.5f;

        if (distance <= 1.5)
        {
            arrow.SetActive(false);
        }
        else
        {
            arrow.SetActive(true);

            Vector2 direction = targetPosition - (Vector2)transform.GetChild(0).transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.GetChild(0).transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }
    }

    /// <summary>
    /// Move the Player to the Circle of the Camera
    /// </summary>
    private void MoveToCamera()
    {
        Vector2 targetPosition = cameraPosition.Value;
        float distance = Vector2.Distance(rb.position, targetPosition);

        if (distance > 0.05f)
        {
            isNeedToMove = true;

            Vector2 direction = (targetPosition - rb.position).normalized;

            rb.linearVelocity = direction * disasterStats.Speed;

            disasterPosition.Value = transform.position;
        }
        else if (isNeedToMove)
        {
            isNeedToMove = false;

            rb.linearVelocity = Vector2.zero;
            transform.position = targetPosition;

            disasterPosition.Value = transform.position;
        }
    }

    /// <summary>
    /// Damage Action
    /// </summary>
    /// <returns></returns>
    private IEnumerator Damage()
    {
        yield return new WaitForSeconds(disasterStats.AttackDelay);

        StartCoroutine(Damage());
    }
}
