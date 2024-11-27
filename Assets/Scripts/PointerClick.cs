using UnityEngine;

public class PointerClick : MonoBehaviour
{
    public float speed;

    private Vector3 target;

    private void Start()
    {
        target = transform.position;
    }

    private void FixedUpdate()
    {
        if(Input.GetMouseButton(0))
        {
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            target.z = transform.position.z;

            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        }
    }
}
