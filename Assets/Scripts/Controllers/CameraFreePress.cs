using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;

public class CameraFreePress : MonoBehaviour
{
    [Title("Output Data")]
    [SerializeField] private RSO_CameraPosition cameraPosition;

    [Title("Input Data")]
    [SerializeField] private RSO_PointerWorldPosition pointerWorldPosition;

    [Title("Input Events")]
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
        circleCamera.localScale = new Vector3(cameraSize / 50f, cameraSize / 50f, 1);
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

    private void MoveCamera()
    {
        if (isTouch)
        {
            movement = touchCurrentPos - touchPos;

            velocity = (movement / Time.deltaTime) * speedDecelaration;

            transform.position -= new Vector3(movement.x, movement.y, 0);

            cameraPosition.Value = transform.position;
        }
    }

    private IEnumerator Deceleration()
    {
        while (isDecelerating)
        {
            velocity = Vector2.Lerp(velocity, Vector2.zero, 0.1f);

            transform.position -= new Vector3(velocity.x, velocity.y, 0) * Time.deltaTime;

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
}