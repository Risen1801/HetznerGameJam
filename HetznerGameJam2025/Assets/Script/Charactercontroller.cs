using System.Drawing;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Charactercontroller : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float speed = 10f;
    [SerializeField] float jumpForce = 50f;
    [SerializeField] float leftborder = 0f;
    [SerializeField] float rightborder = 100f;

    [Header("Grabbing")]
    [SerializeField] float grabheight = 2f;
    [SerializeField] LayerMask layerMask;

    [Header("Shooting")]
    [SerializeField] Transform launchingPoint;
    [SerializeField] float shootingStrength = 100f;
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] int linePoints = 175;
    [SerializeField] float timeIntervalBetweenPoints = 0.01f;

    Rigidbody rb;
    CapsuleCollider collider;
    bool isGrounded = true;
    GameObject currentTarget;
    GameObject grabbedObject;

    Vector2 movingParameters = Vector2.zero;
    Vector2 aimingParameters = Vector2.zero;
    bool isJumping;
    bool interactionStarted;
    bool interactionFinished;
    PlayerInput pInput;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        collider = GetComponent<CapsuleCollider>();
        pInput = GetComponent<PlayerInput>();
        launchingPoint.transform.localPosition = new Vector3(0, grabheight, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (grabbedObject)
        {
            grabbedObject.transform.localPosition = new Vector3(0, grabheight, transform.position.z);
        }

        // Moving
        transform.Translate(Vector3.right * movingParameters.x * speed * Time.deltaTime);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, leftborder, rightborder), transform.position.y, transform.position.z);

        // Jumping
        if (isJumping && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce);
            isGrounded = false;
            isJumping = false;
        }

        // Interaction
        Vector3 raycastOrigin = new Vector3(transform.position.x, transform.position.y - collider.bounds.size.y / 2, transform.position.z);
        RaycastHit[] hits = Physics.SphereCastAll(raycastOrigin, 1, Vector3.up, 1, layerMask);
        Vector3 velocity = launchingPoint.forward.normalized * shootingStrength;

        if (hits.Length > 0)
        {
            currentTarget = hits[0].collider.gameObject;
        }
        else
        {
            currentTarget = null;
        }

        if (interactionFinished)
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

            interactionStarted = false;
            interactionFinished = false;
        }

        if (interactionStarted && grabbedObject && grabbedObject.CompareTag("Throwable"))
        {
            if (pInput.currentControlScheme == "Keyboard&Mouse")
            {
                launchingPoint.transform.LookAt(launchingPoint.transform.position + new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0).normalized);
            }
            else
            {
                launchingPoint.transform.LookAt(launchingPoint.transform.position + new Vector3(aimingParameters.x, aimingParameters.y, 0).normalized);
            }

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

    public void OnMove(InputAction.CallbackContext context)
    {
        movingParameters = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        aimingParameters = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        isJumping = context.performed;
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        interactionStarted = true;
        interactionFinished = context.canceled;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
