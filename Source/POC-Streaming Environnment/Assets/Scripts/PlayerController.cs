using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 5f;

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
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        Vector3 move = new Vector3(moveX, 0, moveZ).normalized * speed;
        rb.linearVelocity = new Vector3(move.x, rb.linearVelocity.y, move.z);

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
        }

        Camera.main.transform.Rotate(Vector3.left * Input.GetAxis("Mouse Y"));
        Camera.main.transform.Rotate(Vector3.up * Input.GetAxis("Mouse X"));

        Camera.main.transform.position = transform.position;
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
