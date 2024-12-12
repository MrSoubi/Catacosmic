using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;

public class CameraFreePress : MonoBehaviour
{
    [Title("Output Data")]
    [SerializeField] private RSO_CurrentMapBounds currentMapBounds;
    [SerializeField] private RSO_CurrentPlayerSize currentPlayerSize;
    [SerializeField] private RSO_CameraPosition cameraPosition;
    [SerializeField] private RSO_PointerWorldPosition pointerWorldPosition;

    [Title("Output Events")]
    [SerializeField] private RSE_PointerDown pointerDown;
    [SerializeField] private RSE_PointerUp pointerUp;

    [Title("Camera References")]
    [SerializeField] private Camera cameraMain;
    [SerializeField] private Transform circleCamera;

    [Title("Camera Parameters")]
    [SerializeField] private float cameraSize;
    [SerializeField] private float speedDecelaration;
    [SerializeField] private float velocityMinDecelaration;

    private Vector2 touchPos;
    private Vector2 touchCurrentPos;
    private Vector2 movement;
    private Vector2 velocity;

    private bool isTouch;
    private bool isDecelerating;

    private Coroutine decelerationCoroutine;

    private void OnValidate()
    {
        cameraMain.orthographicSize = cameraSize;

        float newScale = cameraSize / 50f;

        circleCamera.localScale = new Vector3(newScale, newScale, 1);
    }

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Application.targetFrameRate = 60;

        pointerDown.Fire += TouchDown;
        pointerWorldPosition.onValueChanged += TouchPos;
        pointerUp.Fire += TouchUp;
    }

    private void OnDisable()
    {
        pointerDown.Fire -= TouchDown;
        pointerWorldPosition.onValueChanged -= TouchPos;
        pointerUp.Fire -= TouchUp;
    }

    private void Start()
    {
        cameraPosition.Value = transform.position;
    }

    /// <summary>
    /// Lock the Camera inside the Map
    /// </summary>
    /// <param name="newPosition"></param>
    /// <returns></returns>
    private Vector3 LockToCameraBorder(Vector3 newPosition)
    {
        if (currentMapBounds.MapBounds.extents.x > 0 && currentMapBounds.MapBounds.extents.y > 0)
        {
            Vector2 confinerBounds = currentMapBounds.MapBounds.extents;
            Vector2 confinerCenter = currentMapBounds.MapBounds.center;

            Vector2 playerSize = currentPlayerSize.PlayerSize;

            float minX = confinerCenter.x - confinerBounds.x + playerSize.x;
            float maxX = confinerCenter.x + confinerBounds.x - playerSize.x;
            float minY = confinerCenter.y - confinerBounds.y + playerSize.y;
            float maxY = confinerCenter.y + confinerBounds.y - playerSize.y;

            newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
            newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);
        }

        return newPosition;
    }

    /// <summary>
    /// Move the Camera
    /// </summary>
    private void MoveCamera()
    {
        if (isTouch)
        {
            movement = touchCurrentPos - touchPos;

            velocity = (movement / Time.deltaTime) * speedDecelaration;

            transform.position -= new Vector3(movement.x, movement.y, 0);

            transform.position = LockToCameraBorder(transform.position);

            cameraPosition.Value = transform.position;
        }
    }

    /// <summary>
    /// Move the Camera with Deceleration
    /// </summary>
    /// <returns></returns>
    private IEnumerator Deceleration()
    {
        while (isDecelerating)
        {
            velocity = Vector2.Lerp(velocity, Vector2.zero, 0.1f);

            transform.position -= new Vector3(velocity.x, velocity.y, 0) * Time.deltaTime;

            transform.position = LockToCameraBorder(transform.position);

            cameraPosition.Value = transform.position;

            if (velocity.magnitude < 0.1f)
            {
                isDecelerating = false;
            }

            yield return null;
        }

        velocity = Vector2.zero;
    }

    /// <summary>
    /// Touch Position
    /// </summary>
    /// <param name="ctx"></param>
    private void TouchPos(Vector2 pos)
    {
        touchCurrentPos = pos;

        MoveCamera();
    }

    /// <summary>
    /// Touch Down
    /// </summary>
    private void TouchDown()
    {
        isTouch = true;
        isDecelerating = false;

        if (decelerationCoroutine != null)
        {
            StopCoroutine(decelerationCoroutine);
        }

        touchPos = touchCurrentPos;

        velocity = Vector3.zero;
    }

    /// <summary>
    /// Touch Up
    /// </summary>
    private void TouchUp()
    {
        if (isTouch)
        {
            isTouch = false;

            movement = Vector3.zero;

            if (velocity.magnitude >= velocityMinDecelaration)
            {
                isDecelerating = true;

                decelerationCoroutine = StartCoroutine(Deceleration());
            }
        }
    }

    /// <summary>
    /// Zoom
    /// </summary>
    private void Zoom()
    {
        /*cinemachineCamera.Lens.OrthographicSize += 1;

        float newScale = cinemachineCamera.Lens.OrthographicSize / 50f;

        circleCamera.localScale = new Vector3(newScale, newScale, 1);*/
    }
}