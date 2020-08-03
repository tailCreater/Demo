using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class volumeControl : MonoBehaviour
{
    public AudioSource asound;
    public UISlider slider;


    // Update is called once per frame
    void Update()
    {
        asound.volume = slider.value / 2;
    }
}
