using UnityEngine;
using System.Collections;

public class ItemInteractionController : MonoBehaviour
{
    private GameObject _frozenObject;
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
        if (this.gameObject.CompareTag("FreezePotion") && collision.gameObject.layer == LayerMask.NameToLayer("CanFreeze"))
        {
            var rb = collision.gameObject.GetComponent<Rigidbody>();

            rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationZ;

            StartCoroutine(UnlockXAfterDelay(rb, 10f));
        }
    }

    IEnumerator UnlockXAfterDelay(Rigidbody rb, float delay)
    {
        yield return new WaitForSeconds(delay);

        rb.constraints &= ~RigidbodyConstraints.FreezePositionX;
        rb.constraints &= ~RigidbodyConstraints.FreezeRotationX;
    }

}
