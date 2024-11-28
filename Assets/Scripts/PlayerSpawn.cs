using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    public PlayerTarget playerTarget;

    private void Start()
    {
        playerTarget.location = transform;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawSphere(transform.position, 1f);
    }
}
