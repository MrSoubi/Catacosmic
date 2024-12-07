using Sirenix.OdinInspector;
using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class FreePressV2 : MonoBehaviour
{
    [Title("ScriptableObjects")]
    [SerializeField] private RSO_MapInfos mapInfos;
    [SerializeField] private RSE_PointerDown test;
    [SerializeField] private RSO_PointerWorldPosition test2;
    [SerializeField] private RSE_PointerUp test3;

    [Title("Camera Speed")]
    [SerializeField] private float speedCam;
    [SerializeField, SuffixLabel("s")] private float smoothTime;

    [Title("Camera Circle")]
    [SerializeField] private Transform circleCamera;

    [Title("Cinemachine")]
    [SerializeField] private CinemachineCamera cinemachineCamera;
    [SerializeField] private CinemachineConfiner2D cinemachineConfiner;

    private Vector3 velocity;
    private Vector3 touchPress;
    private Vector3 freePressTouch;
    private Vector3 freePressTouchPos;
    private Vector3 freePressDist;

    private Vector2 boundsSize;

    private bool isPressed;
    private bool isDecelerating;

    private Coroutine moveCoroutine;
    private Coroutine deceleratingCoroutine;

    private void OnValidate()
    {
        if (speedCam < 0)
        {
            speedCam = 0;
        }

        if (smoothTime < 0)
        {
            smoothTime = 0;
        }
    }

    private void Awake()
    {
        test.Fire += TouchDown;
        test2.onValueChanged += TouchPos;
        test3.Fire += TouchUp;
    }

    private void OnDisable()
    {
        test.Fire -= TouchDown;
        test2.onValueChanged -= TouchPos;
        test3.Fire -= TouchUp;
    }

    private void Start()
    {
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

        freePressTouchPos = touchPress;
        freePressTouchPos.z = -10;

        while (isPressed)
        {
            Vector3 currentTouchPos = touchPress;
            currentTouchPos.z = -10;

            freePressDist = freePressTouchPos - currentTouchPos;
            freePressDist.z = -10;

            Vector3 targetPosition = transform.position + freePressDist;

            targetPosition = LockToCameraBorder(targetPosition);

            targetPosition.z = -10;

            velocity = (targetPosition - transform.position) / speedCam * Time.deltaTime;

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
        float decelerationFactor = 6f;
        float velocityThreshold = 0.1f;

        while (isDecelerating)
        {
            Vector3 targetPosition = transform.position + velocity * Time.deltaTime;

            targetPosition = LockToCameraBorder(targetPosition);
            targetPosition.z = -10;

            transform.position = targetPosition;

            velocity *= decelerationFactor;

            mapInfos.CameraTransform = transform.position;

            if (velocity.magnitude < velocityThreshold)
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
        if(isPressed)
        {
            isPressed = false;

            isDecelerating = true;

            if (deceleratingCoroutine != null)
            {
                StopCoroutine(deceleratingCoroutine);
                deceleratingCoroutine = null;
            }

            if (deceleratingCoroutine == null)
            {
                deceleratingCoroutine = StartCoroutine(Deceleration());
            }
        }
    }

    /// <summary>
    /// Zoom
    /// </summary>
    public void Zoom()
    {
        cinemachineCamera.Lens.OrthographicSize += 1;

        float newScale = cinemachineCamera.Lens.OrthographicSize / 50f;

        circleCamera.localScale = new Vector3(newScale, newScale, 1);

        cinemachineConfiner.InvalidateBoundingShapeCache();
        cinemachineConfiner.InvalidateLensCache();
    }
}