using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Targets")]
    public Transform cameraTarget;

    [Header("Default Disaster")]
    public GameObject prefab;

    [Header("Speed")]
    public float speed;

    [Header("RigidBody")]
    public Rigidbody2D rb;

    private bool isNeedToMove;
    private GameObject currentDisaster;

    private void Start()
    {
        SpawnDisater(prefab);
    }

    private void FixedUpdate()
    {
        MoveToCamera();
    }

    /// <summary>
    /// Destroy Disaster & Spawn the new Disaster
    /// </summary>
    /// <param name="disaster"></param>
    private void SpawnDisater(GameObject disaster)
    {
        if(currentDisaster != null)
        {
            Destroy(currentDisaster);
        }

        currentDisaster = Instantiate(disaster, transform.position, Quaternion.identity, transform);
    }

    /// <summary>
    /// Move the Player to the Circle of the Camera
    /// </summary>
    private void MoveToCamera()
    {
        if (cameraTarget == null) return;

        Vector2 targetPosition = cameraTarget.position;
        float distance = Vector2.Distance(rb.position, targetPosition);

        if (distance > 0.05f)
        {
            isNeedToMove = true;

            Vector2 direction = (targetPosition - rb.position).normalized;

            rb.linearVelocity = direction * speed;
        }
        else if (isNeedToMove)
        {
            isNeedToMove = false;

            rb.linearVelocity = Vector2.zero;
            transform.position = targetPosition;
        }
    }
}
