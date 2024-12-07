using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform target;

    void LateUpdate()
    {
        Vector3 newPosition = new Vector3(target.position.x, target.position.y, -10);
        transform.position = newPosition;
        
    }
}
