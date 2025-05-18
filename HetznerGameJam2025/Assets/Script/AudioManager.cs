using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Sources")] 
    [SerializeField] AudioSource backgroundSource;
    [SerializeField] AudioSource SFXSource;

    [Header("Audio Clips")] 
    [SerializeField] AudioClip mainMenuMusic;
    [SerializeField] AudioClip backgroundMusic;
    
    public AudioClip firePotionExplosion;
    public AudioClip firePotionSplatter;
    
    public AudioClip icePotionExplosion;
    public AudioClip icePotionSplatter;
    
    public AudioClip mushroomPotionExplosion;
    public AudioClip mushroomPotionSplatter;

    public AudioClip objectHitsGround;

    private void Start()
    {
        
        backgroundSource.clip = backgroundMusic;
        backgroundSource.Play();
        backgroundSource.loop = true;
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
    