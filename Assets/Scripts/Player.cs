using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("ScriptableObjects")]
    [SerializeField] private MapInfos mapInfos;

    [Header("Default Disaster")]
    [SerializeField] private GameObject prefab;

    [Header("Speed")]
    [SerializeField] private float speed;

    [Header("RigidBody")]
    [SerializeField] private Rigidbody2D rb;

    private bool isNeedToMove;
    private GameObject currentDisaster;

    private void OnValidate()
    {
        if(speed < 0)
        {
            speed = 0;
        }
    }

    private void Awake()
    {
        mapInfos.PlayerRef = gameObject;
    }

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
        Vector2 targetPosition = mapInfos.CameraTransform;
        float distance = Vector2.Distance(rb.position, targetPosition);

        if (distance > 0.05f)
        {
            isNeedToMove = true;

            Vector2 direction = (targetPosition - rb.position).normalized;

            rb.linearVelocity = direction * speed;

            mapInfos.PlayerTransform = transform.position;
        }
        else if (isNeedToMove)
        {
            isNeedToMove = false;

            rb.linearVelocity = Vector2.zero;
            transform.position = targetPosition;
        }
    }
}
