using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [Header("Music")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioClip backgroundMusic;
    [SerializeField] private AudioClip intenseBackgroundMusic;

    [Header("Sound Effects")]
    [SerializeField] private AudioSource sfxSource;

    private void Start()
    {
        musicSource.clip = backgroundMusic;
        musicSource.Play();

        // TODO: add second background audio source to fade between music
        Temp_TestingMusicSwap();
    }

    #region TestCode
    private void Temp_TestingMusicSwap()
    {
        InvokeRepeating("Temp_PlayIntenseMusic", 7, 7);
        InvokeRepeating("Temp_PlayBkgMusic", 3.5f, 7);
    }
    private void Temp_PlayBkgMusic()
    {
        PlayMusic(backgroundMusic);
    }
    private void Temp_PlayIntenseMusic()
    {
        PlayMusic(intenseBackgroundMusic);
    }
    #endregion

    public void PlayMusic(AudioClip music)
    {
        LoadAudio(musicSource, music);
    }

    public void PlaySound(AudioClip sound)
    {
        sfxSource.PlayOneShot(sound);
    }

    private void LoadAudio(AudioSource channel, AudioClip sound)
    {
        channel.Stop();
        channel.clip = sound;
        channel.Play();
    }
}
