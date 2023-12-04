using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SetVolume : MonoBehaviour
{
    public AudioMixer audioMixer;

    public void SetLevelMusic(float sliderValue)
    {
        audioMixer.SetFloat("MusicVol", Mathf.Log10(sliderValue) * 30); //log10 for decibels
    }
    public void SetLevelSound(float sliderValue)
    {
        audioMixer.SetFloat("SoundVol", Mathf.Log10(sliderValue) * 30); //log10 for decibels 
    }
}
