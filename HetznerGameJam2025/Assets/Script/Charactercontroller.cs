using System.Drawing;
using UnityEngine;
using UnityEngine.UIElements;

public class Charactercontroller : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] string inputaxis = "Horizontal";
    [SerializeField] float speed = 10f;
    [SerializeField] float jumpForce = 50f;
    [SerializeField] float leftborder = 0f;
    [SerializeField] float rightborder = 100f;
    [SerializeField] KeyCode jumpKey = KeyCode.Space;

    [Header("Grabbing")]
    [SerializeField] float grabheight = 2f;
    [SerializeField] KeyCode interactionKey = KeyCode.E;
    [SerializeField] LayerMask layerMask;

    [Header("Shooting")]
    [SerializeField] Transform launchingPoint;
    [SerializeField] float shootingStregth = 10f;
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] int linePoints = 175;
    [SerializeField] float timeIntervalBetweenPoints = 0.01f;

    Rigidbody rb;
    CapsuleCollider collider;
    bool isGrounded = true;
    GameObject currentTarget;
    GameObject grabbedObject;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        collider = GetComponent<CapsuleCollider>();
        launchingPoint.transform.localPosition = new Vector3(0, grabheight, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis(inputaxis);
        transform.Translate(Vector3.right * horizontalInput * speed * Time.deltaTime);
        launchingPoint.transform.LookAt(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        Vector3 velocity = launchingPoint.forward * shootingStregth;

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, leftborder, rightborder), transform.position.y, transform.position.z);

        if (Input.GetKeyDown(jumpKey) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce);
            isGrounded = false;
        }

        if (grabbedObject)
        {
            grabbedObject.transform.localPosition = new Vector3(0, grabheight, transform.position.z);
        }

        Vector3 raycastOrigin = new Vector3(transform.position.x, transform.position.y - collider.bounds.size.y / 2, transform.position.z);
        RaycastHit[] hits = Physics.SphereCastAll(raycastOrigin, 1, Vector3.up, 1, layerMask);
        if (hits.Length > 0)
        {
            currentTarget = hits[0].collider.gameObject;
        }
        else
        {
            currentTarget = null;
        }

        if (Input.GetKeyUp(interactionKey))
        {
            if (!grabbedObject && currentTarget)
            {
                grabbedObject = currentTarget;
                grabbedObject.transform.SetParent(transform, true);
                grabbedObject.GetComponent<Rigidbody>().useGravity = false;
            }
            else if (grabbedObject)
            {
                grabbedObject.transform.SetParent(null);
                grabbedObject.transform.position = new Vector3(grabbedObject.transform.position.x, grabbedObject.transform.position.y, 0);
                grabbedObject.GetComponent<Rigidbody>().useGravity = true;

                if (grabbedObject.CompareTag("Throwable"))
                {
                    grabbedObject.GetComponent<Rigidbody>().linearVelocity = velocity;
                    lineRenderer.enabled = false;
                }

                grabbedObject = null;
            }
        }

        if (Input.GetKey(interactionKey))
        {
            if(grabbedObject && grabbedObject.CompareTag("Throwable"))
            {
                lineRenderer.enabled = true;

                Vector3 origin = launchingPoint.position;
                lineRenderer.positionCount = linePoints;
                float time = 0;

                for (int i = 0; i < linePoints; i++)
                {
                    float x = (velocity.x * time) + (Physics.gravity.x / 2 * time * time);
                    float y = (velocity.y * time) + (Physics.gravity.y / 2 * time * time);
                    Vector3 point = new Vector3(x, y, 0);
                    lineRenderer.SetPosition(i, origin + point);
                    time += timeIntervalBetweenPoints;
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
