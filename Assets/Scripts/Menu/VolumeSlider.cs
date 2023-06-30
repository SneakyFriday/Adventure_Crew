using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] private Slider sliderSfx, sliderBgm;
    private void Start()
    {
        // Set the slider values to the saved volume
        sliderBgm.value = SoundManager.Instance.GetBgmVolume();
        sliderSfx.value = SoundManager.Instance.GetSfxVolume();

        // Transfer the slider values to the SoundManager to set the volume
        sliderBgm.onValueChanged.AddListener(val => SoundManager.Instance.SetBGMVolume(val));
        sliderSfx.onValueChanged.AddListener(val => SoundManager.Instance.SetSFXVolume(val));
    }
}
