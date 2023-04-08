using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


/***************************************************************************************
*    Title: UI Volume Slider using AUDIO MIXERS | Unity Audio
*    Author: SpeedTutor
*    Date:  08 Feb 2023
*    Availability: https://www.youtube.com/watch?v=C1gCOoDU29M
*
***************************************************************************************/
public class MixerController : MonoBehaviour
{
    [SerializeField] private AudioMixer myAudioMixer;

    public void SetVolumeSoundEffects(float sliderValue)
    {
        myAudioMixer.SetFloat("SoundEffectsVolume", Mathf.Log10(sliderValue) * 20);
    }
    public void SetVolumeMusic(float sliderValue)
    {
        myAudioMixer.SetFloat("MusicVolume", -18 + (Mathf.Log10(sliderValue) * 20));
    }
}
