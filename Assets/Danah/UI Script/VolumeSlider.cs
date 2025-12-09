using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    public AudioSource bgm;
    public Slider slider;

    void Start()
    {
        slider.value = bgm.volume;
    }

    public void OnVolumeChange()
    {
        bgm.volume = slider.value; 
    }
}

