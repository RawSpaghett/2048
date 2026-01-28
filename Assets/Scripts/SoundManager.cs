using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] //visible in inspector

public class SoundManager : MonoBehaviour
{
    public Sound[] sounds; //list of sounds with Sound class
    public AudioSource Speaker;
    public AudioClip bgm;

public void Awake() // for background music
{
    Speaker.clip = bgm; //moves bgm to audiosource component 
    Speaker.loop = true;
    Speaker.Play();
}


    public void PlayAudio(string name)
    {
        for (int i = 0; i < sounds.Length ; i++ ) // loops through list of sounds to find requested
        {
            if(sounds[i].audioName == name )
            {
                Speaker.PlayOneShot(sounds[i].clip); //plays correct audio
                return; //ends loop
            }
        }
        Debug.Log("No audio found"); // if nothing is found, send a message
    }
}
