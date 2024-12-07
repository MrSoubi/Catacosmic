using Sirenix.OdinInspector;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Title("ScriptableObjects")]
    [SerializeField] private RSO_MapInfos mapInfos;

    [Title("Default Disaster")]
    [SerializeField] private GameObject prefab;

    [Title("RigidBody")]
    [SerializeField] private Rigidbody2D rb;

    private bool isNeedToMove;
    private GameObject currentDisaster;
    private Disaster disasterScript;

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
        disasterScript = currentDisaster.GetComponent<Disaster>();
        currentDisaster.transform.localScale = new Vector3(disasterScript.DisasterStats.Radius, disasterScript.DisasterStats.Radius, disasterScript.DisasterStats.Radius);
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

            rb.linearVelocity = direction * disasterScript.DisasterStats.Speed;

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
