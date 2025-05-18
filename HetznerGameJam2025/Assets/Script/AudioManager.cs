using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    private Scene scene;
    
    [Header("Audio Sources")] 
    [SerializeField] AudioSource backgroundSource;
    [SerializeField] AudioSource SFXSource;

    [Header("Potion Effects")] 
    [SerializeField] AudioClip mainMenuMusic;
    [SerializeField] AudioClip backgroundMusic;
    
    public AudioClip firePotionExplosion;
    public AudioClip firePotionSplatter;
    
    public AudioClip icePotionExplosion;
    public AudioClip icePotionSplatter;
    
    public AudioClip mushroomPotionExplosion;
    public AudioClip mushroomPotionSplatter;

    [Header("Other Effects")]
    public AudioClip objectHitsGround;
    public AudioClip objectCombined;
    public AudioClip pickUpObject;
    public AudioClip ThrowingObject;
    
    private void Start()
    {
        if (scene.buildIndex == 0)
        {
            backgroundSource.clip = mainMenuMusic;
            backgroundSource.Play();
        }

        else
        {
            backgroundSource.clip = backgroundMusic;
            backgroundSource.Play();
            backgroundSource.loop = true;
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
    