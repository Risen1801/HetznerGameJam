using UnityEngine;

public class LetItemsBounceOnShrooms : MonoBehaviour
{
    public float JumpStrength = 5;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * JumpStrength, ForceMode.Impulse);
    }
}
