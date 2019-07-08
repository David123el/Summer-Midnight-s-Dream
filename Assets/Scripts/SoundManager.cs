using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // Audio players components.
    public AudioSource EffectsSource;
    public AudioSource MusicSource;

    // Random pitch adjustment range.
    public float LowPitchRange = .95f;
    public float HighPitchRange = 1.05f;

    // Singleton instance.
    public static SoundManager Instance = null;

    // Initialize the singleton instance.
    private void Awake()
    {
        GetComponent<Camera>().transform.position = Vector3.zero;

        // If there is not already an instance of SoundManager, set it to this.
        if (Instance == null)
        {
            Instance = this;
        }
        //If an instance already exists, destroy whatever this object is to enforce the singleton.
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        //Set SoundManager to DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
        DontDestroyOnLoad(gameObject);
    }

    //add audio code for unmuted audio.

    /// <summary>
    /// Play a single clip through the sound effects source.
    /// </summary>
    /// <param name="clip"></param>
    public void Play(AudioClip clip)
    {
        EffectsSource.mute = false;

        EffectsSource.clip = clip;
        EffectsSource.Play();
    }

    /// <summary>
    /// Play a single clip through the music source.
    /// </summary>
    /// <param name="clip"></param>
    public void PlayMusic(AudioClip clip)
    {
        MusicSource.mute = false;

        MusicSource.clip = clip;
        MusicSource.Play();
    }

    /// <summary>
    /// Stops All Music.
    /// </summary>
    public void StopMusic()
    {
        EffectsSource.Stop();
        MusicSource.Stop();
    }

    /// <summary>
    /// Loop Background Music.
    /// </summary>
    public void LoopBGMusic()
    {
        MusicSource.loop = true;
    }

    /// <summary>
    /// Loop Background Music.
    /// </summary>
    public void LoopSFXMusic()
    {
        EffectsSource.loop = true;
    }

    /// <summary>
    /// Stop Looping Background Music.
    /// </summary>
    public void StopLoopBGMusic()
    {
        MusicSource.loop = false;
    }

    /// <summary>
    /// Stop Looping SoundEffect Music.
    /// </summary>
    public void StopLoopSFXMusic()
    {
        EffectsSource.loop = false;
    }

    /// <summary>
    /// Play a random clip from an array, and randomize the pitch slightly.
    /// </summary>
    /// <param name="clips"></param>
    public void RandomSoundEffect(params AudioClip[] clips)
    {
        int randomIndex = Random.Range(0, clips.Length);
        float randomPitch = Random.Range(LowPitchRange, HighPitchRange);

        EffectsSource.pitch = randomPitch;
        EffectsSource.clip = clips[randomIndex];
        EffectsSource.Play();
    }

    /// <summary>
    /// Mute all Music.
    /// </summary>
    public void Mute()
    {
        EffectsSource.mute = true;
        MusicSource.mute = true;
    }

    /// <summary>
    /// UnMute all Music.
    /// </summary>
    public void UnMute()
    {
        EffectsSource.mute = false;
        MusicSource.mute = false;
    }
}