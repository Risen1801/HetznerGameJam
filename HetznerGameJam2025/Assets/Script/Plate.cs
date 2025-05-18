using System;
using UnityEngine;

public class Plate : MonoBehaviour
{
    public AudioManager audioManager;
    public GameObject forest;
    public ParticleSystem mushroomParticles;
    public ParticleSystem freezeParticles;

    private void Start()
    {
        audioManager = FindFirstObjectByType<AudioManager>();                                                        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("MushroomPotion"))
        {
            forest.SetActive(true);
            Instantiate(mushroomParticles, collision.transform.position, collision.transform.rotation);
        }
        if (collision.collider.CompareTag("FreezePotion"))
        {
            Instantiate(freezeParticles, collision.transform.position, collision.transform.rotation);
        }

        if (!collision.collider.CompareTag("Player"))
        {
            audioManager.PlaySFX(audioManager.objectHitsGround);
        }
    }
}
