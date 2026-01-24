using UnityEngine;
using UnityEngine.UI; //for image
using TMPro; //text mesh pro

public class Tile : MonoBehaviour
{
    public TileState state {get; private set;}
    public TileCell cell{get; private set;}
    //where is this tile currently
    public int number{get; private set;}

    private Image background;
    private TextMeshProUGUI text; //without TMP use "text" instead

    private void Awake()
    {
        background = GetComponent<Image>();
        text = GetComponentInChildren<TextMeshProUGUI>();
    }
    

    public void SetState(TileState state, int number)
    {
        this.state = state;
        this.number = number;

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
}
