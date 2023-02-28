using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFade : MonoBehaviour
{
    private float startVolume;
    private float fadeSpeed = 0.2f;
    private void Start()
    {
        startVolume = GetComponent<AudioSource>().volume;
    }
    public void AudioFadeOut()
    {
        StartCoroutine(MusicFade());
    }
    
    IEnumerator MusicFade()
    {
        while (GetComponent<AudioSource>().volume > 0)
        {
            GetComponent<AudioSource>().volume -= startVolume * Time.deltaTime * fadeSpeed;
            yield return null;
        }
        GetComponent<AudioSource>().Stop();
        GetComponent<AudioSource>().volume = startVolume;
    }
}
