using UnityEngine;

public class AudioManager : GameSingleton<AudioManager>
{
    #region Fields
    [Header("Volume Settings")]
    [Range(0, 1)]
    public float MasterVolume = 1f;
    [Range(0, 1)]
    public float EffectVolume = 1f;
    [Range(0, 1)]
    public float MusicVolume = 1f;
    #endregion

    private AudioSource cameraAudioSource;


    #region Unity Methods
    private void Start()
    {
        cameraAudioSource = Camera.main.GetComponent<AudioSource>();

        if (cameraAudioSource == null)
        {
            Debug.LogWarning("AudioManager could not find AudioSource on camera");
            return;
        }
    }
    #endregion


    #region Custom Methods
    /// <summary>
    /// Play a sound effect at the camera
    /// </summary>
    /// <param name="clip">Sound effect</param>
    /// <param name="volume">Volume multiplier</param>
    public void PlayEffect(AudioClip clip, float volume = 1f)
    {
        if (clip == null || cameraAudioSource == null) return;

        cameraAudioSource.PlayOneShot(clip, EffectVolume * MasterVolume * volume);
    }

    /// <summary>
    /// Play a sound effect at a point in the world
    /// </summary>
    /// <param name="clip">Sound effect</param>
    /// <param name="position">World position</param>
    /// <param name="volume">Volume multiplier</param>
    public void PlayEffect(AudioClip clip, Vector3 position, float volume = 1f)
    {
        if (clip == null) return;

        AudioSource.PlayClipAtPoint(clip, position, EffectVolume * MasterVolume * volume);
    }
    #endregion
}

