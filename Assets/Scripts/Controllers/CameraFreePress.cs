using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;

public class CameraFreePress : MonoBehaviour
{
    [Title("Output Data")]
    [SerializeField] private RSO_CurrentMapBounds currentMapBounds;
    [SerializeField] private RSO_CurrentPlayerSize currentPlayerSize;
    [SerializeField] private RSO_CameraPosition cameraPosition;

    [Title("Input Data")]
    [SerializeField] private RSO_PointerWorldPosition pointerWorldPosition;

    [Title("Input Events")]
    [SerializeField] private RSE_PointerDown pointerDown;
    [SerializeField] private RSE_PointerUp pointerUp;
    [SerializeField] private RSE_CallZoom callZoom;
    [SerializeField] private RSE_CallDeZoom callDeZoom;

    [Title("Camera References")]
    [SerializeField] private Camera cameraMain;
    [SerializeField] private Transform circleCamera;

    [Title("Camera Parameters")]
    [SerializeField] private float cameraSize;
    [SerializeField] private float speedDecelaration;
    [SerializeField] private float velocityMinDecelaration;
    [SerializeField] private int maxZoom;
    [SerializeField] private int minZoom;
    [SerializeField] private int stepZoom;

    private Vector2 touchPos;
    private Vector2 touchCurrentPos;
    private Vector2 movement;
    private Vector2 velocity;

    private bool isTouch;
    private bool isDecelerating;

    private Coroutine decelerationCoroutine;
    private Coroutine zoomCoroutine;

    private float refVelocity;

    private void OnValidate()
    {
        cameraMain.orthographicSize = cameraSize;
        circleCamera.localScale = new Vector3(cameraSize / 50f, cameraSize / 50f, 1);
    }

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Application.targetFrameRate = 60;

        pointerDown.Fire += TouchDown;
        pointerWorldPosition.onValueChanged += TouchPos;
        pointerUp.Fire += TouchUp;

        callZoom.Fire += Zoom;
        callDeZoom.Fire += DeZoom;
    }

    private void OnDisable()
    {
        pointerDown.Fire -= TouchDown;
        pointerWorldPosition.onValueChanged -= TouchPos;
        pointerUp.Fire -= TouchUp;

        callZoom.Fire -= Zoom;
        callDeZoom.Fire -= DeZoom;
    }

    private void Start()
    {
        cameraPosition.Value = transform.position;
    }

    private Vector3 LockToCameraBorder(Vector3 newPosition)
    {
        if (currentMapBounds.MapBounds.extents.x > 0 && currentMapBounds.MapBounds.extents.y > 0)
        {
            Vector2 confinerBounds = currentMapBounds.MapBounds.extents;
            Vector2 confinerCenter = currentMapBounds.MapBounds.center;

            Vector2 playerSize = currentPlayerSize.Value;

            float minX = confinerCenter.x - confinerBounds.x + playerSize.x;
            float maxX = confinerCenter.x + confinerBounds.x - playerSize.x;
            float minY = confinerCenter.y - confinerBounds.y + playerSize.y;
            float maxY = confinerCenter.y + confinerBounds.y - playerSize.y;

            newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
            newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);
        }

        return newPosition;
    }

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

    private void TouchPos(Vector2 pos)
    {
        touchCurrentPos = pos;

        MoveCamera();
    }

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

    private void Zoom()
    {
        if (zoomCoroutine != null)
        {
            StopCoroutine(zoomCoroutine);
        }

        zoomCoroutine = StartCoroutine(SmoothDeZoomCoroutine(-stepZoom));
    }

    private void DeZoom()
    {
        if (zoomCoroutine != null)
        {
            StopCoroutine(zoomCoroutine);
        }

        zoomCoroutine = StartCoroutine(SmoothDeZoomCoroutine(stepZoom));
    }

    private IEnumerator SmoothDeZoomCoroutine(float value)
    {
        float targetValue = Mathf.Clamp(cameraMain.orthographicSize + value, maxZoom, minZoom);

        while (!Mathf.Approximately(cameraMain.orthographicSize, targetValue))
        {
            cameraMain.orthographicSize = Mathf.SmoothDamp(cameraMain.orthographicSize, targetValue, ref refVelocity, 0.1f);

            circleCamera.localScale = new Vector3(cameraMain.orthographicSize / 50f, cameraMain.orthographicSize / 50f, 1);

            yield return null;
        }

        cameraMain.orthographicSize = targetValue;
        circleCamera.localScale = new Vector3(cameraMain.orthographicSize / 50f, cameraMain.orthographicSize / 50f, 1);

        zoomCoroutine = null;
    }
}