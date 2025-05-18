using UnityEngine;
using System.Collections;

public class ItemInteractionController : MonoBehaviour
{
    public GameObject MushroomForest;

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
        if (this.gameObject.CompareTag("FreezePotion") && (collision.gameObject.layer == LayerMask.NameToLayer("Grabable") || collision.gameObject.layer == LayerMask.NameToLayer("Player")))
        {
            var rb = collision.gameObject.GetComponent<Rigidbody>();

            rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationZ;

            Destroy(this.gameObject, 0.2f);

            StartCoroutine(UnlockXAfterDelay(rb, 5f));
        }

        if(this.gameObject.CompareTag("MushroomPotion") && collision.gameObject.CompareTag("Ground"))
        {
            Animator _animator = MushroomForest.GetComponent<Animator>();
            _animator.Play("GrowMushroomForest");

            Destroy(this.gameObject, 0.2f);

            StartCoroutine(ShrinkMushroomForestAfterDelay(10));
        }
    }

    IEnumerator UnlockXAfterDelay(Rigidbody rb, float delay)
    {
        yield return new WaitForSeconds(delay);


        rb.constraints &= ~RigidbodyConstraints.FreezePositionX;
        rb.constraints &= ~RigidbodyConstraints.FreezeRotationX;
    }

    IEnumerator ShrinkMushroomForestAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        Animator _animator = MushroomForest.GetComponent<Animator>();
        _animator.Play("ShrinkMushroomForest");
    }

}
