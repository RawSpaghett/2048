using TMPro;
using UnityEngine;

public class Pulse : MonoBehaviour // got some help from gemini
{

    public TMP_Text text;
    public float speed = 1f;
    

    void Update()
    {
        float t = Mathf.PingPong(Time.time*speed, 1f); //creates a value that bounces between 1 and 0
        text.color = Color.Lerp(Color.red,Color.black,t); // linearly interpolates between black and re
    }
}
