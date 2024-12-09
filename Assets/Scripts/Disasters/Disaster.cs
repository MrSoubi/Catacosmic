using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;

public class Disaster : MonoBehaviour
{
    [Title("ScriptableObjects")]
    [SerializeField] private RSO_MapInfos mapInfos;
    [SerializeField] private RSO_CameraPosition cameraPosition;
    [SerializeField] private RSO_DisasterPosition disasterPosition;
    [SerializeField] private RSO_DisasterStats disasterStats;

    [Title("RigidBody")]
    [SerializeField] private Rigidbody2D rb;

    [Title("SpriteRenderer")]
    [SerializeField] private SpriteRenderer sr;

    private bool isNeedToMove;

    private void Start()
    {
        transform.localScale = new Vector3(disasterStats.Radius, disasterStats.Radius, disasterStats.Radius);
        mapInfos.PlayerSize = sr.bounds.size / 2;

        StartCoroutine(Damage());
    }

    private void FixedUpdate()
    {
        MoveToCamera();
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
        yield return new WaitForSeconds(disasterStats.TimeAction);

        StartCoroutine(Damage());
    }
}
