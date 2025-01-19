using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundSettings : MonoBehaviour
{
    [SerializeField] Slider soundSlider; // Reference to the UI Slider
    [SerializeField] AudioMixer masterMixer; // Reference to the AudioMixer

    // Start is called before the first frame update
    private void Start()
    {
        // Load the saved volume or set to default (100)
        float savedVolume = PlayerPrefs.GetFloat("SavedMasterVolume", 100f);
        RefreshSlider(savedVolume);
        SetVolume(savedVolume);
    }

    public void SetVolume(float _value)
    {
        // Ensure value is not less than 0.001 to avoid issues with logarithmic volume calculations
        if (_value < 1f)
        {
            _value = 0.001f;
        }

        // Save the value to PlayerPrefs and update the AudioMixer
        PlayerPrefs.SetFloat("SavedMasterVolume", _value);
        masterMixer.SetFloat("MasterAudio", Mathf.Log10(_value / 100f) * 20f);
    }

    public void SetVolumeFromSlider()
    {
        // Get the value from the slider and apply it
        SetVolume(soundSlider.value);
    }

    private void RefreshSlider(float _value)
    {
        // Update the slider's value
        soundSlider.value = _value;
    }
}

