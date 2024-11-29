using UnityEngine;

public class Player : MonoBehaviour
{
    public JoyStick joyStick;

    public float speed;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if(joyStick != null && (joyStick.joystickVec.x != 0 || joyStick.joystickVec.y != 0))
        {
            rb.linearVelocity = new Vector2(joyStick.joystickVec.x * speed, joyStick.joystickVec.y * speed);
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
    }
}
