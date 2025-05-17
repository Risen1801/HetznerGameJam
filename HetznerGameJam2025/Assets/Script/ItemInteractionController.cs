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
        if (this.gameObject.CompareTag("FreezePotion") && collision.gameObject.layer == 9)
        {
            collision.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX;
            collision.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX;

            _frozenObject = collision.gameObject;

            StartCoroutine(UnlockXZAfterDelay(10f));
        }
    }

    IEnumerator UnlockXZAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Debug.Log("StartedFreezing");

        _frozenObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX;
        _frozenObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX;

        //StartCoroutine(LockZAxis(0.1f));
    }

    /*
    IEnumerator LockZAxis(float delay)
    {
        yield return new WaitForSeconds(delay);
        Debug.Log("Unfrozen");

        _frozenObject.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationZ;
        _frozenObject.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ;
    }
    */
}
