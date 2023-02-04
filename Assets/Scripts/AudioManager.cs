using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [Header("Audio Channels")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    private AudioSource[] allAudioSources;

    public bool IsMuted { get; private set; } = false;

    private void Awake()
    {
        allAudioSources = GetComponentsInChildren<AudioSource>();
    }

    public void PlayMusic(AudioClip music)
    {
        LoadAudio(musicSource, music);
    }

    public void PlaySound(AudioClip sound)
    {
        sfxSource.PlayOneShot(sound);
    }

    // Use to mute or unmute ALL audio channels handled by the audio manager
    // This is not reliable to call in other script's awake functions
    public void Mute(bool shouldMute)
    {
        foreach (var source in allAudioSources)
        {
            source.mute = shouldMute;
        }
        IsMuted = true;
    }

    private void LoadAudio(AudioSource channel, AudioClip sound)
    {
        channel.Stop();
        channel.clip = sound;
        channel.Play();
    }
}
