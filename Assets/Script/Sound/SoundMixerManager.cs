using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class SoundMixerManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider slider;
    
    public void SetMasterVolume()
    {
        float level = slider.value;
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(level) * 20f);
    }

    public void SetSFXVolume()
    {
        float level = slider.value;
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(level) * 20f);
    }

    public void SetMusicVolume()
    {
        float level = slider.value;
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(level) * 20f);
    }


}
