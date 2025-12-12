using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{


    public AudioMixer audioMixer;
    public Slider musicSlider;
    public Slider SFXSlider;

    public static AudioManager instance;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        float MusicVol = PlayerPrefs.GetFloat("MusicVol", 1f);
        float SFXVol = PlayerPrefs.GetFloat("SFXVol", 1f);

        audioMixer.SetFloat("MusicVolume", Mathf.Log10(MusicVol) * 20);
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(SFXVol) * 20);

        // Add listners to the volume sliders.
        if (musicSlider != null)
        {
            musicSlider.value = MusicVol;
            setMusicVolume(musicSlider.value);
            musicSlider.onValueChanged.AddListener(setMusicVolume);
        }

        if (SFXSlider != null)
        {
            SFXSlider.value = SFXVol;
            setSFXVolume(SFXSlider.value);
            SFXSlider.onValueChanged.AddListener(setSFXVolume);
        }

    }

    public void setMusicVolume(float volume)
    {
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("MusicVol", volume);
    }

    public void setSFXVolume(float volume)
    {
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SFXVol", volume);
    }

}
