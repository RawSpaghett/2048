using System.Collections.Generic;
using UnityEngine;
//for lists

public class TileBoard : MonoBehaviour
{
    public Tile TilePrefab;
    private TileGrid grid;
    private List <Tile> tiles;
    public TileState[] tileStates;
    //takes in the states we made in the editor as scriptable objects

    private void Awake()
    {
        grid = GetComponentInChildren <TileGrid>();
        //change number below to hard-code the size in a way, not required
        tiles = new List<Tile>();
    }
    private void Start()
    {
        CreateTile();
        CreateTile();
    }

     private void CreateTile()
    {
       Tile tile = Instantiate(TilePrefab,grid.transform);
       //need to set the state and position of the tiles
       tile.SetState(tileStates[0],2);
        // This function is responsible for the number to come out, change this to randomize the tiles to sometimes spawn a four
        tile.Spawn(grid.GetRandomEmptyCell()); // could in theory return null, but is only responsible for the intital tiles
        tiles.Add(tile);
    }
    private void Update()
    //get user inputs
    {
        //all of these check in the starting directions
        if (Input.GetKeyDown(KeyCode.W)|| Input.GetKeyDown(KeyCode.UpArrow))
        {
            MoveTiles(Vector2Int.up,0,1,1,1); //checks top down
        } else if (Input.GetKeyDown(KeyCode.S)|| Input.GetKeyDown(KeyCode.DownArrow)){
            MoveTiles(Vector2Int.down,0,1,grid.height - 2,-1); //checks bottom up
        } else if (Input.GetKeyDown(KeyCode.A)|| Input.GetKeyDown(KeyCode.LeftArrow)){
            MoveTiles(Vector2Int.left,1,1,0,1);//checks left to right
        } else if (Input.GetKeyDown(KeyCode.D)|| Input.GetKeyDown(KeyCode.RightArrow)){
            MoveTiles(Vector2Int.right, grid.width-2,-1,0,1); //checks right to left
        }
    }
    private void MoveTiles(Vector2Int direction,int startX, int incrementX, int startY, int incrementY)
    //IMPORTANT, The direction you read the grid must be in the direction you are going (Moving up, read top down etc)
    //using the Starts and increments prevents redundancy with all directions
    {
        for (int x = startX;x >= 0 && x < grid.width; x += incrementX)
        {
            for (int y = startY; y >= 0 && y < grid.height; startY += incrementY )
            {
                TileCell cell = grid.GetCell(x,y);

                if (cell.occupied)
                {
                    MoveTile(cell.tile,direction);
                }
            }
        }
    }
    private void MoveTile(Tile tile, Vector2Int direction)
    {
        TileCell newCell = null;
        TileCell adjacent = grid.GetAdjacentCell(tile.cell,direction);

        while(adjacent != null)
        {
            if(adjacent.occupied)
            {
                //Merging
            }
        }
    }
}
