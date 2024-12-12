using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;

public class Disaster : MonoBehaviour
{
    [Title("Input Data")]
    public SSO_DisasterData disasterData;

    [Title("Output Data")]
    public RSO_MapInfos mapInfos;
    public RSO_CameraPosition cameraPosition;
    public RSO_CurrentDisasterSize disasterSize;
    public RSO_CurrentDisasterStrength disasterStrength;
    public RSO_DisasterPosition disasterPosition;

    [Title("Output Events")]
    public RSE_DisasterAttack disasterAttack;

    [Title("RigidBody")]
    [SerializeField] private Rigidbody2D rb;

    [Title("SpriteRenderer")]
    [SerializeField] private SpriteRenderer sr;

    [Title("Arrow")]
    [SerializeField] private GameObject arrow;
    [SerializeField] private ArrowGenerator arrowScript;

    private bool isNeedToMove;
    private bool isNeedArrow;

    private void Start()
    {
        disasterPosition.Value = transform.position;

        transform.localScale = new Vector3(disasterData.Size / 5f, disasterData.Size / 5f, 1);
        transform.GetChild(0).localScale = new Vector3(5f / disasterData.Size, 5f / disasterData.Size, 1);

        disasterSize.Value = disasterData.Size;
        disasterStrength.Value = disasterData.Strength;

        mapInfos.PlayerSize = sr.bounds.size / 2;

        StartCoroutine(Damage());
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    private void FixedUpdate()
    {
        MoveToCamera();

        Arrow();
    }

    /// <summary>
    /// Manage the Arrow
    /// </summary>
    private void Arrow()
    {
        Vector2 targetPosition = cameraPosition.Value;

        float distance = Vector2.Distance(transform.position, targetPosition);

        if (distance > 1.5f)
        {
            isNeedArrow = true;

            arrowScript.stemLength = distance - 1.5f;

            if (!arrow.activeInHierarchy)
            {
                arrow.SetActive(true);
            }

            Vector2 direction = targetPosition - (Vector2)transform.position;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            transform.GetChild(0).transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }
        else if (isNeedArrow)
        {
            isNeedArrow = false;

            if (arrow.activeInHierarchy)
            {
                arrow.SetActive(false);
            }
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

            rb.linearVelocity = direction * disasterData.Velocity;

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
    /// Damage with Attack Speed
    /// </summary>
    /// <returns></returns>
    private IEnumerator Damage()
    {
        yield return new WaitForSeconds(disasterData.AttackSpeed);

        disasterAttack.FireEvent();

        StartCoroutine(Damage());
    }
}
