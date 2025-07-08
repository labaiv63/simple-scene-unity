using UnityEngine;

public class SoundManager : MonoCache
{
    public static SoundManager instance { get; private set; }
    private AudioSource soundSource => GetComponent<AudioSource>();

    private void Start()
    {
        if (instance == null)
            instance = this;

        else if (instance != null && instance != this)
            Destroy(gameObject);
    }

    public void PlaySound(AudioClip _sound)
    {
        soundSource.PlayOneShot(_sound);
    }
}