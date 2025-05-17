using UnityEngine;

public class WeightController : MonoBehaviour
{

    public float Speed = 10;
    public float minRangeX = 4.5f;
    public float maxRangeX = 1.5f;
    public float minRangeY = 5f;
    public float maxRangeY = 1.3f;

    public GameObject Hand;
    public GameObject InteractCanvas;
    public GameObject LetGoCanvas;
    public GameObject CombineCanvas;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InteractCanvas.SetActive(false);
    }

    private void Update()
    {
        float inputHorizontal = Input.GetAxisRaw("Horizontal");
        float inputVertical = Input.GetAxisRaw("Vertical");

        transform.Translate(Vector3.right * Speed * inputHorizontal * Time.deltaTime);
        transform.Translate(Vector3.up * Speed * inputVertical * Time.deltaTime);

        float clampedX = Mathf.Clamp(transform.position.x, maxRangeX, minRangeX);
        float clampedY = Mathf.Clamp(transform.position.y, maxRangeY, minRangeY);
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);

    }
    
    /*
    private void OnTriggerEnter(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E) && other.gameObject.CompareTag("Gem"))
        {
            Debug.Log("Entered Triggerbox!");
            InteractCanvas.SetActive(true);
            other.transform.position = Hand.transform.position;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (Input.GetKeyDown(KeyCode.E) && collision.gameObject.CompareTag("Gem"))
        {
            Debug.Log("Entered Collision!");
            InteractCanvas.SetActive(true);
            collision.transform.position = Hand.transform.position;
        }
    }
    */
}
