using Sirenix.OdinInspector;
using UnityEngine;

public class Disaster : MonoBehaviour
{
    [Title("ScriptableObjects")]
    [SerializeField] private RSO_MapInfos mapInfos;
    [SerializeField] private RSO_DisasterPosition disasterPosition;
    [SerializeField] private RSO_DisasterStats disasterStats;

    [Title("RigidBody")]
    [SerializeField] private Rigidbody2D rb;

    [Title("SpriteRenderer")]
    [SerializeField] private SpriteRenderer sr;

    private bool isNeedToMove;

    private void Start()
    {
        mapInfos.PlayerSize = sr.bounds.size / 2;
        transform.localScale = new Vector3(disasterStats.Radius, disasterStats.Radius, disasterStats.Radius);
    }

    private void Update()
    {
        disasterPosition.Value = transform.position;
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
        Vector2 targetPosition = mapInfos.CameraTransform;
        float distance = Vector2.Distance(rb.position, targetPosition);

        if (distance > 0.05f)
        {
            isNeedToMove = true;

            Vector2 direction = (targetPosition - rb.position).normalized;

            rb.linearVelocity = direction * disasterStats.Speed;

            mapInfos.PlayerTransform = transform.position;
        }
        else if (isNeedToMove)
        {
            isNeedToMove = false;

            rb.linearVelocity = Vector2.zero;
            transform.position = targetPosition;

            mapInfos.PlayerTransform = transform.position;
        }
    }
}
