using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class FreePress : MonoBehaviour
{
    [Header("ScriptableObjects")]
    [SerializeField] private MapInfos mapInfos;

    [Header("Target")]
    [SerializeField] private Transform player;

    [Header("Zoom")]
    [SerializeField] private float speedCam;
    [SerializeField] private float zoomMax;
    [SerializeField] private float zoomMin;
    [SerializeField] private float speedZoomCam;
    [SerializeField] private float smoothTime;
    [SerializeField] private float zoomChangeMin;

    [Header("Camera Circle")]
    [SerializeField] private Transform circleCamera;

    [Header("Cinemachine")]
    [SerializeField] private CinemachineCamera cinemachineCamera;
    [SerializeField] private CinemachineConfiner2D cinemachineConfiner;

    private Camera mainCamera;

    private Vector3 velocity;
    private Vector3 touchPress;
    private Vector3 freePressTouch;
    private Vector3 freePressTouchPos;
    private Vector3 freePressDist;
    private Vector3 touch1Press;
    private Vector3 touch2Press;

    private Vector2 boundsSize;

    private bool isPressed;
    private bool isDecelerating;
    private bool isTouchPress1;
    private bool isZooming;

    private Coroutine moveCoroutine;
    private Coroutine deceleratingCoroutine;
    private Coroutine zoomCoroutine;


    public RSE_PointerDown test;
    public RSO_PointerPosition test2;

    private void Awake()
    {
        test.Fire += temp;
        test2.onValueChanged += temp2;
    }

    private void temp()
    {
        Debug.Log("Touch");
    }

    private void temp2(Vector2 temp)
    {
        Debug.Log("Move to " + temp);
    }

    private void OnValidate()
    {
        if (zoomMax < 10)
        {
            zoomMax = 10;
        }

        if(speedZoomCam < 0)
        {
            speedZoomCam = 0;
        }

        if (smoothTime < 0)
        {
            smoothTime = 0;
        }

        if (zoomChangeMin < 0)
        {
            zoomChangeMin = 0;
        }
    }

    private void Start()
    {
        mainCamera = Camera.main;

        cinemachineConfiner.BoundingShape2D = mapInfos.Collider;

        Collider2D confinerCollider = cinemachineConfiner.BoundingShape2D;

        if (confinerCollider != null)
        {
            boundsSize.x = confinerCollider.GetComponent<SpriteRenderer>().bounds.size.x;
            boundsSize.y = confinerCollider.GetComponent<SpriteRenderer>().bounds.size.y;
        }
    }

    /// <summary>
    /// Lock the Camera inside the Border
    /// </summary>
    /// <param name="newPosition"></param>
    /// <returns></returns>
    private Vector3 LockToCameraBorder(Vector3 newPosition)
    {
        Collider2D confinerCollider = cinemachineConfiner.BoundingShape2D;

        if (confinerCollider != null && player != null)
        {
            Vector2 confinerBounds = boundsSize;
            Vector2 confinerCenter = confinerCollider.bounds.center;

            Vector2 val = player.transform.GetChild(0).GetComponent<SpriteRenderer>().bounds.size / 2;

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

        freePressTouchPos = mainCamera.ScreenToWorldPoint(touchPress);
        freePressTouchPos.z = -10;

        if (!IsPointerOverUIObject())
        {
            while (!isZooming && (isPressed))
            {
                Vector3 currentTouchPos = mainCamera.ScreenToWorldPoint(touchPress);
                currentTouchPos.z = -10;

                freePressDist = freePressTouchPos - currentTouchPos;
                freePressDist.z = -10;

                Vector3 targetPosition = transform.position + freePressDist;

                targetPosition = LockToCameraBorder(targetPosition);

                targetPosition.z = -10;

                transform.position = Vector3.Lerp(transform.position, targetPosition, speedCam * Time.deltaTime);

                mapInfos.CameraTransform = transform.position;

                yield return null;
            }
        }
    }

    /// <summary>
    /// Deceleration of the Camera on UnPress
    /// </summary>
    /// <returns></returns>
    private IEnumerator Deceleration()
    {
        while (isDecelerating && !isZooming)
        {
            Vector3 currentTouchPos = mainCamera.ScreenToWorldPoint(freePressTouch);
            currentTouchPos.z = -10;

            freePressDist = freePressTouchPos - currentTouchPos;
            freePressDist.z = -10;

            Vector3 targetPosition = transform.position + freePressDist;

            targetPosition = LockToCameraBorder(targetPosition);
            targetPosition.z = -10;

            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

            mapInfos.CameraTransform = transform.position;

            if (velocity.magnitude < 0.1f)
            {
                isDecelerating = false;

                velocity = Vector3.zero;
                freePressTouchPos = Vector3.zero;
                freePressDist = Vector3.zero;
            }

            yield return null;
        }
    }

    /// <summary>
    /// Verify if UI on Top
    /// </summary>
    /// <returns></returns>
    private bool IsPointerOverUIObject()
    {
        /*PointerEventData pointerData = new PointerEventData(EventSystem.current)
        {
            position = freePressTouch
        };

        List<RaycastResult> raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, raycastResults);

        return raycastResults.Exists(result => result.gameObject.CompareTag("UI"));*/
        return false;
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
    /// Touch Down
    /// </summary>
    public void TouchDown(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && !isZooming)
        {
            isPressed = true;
            isDecelerating = false;

            if (moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine);
                moveCoroutine = null;
            }

            if (deceleratingCoroutine != null)
            {
                StopCoroutine(deceleratingCoroutine);
                deceleratingCoroutine = null;
            }

            velocity = Vector3.zero;
            freePressTouchPos = Vector3.zero;
            freePressDist = Vector3.zero;

            freePressTouch = touchPress;
            freePressTouch.z = -10;

            if (moveCoroutine == null)
            {
                moveCoroutine = StartCoroutine(CameraMove());
            }
        }
        else if (ctx.canceled)
        {
            isPressed = false;

            isDecelerating = true;

            if (deceleratingCoroutine != null)
            {
                StopCoroutine(deceleratingCoroutine);
                deceleratingCoroutine = null;
            }

            freePressTouch = touchPress;

            if (deceleratingCoroutine == null)
            {
                deceleratingCoroutine = StartCoroutine(Deceleration());
            }
        }
    }

    /// <summary>
    /// Zoom and DeZoom
    /// </summary>
    /// <param name="increment"></param>
    private void Zoom(float increment)
    {
        cinemachineCamera.Lens.OrthographicSize = Mathf.Clamp(cinemachineCamera.Lens.OrthographicSize - increment, zoomMax, zoomMin);

        float newScale = cinemachineCamera.Lens.OrthographicSize / 50f;

        circleCamera.localScale = new Vector3(newScale, newScale, 1);

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
            float increment = ctx.ReadValue<float>() * speedZoomCam;
            float newZoom = cinemachineCamera.Lens.OrthographicSize - increment;

            isZooming = true;

            if (newZoom > zoomMin)
            {
                cinemachineCamera.Lens.OrthographicSize = zoomMin;

                return;
            }
            else if (newZoom < zoomMax)
            {
                cinemachineCamera.Lens.OrthographicSize = zoomMax;

                return;
            }

            Zoom(increment);
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
