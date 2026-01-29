using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] //visible in inspector

public class SoundManager : MonoBehaviour
{
    public Sound[] sounds; //list of sounds with Sound class
    public Sound[] backgroundMusic; //list of bgm
    public AudioSource Speaker;
    

    public void Awake() // for background music
    {
        //handles background music startup
    }
 
    //IEnumerator Jukebox() 
    //{
        //handle background music
        //use Lerp to fade music in and out
   // }


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
