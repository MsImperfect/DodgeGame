using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;
    public float speed;
    public float sidewaysForce = 10f;
    public float smoothness = 5f;
    public float jumpForce = 9f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    public bool gameOver = false;

    private bool isGrounded;

    void Update()
    {
        if (gameOver) return;

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            Jump();
        }
    }

    void FixedUpdate()
    {
        if (gameOver) return;

        speed = GameManager.Instance.gameSpeed;
        sidewaysForce = GameManager.Instance.sidewaysForce;

        Move();
    }

    void Move()
    {
        float horizontalInput = 0f;

        if (Input.GetKey(KeyCode.A)) horizontalInput = -1f;
        if (Input.GetKey(KeyCode.D)) horizontalInput = 1f;

        Vector3 targetVelocity = new Vector3(horizontalInput * sidewaysForce, rb.linearVelocity.y, speed);
        rb.linearVelocity = Vector3.Lerp(rb.linearVelocity, targetVelocity, Time.fixedDeltaTime * smoothness);
    }

    void Jump()
    {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z); // Reset Y before jump
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
}
