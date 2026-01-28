using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Sound")]
public class Sound : ScriptableObject //Create a custom class for audios that include name and clip
{
    public string audioName;
    public AudioClip clip;
}

