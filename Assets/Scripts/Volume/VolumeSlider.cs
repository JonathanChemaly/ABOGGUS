using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class VolumeSlider : MonoBehaviour
{
    public Slider slider;
    private float initialVolume = 0.25f;

    public void Start()
    {
        slider.onValueChanged.AddListener(delegate { AdjustVolume(); });
        slider.value = initialVolume;
    }
    public void AdjustVolume()
    {
        AudioListener.volume = slider.value;
    }
}
