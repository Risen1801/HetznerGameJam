using System;
using UnityEngine;

public class Plate : MonoBehaviour
{
    public AudioManager audioManager;
    public GameObject forest;

    private void Start()
    {
        audioManager = FindFirstObjectByType<AudioManager>();                                                        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("MushroomPotion"))
        {
            forest.SetActive(true);
        }

        if (!collision.collider.CompareTag("Player"))
        {
            audioManager.PlaySFX(audioManager.objectHitsGround);
        }
    }
}
