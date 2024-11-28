using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class JoyStick : MonoBehaviour
{
    public GameObject joystick;
    public GameObject joystickBG;

    public Vector2 joystickVec;

    private Vector2 joystickOriginalPos;

    private Vector2 joystickTouchPos;
    private float joystickRadius;

    private Vector2 joystickDist;

    private void Start()
    {
        joystickRadius = joystickBG.GetComponent<RectTransform>().sizeDelta.y / 4;

        joystickOriginalPos = joystickBG.GetComponent<RectTransform>().anchoredPosition;

        joystickTouchPos = joystickOriginalPos;
    }

    /// <summary>
    /// Touch Down
    /// </summary>
    public void PointerDown()
    {
        Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        joystickTouchPos = touchPos;

        joystickBG.transform.position = joystickTouchPos;
    }

    /// <summary>
    /// Touch Drag
    /// </summary>
    /// <param name="baseEventData"></param>
    public void Drag(BaseEventData baseEventData)
    {
        joystickTouchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - joystickBG.transform.position;

        joystickVec = Vector2.ClampMagnitude(joystickTouchPos, 1);
        joystickDist = Vector2.ClampMagnitude(joystickTouchPos * joystickRadius, joystickRadius);

        joystick.GetComponent<RectTransform>().anchoredPosition = joystickDist;
    }

    /// <summary>
    /// Touch Up
    /// </summary>
    public void PointerUp()
    {
        joystickTouchPos = Vector2.zero;
        joystickVec = Vector2.zero;
        joystickDist = Vector2.zero;

        joystick.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        joystickBG.GetComponent<RectTransform>().anchoredPosition = joystickOriginalPos;
    }
}
