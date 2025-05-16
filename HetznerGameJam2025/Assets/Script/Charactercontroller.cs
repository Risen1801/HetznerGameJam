using UnityEngine;

public class Charactercontroller : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float jumpForce = 50f;
    [SerializeField] private float leftborder = 0f;
    [SerializeField] private float rightborder = 100f;

    [SerializeField] private string inputaxis = "Horizontal";
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;

    private Rigidbody rb;
    private bool isGrounded = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis(inputaxis);
        transform.Translate(Vector3.right * horizontalInput * speed * Time.deltaTime);

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, leftborder, rightborder), transform.position.y, 0);

        if (Input.GetKeyDown(jumpKey) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce);
            isGrounded = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            isGrounded=true;
        }
    }
}
