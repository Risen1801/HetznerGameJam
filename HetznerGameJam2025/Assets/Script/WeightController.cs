using UnityEngine;

public class WeightController : MonoBehaviour
{
    public GameObject[] Weights;
    public float Speed = 10;
    public float minRangeX = -4.5f;
    public float maxRangeX = -1.5f;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void Update()
    {
        float inputHorizontal = Input.GetAxisRaw("Horizontal");

        transform.Translate(Vector3.right * inputHorizontal * Speed * Time.deltaTime);
        

        float clampedX = Mathf.Clamp(transform.position.x, minRangeX, maxRangeX);
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
    }

}
