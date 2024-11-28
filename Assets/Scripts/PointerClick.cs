using UnityEngine;
using UnityEngine.EventSystems;

public class PointerClick : MonoBehaviour
{
    public float speed;

    private Vector3 target;

    private void Start()
    {
        target = transform.position;
    }

    private bool IsPointerOverUIObject()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }

    private void FixedUpdate()
    {
        if(Input.GetMouseButton(0) && !IsPointerOverUIObject())
        {
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            target.z = transform.position.z;

            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        }
    }
}
