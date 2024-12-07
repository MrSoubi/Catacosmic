using TMPro;
using UnityEngine;

public class DisasterController : MonoBehaviour
{
    [Header("Input Events")]
    public RSE_PointerDown pointerDown;
    public RSE_PointerUp pointerUp;
    public RSO_PointerWorldPosition pointerPosition;

    [Header("Output Events")]
    public RSO_DisasterPosition disasterPosition;

    [Header("Settings")]
    [SerializeField] float movementSpeed = 1.0f;


    bool canMove = false;
    Vector2 target = Vector2.zero;

    private void OnEnable()
    {
        pointerDown.Fire += EnableMovement;
        pointerUp.Fire += DisableMovement;
    }

    private void OnDisable()
    {
        pointerDown.Fire -= EnableMovement;
        pointerUp.Fire -= DisableMovement;
    }

    void Start()
    {
        transform.position = Vector2.zero;
    }

    void EnableMovement()
    {
        pointerPosition.onValueChanged += SetTargetPosition;
        canMove = true;
    }

    void DisableMovement()
    {
        pointerPosition.onValueChanged -= SetTargetPosition;
        canMove = false;
    }

    void SetTargetPosition(Vector2 targetPosition)
    {
        target = targetPosition;
    }

    private void Update()
    {
        if (!canMove) return;

        Vector2 position = new Vector2(transform.position.x, transform.position.y);
        Vector2 direction = (target - position).normalized;
        Vector2 newPosition = position + direction * movementSpeed * Time.deltaTime;
        transform.position = newPosition;

        disasterPosition.Value = newPosition;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(pointerPosition.Value, 0.2f);
    }
}
