using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 8f;

    [Header("Jump")]
    public float jumpForce = 6f;

    private Rigidbody rb;
    private bool isGrounded;

    private bool mobileForward;
    private bool mobileBackward;
    private bool mobileLeft;
    private bool mobileRight;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Keyboard Jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    void FixedUpdate()
    {
        // Check if player is standing on something
        isGrounded = Physics.Raycast(
            transform.position,
            Vector3.down,
            1.1f
        );

        float horizontal = 0f;
        float vertical = 0f;

        // Keyboard
        if (Input.GetKey(KeyCode.LeftArrow))
            horizontal = -1f;

        if (Input.GetKey(KeyCode.RightArrow))
            horizontal = 1f;

        if (Input.GetKey(KeyCode.UpArrow))
            vertical = 1f;

        if (Input.GetKey(KeyCode.DownArrow))
            vertical = -1f;

        // Mobile
        if (mobileLeft)
            horizontal = -1f;

        if (mobileRight)
            horizontal = 1f;

        if (mobileForward)
            vertical = 1f;

        if (mobileBackward)
            vertical = -1f;

        Vector3 direction =
            new Vector3(horizontal, 0f, vertical).normalized;

        Vector3 velocity = rb.linearVelocity;

        rb.linearVelocity = new Vector3(
            direction.x * moveSpeed,
            velocity.y,
            direction.z * moveSpeed
        );

        // Rotate player
        if (direction.magnitude > 0.1f)
        {
            Quaternion targetRotation =
                Quaternion.LookRotation(direction);

            rb.MoveRotation(
                Quaternion.Slerp(
                    rb.rotation,
                    targetRotation,
                    rotationSpeed * Time.fixedDeltaTime
                )
            );
        }
    }

    // JUMP
    public void Jump()
    {
        if (isGrounded)
        {
            rb.AddForce(
                Vector3.up * jumpForce,
                ForceMode.Impulse
            );
        }
    }

    // MOBILE MOVEMENT

    public void ForwardDown()
    {
        mobileForward = true;
    }

    public void ForwardUp()
    {
        mobileForward = false;
    }

    public void BackwardDown()
    {
        mobileBackward = true;
    }

    public void BackwardUp()
    {
        mobileBackward = false;
    }

    public void LeftDown()
    {
        mobileLeft = true;
    }

    public void LeftUp()
    {
        mobileLeft = false;
    }

    public void RightDown()
    {
        mobileRight = true;
    }

    public void RightUp()
    {
        mobileRight = false;
    }

    // RESTART
    public void RestartGame()
    {
        SceneManager.LoadScene(
            SceneManager.GetActiveScene().name
        );
    }
}