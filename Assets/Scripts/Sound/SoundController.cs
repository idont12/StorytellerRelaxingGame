using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundController : MonoBehaviour
{
    [Header("Mixers")]
    [SerializeField] AudioMixer MusicMixer;
    [SerializeField] AudioMixer SfxMixer;

    [Header("Sliders")]
    [SerializeField] Slider MusicSlider;
    [SerializeField] Slider SfxSlider;

    const string music = "MusicVolume";
    const string sfx = "SfxVolume";


    private void Start()
    {
        //PlayerPrefs.GetFloat("MusicVolume") == 0;
        float musicLevel;
        MusicMixer.GetFloat(music, out musicLevel);
        float MusicLevelPresent = Mathf.Pow(10.0f, musicLevel / 20.0f);
        MusicSlider.value = MusicLevelPresent;
        MusicVolume(MusicSlider.value);

        float sfxLevel;
        SfxMixer.GetFloat(sfx, out sfxLevel);
        float SfxLevelPresent = Mathf.Pow(10.0f, sfxLevel / 20.0f);
        SfxSlider.value = SfxLevelPresent;
        SoundEffectVolume(SfxSlider.value);
    }

    public void MusicVolume(float volume)
    {
        PlayerPrefs.SetFloat("MusicVolume", volume);
        MusicMixer.SetFloat(music, Mathf.Log10(volume) * 20);
        MusicSlider.gameObject.transform.Find("ValueNum").GetComponent<TMP_Text>().text = MathF.Floor(volume * 100).ToString();
    }

    public void SoundEffectVolume(float volume)
    {
        PlayerPrefs.SetFloat("SfxVolume", volume);
        SfxMixer.SetFloat(sfx, Mathf.Log10(volume) * 20);
        SfxSlider.gameObject.transform.Find("ValueNum").GetComponent<TMP_Text>().text = MathF.Floor(volume*100).ToString();
    }

}
