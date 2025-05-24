using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;
    public int speed = 10;
    public int sidewaysForce = 5;
    public float smoothness = 5f;
    public float jumpForce = 5f;


    public float jumpGrav;
    public float fallGrav;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    private bool isGrounded;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Invoke("Move", 1f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (rb.linearVelocity.y < 0)
        {
            rb.linearVelocity += Vector3.up * Physics.gravity.y * (fallGrav - 1) * Time.fixedDeltaTime;
        }
        else if (rb.linearVelocity.y > 0 && !Input.GetKey(KeyCode.W))
        {
            rb.linearVelocity += Vector3.up * Physics.gravity.y * (jumpGrav - 1) * Time.fixedDeltaTime;
        }

        Move();
    }

    void Move()
    {
        int horizontalInput = 0;

        Vector3 velocity = rb.linearVelocity;

        if (isGrounded && rb.linearVelocity.y < 0f)
        {
            velocity.y = 0f;
        }

        if (Input.GetKey("a"))
        {
            horizontalInput = -1;
        }
        if (Input.GetKey("d"))
        {
            horizontalInput = 1;
        }


        Vector3 targetVelocity = new Vector3(horizontalInput * sidewaysForce, velocity.y, speed);
        rb.linearVelocity = Vector3.Lerp(rb.linearVelocity, targetVelocity, Time.fixedDeltaTime * smoothness);

        if (Input.GetKey(KeyCode.W) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

    }
}