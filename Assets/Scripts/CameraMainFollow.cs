using UnityEngine;

public class CameraMainFollow : MonoBehaviour
{
    public Transform target;

    private void Update()
    {
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
    }
}
