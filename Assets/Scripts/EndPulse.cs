using UnityEngine;
using UnityEngine.UI; 

public class EndPulse : MonoBehaviour //written by gemini
{
    public Image background;
    public float speed = 0.5f;

    void Awake()
    {
        background = GetComponentInChildren<Image>(); // because the script is added dynamically, we need to connect the text to this script
        //GetComponentsInChildren returns an array
    }
    void Update() //adjusts every frame
    {
        //Time.time (actual time since application has started) times speed variable with the modulus operator to keep the number between 1 and 0
        float hue = (Time.time * speed) % 1.0f;

        // Color.HSVToRGB(hue, saturation, value)
        // Takes in these parameters and spits out a color on the wheel
        background.color = Color.HSVToRGB(hue, 0.5f, 0.5f);
    }
}