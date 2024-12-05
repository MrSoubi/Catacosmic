using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;

public class FreePressV2 : MonoBehaviour
{
    [Header("ScriptableObjects")]
    [SerializeField] private MapInfos mapInfos;
    [SerializeField] private RSE_PointerDown test;
    [SerializeField] private RSO_PointerPosition test2;
    [SerializeField] private RSE_PointerUp test3;

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

    private void Awake()
    {
        test.Fire += TouchDown;
        test2.onValueChanged += TouchPos;
        test3.Fire += TouchUp;
    }

    private void Start()
    {
        mainCamera = Camera.main;

        cinemachineConfiner.BoundingShape2D = mapInfos.BoxCollider2DRef;

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

        if (confinerCollider != null)
        {
            Vector2 confinerBounds = boundsSize;
            Vector2 confinerCenter = confinerCollider.bounds.center;

            Vector2 val = mapInfos.PlayerRef.transform.GetChild(0).GetComponent<SpriteRenderer>().bounds.size / 2;

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

    /// <summary>
    /// Deceleration of the Camera on UnTouch
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
    /// Touch Position
    /// </summary>
    /// <param name="ctx"></param>
    public void TouchPos(Vector2 pos)
    {
        touchPress = pos;
        touchPress.z = -10;
    }

    /// <summary>
    /// Touch Down
    /// </summary>
    public void TouchDown()
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

    /// <summary>
    /// Touch Up
    /// </summary>
    public void TouchUp()
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