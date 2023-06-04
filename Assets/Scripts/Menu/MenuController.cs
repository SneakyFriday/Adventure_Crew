using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private Resolution[] resolutions;

    public static MenuController Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }


    private void Start()
    {
        // Get available resolutions and add them to the dropdown
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        var options = new List<string>();
        var currentResolutionIndex = 0;
        for (var i = 0; i < resolutions.Length; i++)
        {
            var option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("BGMVolume", volume);
    }

    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("SFXVolume", volume);
    }

    public void SetVoiceVolume(float volume)
    {
        audioMixer.SetFloat("VoiceVolume", volume);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        var resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    
    public void SetMenuActive(bool isActive)
    {
        gameObject.SetActive(isActive);
    }
}

