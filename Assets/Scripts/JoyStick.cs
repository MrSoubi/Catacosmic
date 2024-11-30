using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class JoyStick : MonoBehaviour
{
    public Transform cam;
    public Transform player;

    [Header("Zoom")]
    public float speedCam;
    public int zoomMax;
    public int zoomMin;
    public float speedZoomCam;
    public float zoomChangeMin;

    [Header("JoyStickUI")]
    public GameObject joystick;
    public GameObject joystickBG;

    private Vector2 joystickVec;
    private Vector3 joystickOriginalPos;
    private Vector3 touchPress;
    private Vector3 joystickTouchPos;
    private Vector2 joystickDist;
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
        if(joystickBG != null)
        {
            joystickRadius = joystickBG.GetComponent<RectTransform>().sizeDelta.y / 4;

            joystickOriginalPos = joystickBG.GetComponent<RectTransform>().anchoredPosition;

            joystickTouchPos = joystickOriginalPos;
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
            Vector3 targetPosition = cam.position + new Vector3(joystickVec.x, joystickVec.y, 0);

            Vector3 playerPos = player.position;
            SpriteRenderer playerSprite = player.GetChild(0).GetComponent<SpriteRenderer>();
            Vector3 playerSize = playerSprite.bounds.size;

            float playerLeft = playerPos.x - playerSize.x / 2;
            float playerRight = playerPos.x + playerSize.x / 2;
            float playerBottom = playerPos.y - playerSize.y / 2;
            float playerTop = playerPos.y + playerSize.y / 2;

            Vector3 viewportLeft = Camera.main.WorldToViewportPoint(new Vector3(playerLeft, playerPos.y, playerPos.z));
            Vector3 viewportRight = Camera.main.WorldToViewportPoint(new Vector3(playerRight, playerPos.y, playerPos.z));
            Vector3 viewportBottom = Camera.main.WorldToViewportPoint(new Vector3(playerPos.x, playerBottom, playerPos.z));
            Vector3 viewportTop = Camera.main.WorldToViewportPoint(new Vector3(playerPos.x, playerTop, playerPos.z));

            bool isOnScreen = viewportLeft.x >= 0f && viewportRight.x <= 1f && viewportBottom.y >= 0f && viewportTop.y <= 1f;

            if (isOnScreen)
            {
                // BOUND LIMIT
                Vector3 newPosition = Vector3.Lerp(cam.position, targetPosition, speedCam * Time.deltaTime);

                Collider2D confinerCollider = cam.GetComponent<CinemachineConfiner2D>().BoundingShape2D;

                Vector2 confinerBounds = confinerCollider.bounds.size;
                Vector2 confinerCenter = confinerCollider.bounds.center;

                Vector2 val = player.GetChild(0).GetComponent<SpriteRenderer>().bounds.size / 2;

                float minX = confinerCenter.x - confinerBounds.x / 2 + val.x;
                float maxX = confinerCenter.x + confinerBounds.x / 2 - val.x;
                float minY = confinerCenter.y - confinerBounds.y / 2 + val.y;
                float maxY = confinerCenter.y + confinerBounds.y / 2 - val.y;

                newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
                newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);

                cam.position = newPosition;
            }

            yield return null;
        }
    }

    /// <summary>
    /// Touch Position
    /// </summary>
    /// <param name="ctx"></param>
    public void TouchPos(InputAction.CallbackContext ctx)
    {
        touchPress = ctx.ReadValue<Vector2>();

        if (ctx.performed && isJoyStickMove && !isZooming)
        {
            Vector3 backgroundJoyStick = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 0));

            if (joystickBG != null)
            {
                backgroundJoyStick = joystickBG.transform.position;
            }

            joystickTouchPos = Camera.main.ScreenToWorldPoint(new Vector3(touchPress.x, touchPress.y, 0)) - backgroundJoyStick;

            joystickVec = Vector2.ClampMagnitude(joystickTouchPos, 1);
            joystickDist = Vector2.ClampMagnitude(joystickTouchPos * joystickRadius, joystickRadius);

            if (joystickBG != null)
            {
                joystick.GetComponent<RectTransform>().anchoredPosition = joystickDist;
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

        bool overUI = false;

        foreach (RaycastResult result in raycastResults)
        {
            if(result.gameObject.CompareTag("UI"))
            {
                overUI = true;
            }     
        }

        return overUI;
    }

    /// <summary>
    /// Move the JoyStick with a Frame Delay
    /// </summary>
    /// <returns></returns>
    private IEnumerator MoveJoyStick()
    {
        yield return null;

        if(!IsPointerOverUIObject())
        {
            joystickTouchPos = Camera.main.ScreenToWorldPoint(touchPress);
            joystickTouchPos.z = 0;

            if(joystickBG != null)
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
        joystickTouchPos = Vector2.zero;
        joystickVec = Vector2.zero;
        joystickDist = Vector2.zero;

        if(joystickBG != null)
        {
            joystick.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            joystickBG.GetComponent<RectTransform>().anchoredPosition = joystickOriginalPos;
        }

        isJoyStickMove = false;
        
        if(moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
            moveCoroutine = null;
        } 
    }

    /// <summary>
    /// Touch Down
    /// </summary>
    public void TouchDown(InputAction.CallbackContext ctx)
    {
        if (ctx.started && !isPressed)
        {
            isPressed = ctx.started || ctx.performed;

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
        cam.GetComponent<CinemachineCamera>().Lens.OrthographicSize = Mathf.Clamp(cam.GetComponent<CinemachineCamera>().Lens.OrthographicSize - increment, zoomMax, zoomMin);
    }

    /// <summary>
    /// Scroll with Mouse
    /// </summary>
    /// <param name="ctx"></param>
    public void ScrollZoom(InputAction.CallbackContext ctx)
    {
        if (!isZooming && !IsPointerOverUIObject())
        {
            isZooming = ctx.started || ctx.performed;

            float scrollValue = ctx.ReadValue<float>();

            Zoom(scrollValue * speedZoomCam);
        }
        else if (ctx.canceled)
        {
            isZooming = false;
        }
    }

    public void TouchPosition1(InputAction.CallbackContext ctx)
    {
        touch1Press = ctx.ReadValue<Vector2>();
    }

    public void TouchPosition2(InputAction.CallbackContext ctx)
    {
        touch2Press = ctx.ReadValue<Vector2>();
    }

    public void TouchPress1(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            isTouchPress1 = ctx.started || ctx.performed;
        }
        else if (ctx.canceled)
        {
            isTouchPress1 = false;
        }
    }

    public void ZoomStart(InputAction.CallbackContext ctx)
    {
        if(ctx.started && !isZooming)
        {
            isZooming = ctx.started || ctx.performed;

            zoomCoroutine = StartCoroutine(ZoomDetection());
        }
        else if(ctx.canceled)
        {
            isZooming = false;

            ZoomStop();
        }
    }

    private void ZoomStop()
    {
        if(zoomCoroutine != null)
        {
            StopCoroutine(zoomCoroutine);
            zoomCoroutine = null;
        }
    }

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
}
