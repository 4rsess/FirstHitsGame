using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    public Toggle toggleMusic;
    public Slider sliderMusic;
    public AudioSource audio;
    public float volume;

    void Start()
    {
        Load();
        ValueMusic();

        toggleMusic = GameObject.FindGameObjectWithTag("Toggle").GetComponent<Toggle>();
        sliderMusic = GameObject.FindGameObjectWithTag("Slider").GetComponent<Slider>();
    }

    public void SliderMusic()
    {
        volume = sliderMusic.value;
        Save();
        ValueMusic();
    }

    public void ToggleMusic()
    {

        if (toggleMusic.isOn == true)
        {
            volume = 1;
        }
        else
        {
            volume = 0;
        }
        Save();
        ValueMusic();
    }

    private void ValueMusic()
    {
        audio.volume = volume;
        sliderMusic.value = volume;
        if (volume == 0)
        {
            toggleMusic.isOn = false;
        }
        else
        {
            toggleMusic.isOn = true;
        }
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("volume", volume);
    }

    private void Load()
    {
        volume = PlayerPrefs.GetFloat("volume", volume);
    }
}
