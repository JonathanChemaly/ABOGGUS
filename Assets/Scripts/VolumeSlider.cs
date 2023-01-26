using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class VolumeSlider : MonoBehaviour
{
    public Slider slider;

    public void Start()
    {
        slider.onValueChanged.AddListener(delegate { AdjustVolume(); });
    }
    public void AdjustVolume()
    {
        AudioListener.volume = slider.value;
    }
}
