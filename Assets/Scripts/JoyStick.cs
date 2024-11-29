using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class JoyStick : MonoBehaviour
{
    public Transform cam;
    public Transform player;
    public float speedCam;

    public GameObject joystick;
    public GameObject joystickBG;

    public Vector2 joystickVec;
    private Vector3 joystickOriginalPos;
    private Vector3 touchPress;
    private Vector3 joystickTouchPos;
    private Vector2 joystickDist;
    private float joystickRadius;
    private bool isPressed;
    private bool isJoyStickMove;

    private Coroutine coroutine;

    private void Start()
    {
        joystickRadius = joystickBG.GetComponent<RectTransform>().sizeDelta.y / 4;

        joystickOriginalPos = joystickBG.GetComponent<RectTransform>().anchoredPosition;

        joystickTouchPos = joystickOriginalPos;
    }

    private IEnumerator CameraMove()
    {
        while (isJoyStickMove)
        {
            Vector3 targetPosition = cam.position + new Vector3(joystickVec.x, joystickVec.y, 0);

            cam.position = Vector3.Lerp(cam.position, targetPosition, speedCam * Time.deltaTime);

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

        if (ctx.performed && isJoyStickMove)
        {
            joystickTouchPos = Camera.main.ScreenToWorldPoint(new Vector3(touchPress.x, touchPress.y, 0)) - joystickBG.transform.position;

            joystickVec = Vector2.ClampMagnitude(joystickTouchPos, 1);
            joystickDist = Vector2.ClampMagnitude(joystickTouchPos * joystickRadius, joystickRadius);

            joystick.GetComponent<RectTransform>().anchoredPosition = joystickDist;

            if(coroutine == null)
            {
                coroutine = StartCoroutine(CameraMove());
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
            joystickBG.transform.position = joystickTouchPos;

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

        joystick.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        joystickBG.GetComponent<RectTransform>().anchoredPosition = joystickOriginalPos;

        isJoyStickMove = false;
        
        if(coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
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
}
