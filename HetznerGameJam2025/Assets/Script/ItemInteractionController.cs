using UnityEngine;
using System.Collections;

public class ItemInteractionController : MonoBehaviour
{
    public GameObject MushroomForest;

    public AudioManager audioManager;
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
<<<<<<< Updated upstream

            Destroy(this.gameObject, 0.2f);

=======
            
            // added audio to play ice soundeffects
            audioManager.PlaySFX(audioManager.icePotionSplatter);
            StartCoroutine(WaitForSplatterIcePotion(0.25f));
            
>>>>>>> Stashed changes
            StartCoroutine(UnlockXAfterDelay(rb, 5f));
        }

        if(this.gameObject.CompareTag("MushroomPotion") && collision.gameObject.CompareTag("Ground"))
        {
            Animator _animator = MushroomForest.GetComponent<Animator>();
            _animator.Play("GrowMushroomForest");
<<<<<<< Updated upstream

            Destroy(this.gameObject, 0.2f);

=======
             
            // added audio to play mushroom soundeffects
            audioManager.PlaySFX((audioManager.mushroomPotionSplatter));
            StartCoroutine(WaitForSplatterMushroomPotion(0.25f));
            
>>>>>>> Stashed changes
            StartCoroutine(ShrinkMushroomForestAfterDelay(10));
        }
    }

    // added Coroutine to wait for explosion of shatted bottles
    IEnumerator WaitForSplatterIcePotion(float delay)
    {
        yield return new WaitForSeconds(delay);
        
        audioManager.PlaySFX(audioManager.icePotionExplosion);
    }
    
    IEnumerator WaitForSplatterMushroomPotion(float delay)
    {
        yield return new WaitForSeconds(delay);
        
        audioManager.PlaySFX(audioManager.mushroomPotionExplosion);
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
