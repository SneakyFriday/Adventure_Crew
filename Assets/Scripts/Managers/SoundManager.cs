using System;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private AudioSource bgmSource, sfxSource;
    [SerializeField] private AudioClip[] bgmClips;
    [SerializeField] private AudioClip introClip;

    public static AudioMixerGroup bgmMixerGroup, sfxMixerGroup;
    private float bgmVolume = 0.5f;
    private float sfxVolume = 0.5f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Get the audio mixer groups
        bgmMixerGroup = mixer.FindMatchingGroups("Master")[1];
        sfxMixerGroup = mixer.FindMatchingGroups("Master")[2];

        // Load the saved volume settings from PlayerPrefs or a config file
        bgmVolume = PlayerPrefs.GetFloat("bgmVolume", bgmVolume);
        sfxVolume = PlayerPrefs.GetFloat("sfxVolume", sfxVolume);

        // Set the initial volumes for the audio mixer groups
        mixer.SetFloat("bgmVolume", Mathf.Log10(bgmVolume) * 20);
        mixer.SetFloat("sfxVolume", Mathf.Log10(sfxVolume) * 20);

        // Set the initial volumes for the audio sources
        bgmSource.volume = bgmVolume;
        sfxSource.volume = sfxVolume;
    }

    public void SetBGMVolume(float volume)
    {
        // Update the audio mixer group volume for BGM
        mixer.SetFloat("bgmVolume", Mathf.Log10(volume) * 20);

        // Update the volume for the audio source
        bgmSource.volume = volume;

        // Save the volume setting for future use
        bgmVolume = volume;
        PlayerPrefs.SetFloat("bgmVolume", bgmVolume);
        PlayerPrefs.Save();
    }

    public void SetSFXVolume(float volume)
    {
        // Update the audio mixer group volume for SFX
        mixer.SetFloat("sfxVolume", Mathf.Log10(volume) * 20);

        // Update the volume for the audio source
        sfxSource.volume = volume;

        // Save the volume setting for future use
        sfxVolume = volume;
        PlayerPrefs.SetFloat("sfxVolume", sfxVolume);
        PlayerPrefs.Save();
    }
    
    public void PlayBGM(int index, AudioMixerGroup bgmMixerGroup)
    {
        bgmSource.clip = bgmClips[index];
        bgmSource.outputAudioMixerGroup = bgmMixerGroup;
        bgmSource.Play();
    }

    public void PlayIntro()
    {
        bgmSource.clip = introClip;
        bgmSource.Play();
    }
    
    public float GetBgmVolume()
    {
        return bgmVolume;
    }
    
    public float GetSfxVolume()
    {
        return sfxVolume;
    }

    public void StopMusic()
    {
        bgmSource.Stop();
    }

    public void PlaySFX(AudioClip hoverSound, AudioMixerGroup sfxMixerGroup)
    {
        sfxSource.clip = hoverSound;
        sfxSource.outputAudioMixerGroup = sfxMixerGroup;
        sfxSource.Play();
    }
}