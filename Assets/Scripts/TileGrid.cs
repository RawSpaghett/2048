
using UnityEngine;

public class TileGrid : MonoBehaviour
{
   //tracks rows and cells
   public TileRow[] rows {get; private set;}
   public TileCell[] cells {get; private set;}

    //Scalability logic
   public int size => cells.Length;
   public int height => rows.Length;
   public int width => size/height;

   private void Awake(){
    rows = GetComponentsInChildren<TileRow>();
    cells = GetComponentsInChildren<TileCell>();
   }
   private void Start (){
        //iterates through all of the rows, arrays start at 0!
        for (int y = 0; y < rows.Length; y++)
        {
            //then loops through all of the cells in row i
            for(int x = 0; x < rows[y].cells.Length; x++)
            {
                rows[y].cells[x].coordinates = new Vector2Int(x,y);
            }

        }
   }

    //helper function to identify the location of a cell
    public TileCell GetCell (int x, int y)
    {
        if (x>=0 && x< width && y >=0 && y< height)
        {
            return rows[y].cells[x];
        } else {
            return null;
        }
    }

    //overload
    public TileCell GetCell(Vector2Int coordinates)
    {
        return GetCell(coordinates.x,coordinates.y);
    }

    //Helper function to identify adjacent cells
    public TileCell GetAdjacentCell(TileCell cell, Vector2Int direction)
    {
        Vector2Int coordinates = cell.coordinates;
        coordinates.x += direction.x;
        coordinates.y -= direction.y; //inverse

        return GetCell(coordinates);
    } 

   //Helper function to identify an empty cell on the grid
   public TileCell GetRandomEmptyCell()
   {
        //finds a random space from o to the max cell amount and then checks to see if its occupied
        int index = Random.Range(0,cells.Length);
        int startingIndex = index;
        //takes in bool from TileCell.cs
        while (cells[index].occupied)
        {
            index++;
            //skip if occupied
            
            //makes sure it doesnt go out of bounds
            if (index >= cells.Length)
            {
                index = 0;
            }
            //checks to see if all cells have been checked, preventing infinite loop 
            if (index == startingIndex)
            {
                return null;
            }
        }
        return cells[index];

   }
}
