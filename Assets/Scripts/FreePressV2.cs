using UnityEngine;

public class FreePressV2 : MonoBehaviour
{
    public RSE_PointerDown test;
    public RSO_PointerPosition test2;
    public RSE_PointerUp test3;

    public float moveSpeed;

    private Vector2 touchPress;
    private Vector2 touchPressCurrent;

    private bool isTouchPress;

    private void Awake()
    {
        test.Fire += TouchPress;
        test2.onValueChanged += TouchMove;
        test3.Fire += TouchUp;
    }

    private void TouchPress()
    {
        // Store the initial touch position when the user presses on the screen
        touchPress = test2.Value;  // Assuming test2 gives the current touch position
        isTouchPress = true;
    }

    private void TouchMove(Vector2 currentPosition)
    {
        if (!isTouchPress) return;

        // Calculate the movement direction based on touch movement
        touchPressCurrent = currentPosition;

        // Calculate the difference in position
        Vector2 delta = touchPressCurrent - touchPress;

        // Move the camera in the opposite direction of the touch movement
        // Adjust the camera's position based on the delta
        Camera.main.transform.position -= new Vector3(delta.x, 0, delta.y) * moveSpeed * Time.deltaTime;

        // Update the touchPress to the current position for smooth continuous movement
        touchPress = currentPosition;
    }

    private void TouchUp()
    {
        // Reset the touch press state when the touch ends
        isTouchPress = false;
    }
}
