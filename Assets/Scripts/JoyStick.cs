using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class JoyStick : MonoBehaviour
{
    [Header("Targets")]
    public Transform player;

    [Header("Zoom")]
    public float speedCam;
    public float zoomMax;
    public float zoomMin;
    public float speedZoomCam;
    public float zoomChangeMin;

    [Header("JoyStickUI")]
    public GameObject joystick;
    public GameObject joystickBG;
    public RectTransform joystickRectTransform;
    public RectTransform joystickBGRectTransform;

    [Header("Cinemachine")]
    public CinemachineCamera cinemachineCamera;
    public CinemachineConfiner2D cinemachineConfiner;

    private Vector3 joystickVec;
    private Vector3 joystickOriginalPos;
    private Vector3 touchPress;
    private Vector3 joystickTouchPos;
    private Vector3 joystickDist;
    private Vector3 touch1Press;
    private Vector3 touch2Press;

    private float joystickRadius;

    private bool isPressed;
    private bool isJoyStickMove;
    private bool isTouchPress1;
    private bool isZooming;

    private Coroutine moveCoroutine;
    private Coroutine zoomCoroutine;

    private void Start()
    {
        Initialisation();
    }

    /// <summary>
    /// Initialise the JoyStick Values
    /// </summary>
    private void Initialisation()
    {
        if (joystickBG == null) return;

        joystickRadius = joystickBGRectTransform.sizeDelta.y / 4;

        joystickOriginalPos = joystickBGRectTransform.anchoredPosition;

        joystickTouchPos = joystickOriginalPos;
    }

    /// <summary>
    /// Lock the Camera for Always see the Player
    /// </summary>
    /// <returns></returns>
    private bool isInsideCameraBorder()
    {
        Vector3 playerPos = player.position;
        SpriteRenderer playerSprite = player.GetComponentInChildren<SpriteRenderer>();
        Vector3 playerSize = playerSprite.bounds.size;

        Vector3 adjustedMin = playerPos - playerSize / 2 + new Vector3(0.01f, 0.01f, 0);
        Vector3 adjustedMax = playerPos + playerSize / 2 - new Vector3(0.01f, 0.01f, 0);

        Vector3 viewportMin = Camera.main.WorldToViewportPoint(adjustedMin);
        Vector3 viewportMax = Camera.main.WorldToViewportPoint(adjustedMax);

        return viewportMin.x >= 0 && viewportMax.x <= 1 && viewportMin.y >= 0 && viewportMax.y <= 1;
    }

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
        while (isJoyStickMove)
        {
            if (isInsideCameraBorder())
            {
                if(Mathf.Abs(joystickVec.x) > 0.1f || Mathf.Abs(joystickVec.y) > 0.1f)
                {
                    Vector3 targetPosition = transform.position + new Vector3(joystickVec.x, joystickVec.y, 0);

                    Vector3 newPosition = Vector3.MoveTowards(transform.position, targetPosition, speedCam * Time.deltaTime);

                    transform.position = LockToCameraBorder(newPosition);
                }
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
        PointerEventData pointerData = new PointerEventData(EventSystem.current)
        {
            position = touchPress
        };

        List<RaycastResult> raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, raycastResults);

        return raycastResults.Exists(result => result.gameObject.CompareTag("UI"));
    }

    /// <summary>
    /// Move the JoyStick with a Frame Delay
    /// </summary>
    /// <returns></returns>
    private IEnumerator MoveJoyStick()
    {
        yield return null;

        if (!IsPointerOverUIObject())
        {
            joystickTouchPos = Camera.main.ScreenToWorldPoint(touchPress);
            joystickTouchPos.z = 0;

            if (joystickBG != null)
            {
                joystickBG.transform.position = joystickTouchPos;
            }

            isJoyStickMove = true;
        }
    }

    /// <summary>
    /// Reset the JoyStick Position
    /// </summary>
    private void ResetJoyStickPos()
    {
        joystickTouchPos = Vector3.zero;
        joystickVec = Vector3.zero;
        joystickDist = Vector3.zero;

        isJoyStickMove = false;

        if (joystickBG != null)
        {
            joystickRectTransform.anchoredPosition = Vector2.zero;
            joystickBGRectTransform.anchoredPosition = joystickOriginalPos;
        }

        if (moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
            moveCoroutine = null;
        }
    }

    /// <summary>
    /// Touch Position
    /// </summary>
    /// <param name="ctx"></param>
    public void TouchPos(InputAction.CallbackContext ctx)
    {
        touchPress = ctx.ReadValue<Vector2>();
        touchPress.z = -10;

        if (ctx.performed && isJoyStickMove && !isZooming)
        {
            Vector3 backgroundJoyStick = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 0));

            if (joystickBG != null)
            {
                backgroundJoyStick = joystickBG.transform.position;
            }

            joystickTouchPos = Camera.main.ScreenToWorldPoint(new Vector3(touchPress.x, touchPress.y, 0)) - backgroundJoyStick;

            joystickVec = Vector2.ClampMagnitude(joystickTouchPos, 1);
            joystickVec.z = -10;

            joystickDist = Vector2.ClampMagnitude(joystickTouchPos * joystickRadius, joystickRadius);

            if (joystickBG != null)
            {
                joystickRectTransform.anchoredPosition = joystickDist;
            }

            if(moveCoroutine == null)
            {
                moveCoroutine = StartCoroutine(CameraMove());
            }
        }
        else if(ctx.performed && isJoyStickMove && isZooming)
        {
            ResetJoyStickPos();
        }
    }

    /// <summary>
    /// Touch Down
    /// </summary>
    public void TouchDown(InputAction.CallbackContext ctx)
    {
        if (ctx.started && !isPressed)
        {
            isPressed = true;

            StartCoroutine(MoveJoyStick());
        }
        else if (ctx.canceled)
        {
            isPressed = false;

            ResetJoyStickPos();
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

            ResetJoyStickPos();

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

        ResetJoyStickPos();

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
        if(ctx.started && !isZooming)
        {
            isZooming = true;

            zoomCoroutine = StartCoroutine(ZoomDetection());
        }
        else if(ctx.canceled)
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
        if(zoomCoroutine != null)
        {
            StopCoroutine(zoomCoroutine);
            zoomCoroutine = null;
        }
    }
}
