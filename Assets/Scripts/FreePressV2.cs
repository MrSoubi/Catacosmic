using UnityEngine;

public class FreePressV2 : MonoBehaviour
{
    public RSE_PointerDown test;
    public RSO_PointerPosition test2;

    private Vector2 touchPress;
    private Vector2 touchPressCurrent;

    private void Awake()
    {
        test.Fire += TouchPress;
        test2.onValueChanged += TouchMove;
    }

    private void TouchPress()
    {
        touchPressCurrent = touchPress;
    }

    private void TouchMove(Vector2 temp)
    {
        Vector2 delta = temp - touchPressCurrent;
        touchPressCurrent = temp; // Update to the current position to avoid large jumps

        Vector3 targetPosition = transform.position + (Vector3)delta;
        targetPosition.z = -10; // If you

        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 10); // Adjust the multiplier for smoother movement
    }
}
