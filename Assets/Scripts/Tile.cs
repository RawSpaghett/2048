using UnityEngine;
using UnityEngine.UI; //for image
using TMPro; //text mesh pro
using System.Collections; //for coroutines

public class Tile : MonoBehaviour
{
    public TileState state {get; private set;}
    public TileCell cell{get; private set;}
    //where is this tile currently
    public int number{get; private set;}

    public const float duration = 0.1f; //can be edited in inspector now

    public bool locked{get;set;}

    public SoundManager soundManager;

    private Image background;
    private TextMeshProUGUI text; //without TMP use "text" instead

    private void Awake()
    {
        background = GetComponent<Image>();
        text = GetComponentInChildren<TextMeshProUGUI>();
      
        soundManager = FindObjectOfType<SoundManager>(); //solution by Gemini, Had trouble attatching my SoundManager to each tile
    }
    

    public void SetState(TileState state, int number)
    {
        this.state = state;
        this.number = number;
        if( number == 2048)
        {
               gameObject.AddComponent<EndPulse>();
        }
        background.color = state.backgroundColor;
        text.color = state.textColor;
        text.text = number.ToString();
    }

    public void Spawn(TileCell cell)
    {
        if (this.cell != null)
        {
            this.cell.Tile = null;
        }

        //mutally identifys cell to tile and tile to cell
        this.cell = cell;
        this.cell.Tile = this;

        //Sets the position of the tile to the position of the cell
        transform.position = cell.transform.position;
    }
    public void MoveTo(TileCell cell){ //identical to spawning except animation
         if (this.cell != null)
        {
            this.cell.Tile = null;
        }

        //mutally identifys acell to tile and tile to cell
        this.cell = cell;
        this.cell.Tile = this;

        //Sets the position of the tile to the position of the cell
        StartCoroutine(Animate(cell.transform.position,duration, false));
    }

    public void Merge(TileCell cell)
    {
          if (this.cell != null)
        {
            this.cell.Tile = null;
        }

        this.cell = null;
        cell.Tile.locked = true;
        StartCoroutine(Animate(cell.transform.position,duration, true));
    }

    private IEnumerator Animate(Vector3 to,float duration, bool merging) //Animation Coroutine
    {
        //timer
        float elapsed = 0f;

        Vector3 from = transform.position; //current position of tile

        while (elapsed < duration)
        {
            transform.position = Vector3.Lerp(from,to,elapsed/duration); 
            //linear interpolates, essentially slides. Going from A to B
            elapsed += Time.deltaTime;
            //Time.deltaTime is how much time has elapsed since the last frame
            yield return null;
            //coroutines allow you to essentially suspend something until the next frame
        }
        transform.position = to; // to align the tile and avoid slight positioning errors
        

        if (merging) //if its merging, destroy after moving
        {
            Destroy(gameObject);
            soundManager.PlayAudio("Merge");//merge audio
        }
    }
}
