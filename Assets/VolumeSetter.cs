using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSetter : MonoBehaviour
{

    public Slider slider;
    AudioSource aud;
    // Start is called before the first frame update
    void Start()
    {
        aud = gameObject.GetComponent<AudioSource>();
        slider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
    }

    public void ValueChangeCheck()
    {
        AudioListener.volume = slider.value;
        aud.Play();
    }
}
