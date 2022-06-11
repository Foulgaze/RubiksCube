using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class audioController : MonoBehaviour
{
    // Start is called before the first frame update
    AudioSource audioSource;
    public Slider audioSlider;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0.1f;
        audioSource.Play();
        audioSource.loop = true;
        audioSlider.value = audioSource.volume;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeVolume()
    {
        audioSource.volume = audioSlider.value;
    }
}
