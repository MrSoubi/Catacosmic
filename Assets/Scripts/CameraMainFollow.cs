using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class CameraMainFollow : MonoBehaviour
{
    public Transform target;

    public float speed;

    public int zoomMax;
    public int zoomMin;
    public float zoomSpeed;

    private Vector3 origin;
    private Vector3 difference;

    private bool isDragging;

    /// <summary>
    /// Verify if UI on Top
    /// </summary>
    /// <returns></returns>
    private bool IsPointerOverUIObject()
    {
        PointerEventData pointerData = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };

        List<RaycastResult> raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, raycastResults);

        return raycastResults.Count > 0;
    }

    /// <summary>
    /// Zoom and DeZoom
    /// </summary>
    /// <param name="increment"></param>
    private void Zoom(float increment)
    {
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - increment, zoomMax, zoomMin);
    }

    private void Update()
    {
        if (target != null && target.childCount > 0 && (Input.touchCount == 1 || Input.GetMouseButton(0)))
        {
            transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
        }

        if (Input.touchCount == 2 && !IsPointerOverUIObject())
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

            float difference = currentMagnitude - prevMagnitude;

            Zoom(difference * zoomSpeed);
        }
    }

    /// <summary>
    /// Touch Press
    /// </summary>
    /// <param name="ctx"></param>
    public void OnDrag(InputAction.CallbackContext ctx)
    {
        if ((Input.touchCount == 1 || Input.GetMouseButton(0)) && !isDragging && ctx.started && !IsPointerOverUIObject())
        {
            origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            isDragging = ctx.started || ctx.performed;
        }
        else if (ctx.canceled)
        {
            isDragging = false;
        }
    }

    private void LateUpdate()
    {
        if(!isDragging || target.childCount > 0 || Input.touchCount > 1)
        {
            return;
        }

        difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        transform.position = origin - difference;

        target.position = new Vector3(transform.position.x, transform.position.y, target.position.z);
    }

    private void FixedUpdate()
    {
        if (!isDragging || target.childCount <= 0 || Input.touchCount > 1)
        {
            return;
        }

        Vector3 player = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        player.z = target.position.z;

        target.position = Vector3.MoveTowards(target.position, player, speed * Time.deltaTime);

        transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.x, player.y, transform.position.z), speed * Time.deltaTime);
    }
}
