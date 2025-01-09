using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;

public class Disaster : MonoBehaviour
{
    [Title("Input Data")]
    [SerializeField] private SSO_DisasterData disasterData;

    [Title("Output Data")]
    [SerializeField] private RSO_CurrentPlayerSize currentPlayerSize;
    [SerializeField] private RSO_CameraPosition cameraPosition;
    [SerializeField] private RSO_DisasterPosition disasterPosition;
    [SerializeField] private RSO_CurrentDisasterName currentDisasterName;
    [SerializeField] private RSO_CurrentDisasterSprite currentDisasterSprite;
    [SerializeField] private RSO_CurrentDisasterSize currentDisasterSize;
    [SerializeField] private RSO_CurrentDisasterVelocity currentDisasterVelocity;
    [SerializeField] private RSO_CurrentDisasterStrength currentDisasterStrength;
    [SerializeField] private RSO_CurrentDisasterCriticChance currentDisasterCriticChance;
    [SerializeField] private RSO_CurrentDisasterCriticMultiplier currentDisasterCriticMultiplier;
    [SerializeField] private RSO_CurrentDisasterAttackSpeed currentDisasterAttackSpeed;

    [Title("Output Events")]
    [SerializeField] private RSE_DisasterAttack disasterAttack;

    [Title("RigidBody")]
    [SerializeField] private Rigidbody2D rb;

    [Title("SpriteRenderer")]
    [SerializeField] private SpriteRenderer sr;

    [Title("Arrow")]
    [SerializeField] private GameObject arrow;
    [SerializeField] private ArrowGenerator arrowGenerator;

    private bool isNeedToMove;
    private bool isNeedArrow;

    private void Awake()
    {
        currentDisasterSize.onValueChanged += ChangeSize;
    }

    private void OnDisable()
    {
        currentDisasterSize.onValueChanged -= ChangeSize;
    }

    private void Start()
    {
        disasterPosition.Value = transform.position;
        currentPlayerSize.Value = sr.bounds.size / 2;

        transform.localScale = new Vector3(disasterData.Size, disasterData.Size, 1);
        transform.GetChild(0).localScale = new Vector3(disasterData.Size, disasterData.Size, 1);

        currentDisasterName.Value = disasterData.Name;
        currentDisasterSprite.Value = disasterData.Sprite;
        currentDisasterSize.Value = disasterData.Size;
        currentDisasterVelocity.Value = disasterData.Velocity;
        currentDisasterStrength.Value = disasterData.Strength;
        currentDisasterCriticChance.Value = disasterData.CriticChance;
        currentDisasterCriticMultiplier.Value = disasterData.CriticMultiplier;
        currentDisasterAttackSpeed.Value = disasterData.AttackSpeed;

        arrowGenerator.stemLength = 0;
        arrowGenerator.stemWidth = sr.size.x / 5f;
        arrowGenerator.tipLength = sr.size.x / 2f;
        arrowGenerator.tipWidth = sr.size.x / 2f;

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

        if (distance > sr.bounds.size.x)
        {
            isNeedArrow = true;

            arrowGenerator.stemLength = distance - sr.bounds.size.x;

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

        disasterAttack.FireEvent(Mathf.Pow(1.8f, currentDisasterStrength.Value));

        StartCoroutine(Damage());
    }

    /// <summary>
    /// Change the Size of the Disaster
    /// </summary>
    private void ChangeSize(int size)
    {
        transform.localScale = new Vector3(size, size, 1);
        transform.GetChild(0).localScale = new Vector3(size, size, 1);

        arrowGenerator.stemWidth = sr.size.x / 5f;
        arrowGenerator.tipLength = sr.size.x / 2f;
        arrowGenerator.tipWidth = sr.size.x / 2f;

        currentPlayerSize.Value = sr.bounds.size / 2;
    }
}
