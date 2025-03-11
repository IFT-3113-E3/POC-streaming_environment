using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 5f;
    public float cameraDistance = 5f;
    public float cameraHeight = 2f;

    private Rigidbody rb;
    private bool isGrounded;

    internal void Start()
    {
        rb = GetComponent<Rigidbody>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    internal void Update()
    {
        HandleMovement();
        HandleJump();
        UpdateCameraPosition();
        HandleEscape();
    }

    private void HandleMovement()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        Vector3 move = new Vector3(moveX, 0, moveZ).normalized * speed;
        rb.linearVelocity = new Vector3(move.x, rb.linearVelocity.y, move.z);
    }

    private void HandleJump()
    {
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
        }
    }

    private void UpdateCameraPosition()
    {
        Vector3 cameraPosition = transform.position + Vector3.back * cameraDistance + Vector3.up * cameraHeight;
        Camera.main.transform.position = cameraPosition;
        Camera.main.transform.LookAt(transform.position + Vector3.up * cameraHeight / 2);
    }

    private void HandleEscape()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    internal void OnCollisionStay(Collision collision)
    {
        isGrounded = true;
    }

    internal void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }
}
