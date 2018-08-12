using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip ButtonClick;

    public void PlayButtonClickSound()
    {
        GetComponent<AudioSource>().clip = ButtonClick;
        GetComponent<AudioSource>().Play();
    }
}
