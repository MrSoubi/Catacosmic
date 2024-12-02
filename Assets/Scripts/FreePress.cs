using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class FreePress : MonoBehaviour
{
    [Header("Targets")]
    public Transform player;

    [Header("Zoom")]
    public float zoomMax;
    public float zoomMin;
    public float speedZoomCam;
    public float zoomChangeMin;

    [Header("Cinemachine")]
    public CinemachineCamera cinemachineCamera;
    public CinemachineConfiner2D cinemachineConfiner;

    private Vector3 freePressVec;
    private Vector3 touchPress;
    private Vector3 freePressTouchPos;
    private Vector3 freePressDist;
    private Vector3 touch1Press;
    private Vector3 touch2Press;

    private bool isPressed;
    private bool isTouchPress1;
    private bool isZooming;

    private Coroutine moveCoroutine;
    private Coroutine zoomCoroutine;

    /// <summary>
    /// Lock the Camera inside the Border
    /// </summary>
    /// <param name="newPosition"></param>
    /// <returns></returns>
    private Vector3 LockToCameraBorder(Vector3 newPosition)
    {
        Collider2D confinerCollider = transform.GetComponent<CinemachineConfiner2D>().BoundingShape2D;

        if (confinerCollider != null && player != null)
        {
            Vector2 confinerBounds = confinerCollider.bounds.size;
            Vector2 confinerCenter = confinerCollider.bounds.center;

            Vector2 val = player.GetChild(0).GetComponent<SpriteRenderer>().bounds.size / 2;

            float minX = confinerCenter.x - confinerBounds.x / 2 + val.x;
            float maxX = confinerCenter.x + confinerBounds.x / 2 - val.x;
            float minY = confinerCenter.y - confinerBounds.y / 2 + val.y;
            float maxY = confinerCenter.y + confinerBounds.y / 2 - val.y;

            newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
            newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);

            return newPosition;
        }
        else
        {
            return newPosition;
        }
    }

    /// <summary>
    /// Move the Camera
    /// </summary>
    /// <returns></returns>
    private IEnumerator CameraMove()
    {
        yield return null;

        freePressTouchPos = Camera.main.ScreenToWorldPoint(touchPress);
        freePressTouchPos.z = -10;

        if (!IsPointerOverUIObject())
        {
            while (!isZooming && isPressed)
            {
                Vector3 currentTouchPos = Camera.main.ScreenToWorldPoint(touchPress);
                currentTouchPos.z = -10;

                freePressDist = freePressTouchPos - currentTouchPos;
                freePressDist.z = -10;

                Vector3 targetPosition = transform.position + freePressDist;

                targetPosition = LockToCameraBorder(targetPosition);

                Vector3 newPosition = Vector3.MoveTowards(transform.position, targetPosition, freePressDist.magnitude * Time.deltaTime * 5);

                transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);

                yield return null;
            }
        }
    }

    /// <summary>
    /// Verify if UI on Top
    /// </summary>
    /// <returns></returns>
    private bool IsPointerOverUIObject()
    {
        PointerEventData pointerData = new PointerEventData(EventSystem.current)
        {
            position = touchPress
        };

        List<RaycastResult> raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, raycastResults);

        return raycastResults.Exists(result => result.gameObject.CompareTag("UI"));
    }


    /// <summary>
    /// Touch Position
    /// </summary>
    /// <param name="ctx"></param>
    public void TouchPos(InputAction.CallbackContext ctx)
    {
        touchPress = ctx.ReadValue<Vector2>();
        touchPress.z = -10;
    }

    /// <summary>
    /// Reset when stop Touch
    /// </summary>
    private void ResetTouch()
    {
        if (moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
            moveCoroutine = null;
        }

        freePressTouchPos = Vector3.zero;
        freePressDist = Vector3.zero;
    }

    /// <summary>
    /// Touch Down
    /// </summary>
    public void TouchDown(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && !isZooming)
        {
            isPressed = true;

            if (moveCoroutine == null)
            {
                moveCoroutine = StartCoroutine(CameraMove());
            }
        }
        else if (ctx.canceled)
        {
            isPressed = false;

            ResetTouch();
        }
    }

    /// <summary>
    /// Zoom and DeZoom
    /// </summary>
    /// <param name="increment"></param>
    private void Zoom(float increment)
    {
        cinemachineCamera.Lens.OrthographicSize = Mathf.Clamp(transform.GetComponent<CinemachineCamera>().Lens.OrthographicSize - increment, zoomMax, zoomMin);

        cinemachineConfiner.InvalidateBoundingShapeCache();
        cinemachineConfiner.InvalidateLensCache();
    }

    /// <summary>
    /// Scroll with Mouse
    /// </summary>
    /// <param name="ctx"></param>
    public void ScrollZoom(InputAction.CallbackContext ctx)
    {
        if (ctx.started && !isZooming && !IsPointerOverUIObject())
        {
            isZooming = true;

            Zoom(ctx.ReadValue<float>() * speedZoomCam);
        }
        else if (ctx.canceled)
        {
            isZooming = false;
        }
    }

    /// <summary>
    /// Zoom & DeZoom with Touch Pinch
    /// </summary>
    /// <returns></returns>
    private IEnumerator ZoomDetection()
    {
        yield return null;

        float previousDistance = 0f;
        float distance = 0f;

        while (isZooming && isTouchPress1)
        {
            distance = Vector2.Distance(touch1Press, touch2Press);

            float distanceChange = Mathf.Abs(distance - previousDistance);

            if (distanceChange >= zoomChangeMin)
            {
                if (distance > previousDistance && previousDistance > 0)
                {
                    Zoom(1 * speedZoomCam);
                }
                else if (distance < previousDistance)
                {
                    Zoom(-1 * speedZoomCam);
                }
            }

            previousDistance = distance;

            yield return null;
        }
    }

    /// <summary>
    /// Touch Position 1
    /// </summary>
    /// <param name="ctx"></param>
    public void TouchPosition1(InputAction.CallbackContext ctx)
    {
        touch1Press = ctx.ReadValue<Vector2>();
        touch1Press.z = -10;
    }

    /// <summary>
    /// Touch Position 2
    /// </summary>
    /// <param name="ctx"></param>
    public void TouchPosition2(InputAction.CallbackContext ctx)
    {
        touch2Press = ctx.ReadValue<Vector2>();
        touch2Press.z = -10;
    }

    /// <summary>
    /// Touch Press 1
    /// </summary>
    /// <param name="ctx"></param>
    public void TouchPress1(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            isTouchPress1 = true;
        }
        else if (ctx.canceled)
        {
            isTouchPress1 = false;

            touch1Press = Vector3.zero;
        }
    }

    /// <summary>
    /// Touch Press 2 & Start the Zoom Mode
    /// </summary>
    /// <param name="ctx"></param>
    public void ZoomStart(InputAction.CallbackContext ctx)
    {
        if (ctx.started && !isZooming)
        {
            isZooming = true;

            zoomCoroutine = StartCoroutine(ZoomDetection());
        }
        else if (ctx.canceled)
        {
            isZooming = false;

            touch2Press = Vector3.zero;

            ZoomStop();
        }
    }

    /// <summary>
    /// Stop the Zoom Mode
    /// </summary>
    private void ZoomStop()
    {
        if (zoomCoroutine != null)
        {
            StopCoroutine(zoomCoroutine);
            zoomCoroutine = null;
        }
    }
}
