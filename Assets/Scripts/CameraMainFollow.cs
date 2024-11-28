using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class CameraMainFollow : MonoBehaviour
{
    public Transform target;

    public float speed = 5f;

    private Vector3 lastMousePosition;

    private bool IsPointerOverUIObject()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }

    private void Update()
    {
        if (target != null && target.childCount > 0 && !IsPointerOverUIObject())
        {
            transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
        }
        else if (target != null && Input.GetMouseButton(0) && !IsPointerOverUIObject())
        {
            /*Vector3 mousePosition = Input.mousePosition;

            // Convert the screen position to world position (2D space)
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

            // Keep the camera's z-position the same (to stay in 2D space)
            worldPosition.z = transform.position.z;

            // Move the camera only if the mouse is pressed and there is movement
            if (lastMousePosition != Vector3.zero)
            {
                // Move the camera towards the mouse position
                Vector3 direction = worldPosition - lastMousePosition;
                transform.position += -direction * speed * Time.deltaTime;
            }

            // Update the last mouse position
            lastMousePosition = worldPosition;*/
        }
    }

    private Vector3 origin;
    private Vector3 difference;

    private bool isDragging;

    public void OnDrag(InputAction.CallbackContext ctx)
    {
        if(!IsPointerOverUIObject() && target.childCount <= 0)
        {
            if (ctx.started)
            {
                origin = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
                isDragging = ctx.started || ctx.performed;
            }
            else if (ctx.canceled)
            {
                isDragging = false;
            }
        }

    }

    private void LateUpdate()
    {
        if(!isDragging)
        {
            return;
        }

        difference = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()) - transform.position;
        transform.position = origin - difference;
    }
}
