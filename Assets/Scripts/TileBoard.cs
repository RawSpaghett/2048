using System.Collections;
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

    private bool waiting; 
    //for animations

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
        if(!waiting) //is false, prevents weird movements
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
        
    }
    private void MoveTiles(Vector2Int direction,int startX, int incrementX, int startY, int incrementY)
    //IMPORTANT, The direction you read the grid must be in the direction you are going (Moving up, read top down etc)
    //using the Starts and increments prevents redundancy with all directions
    {
        bool changed = false; //assume board state hasnt changed

        for (int x = startX;x >= 0 && x < grid.width; x += incrementX)
        {
            for (int y = startY; y >= 0 && y < grid.height; y += incrementY )
            {
                TileCell cell = grid.GetCell(x,y);

                if (cell.occupied)
                {
                    changed |= MoveTile(cell.Tile,direction); //OR equals, once its set to true, it will stay true
                }
            }
        }
        if(changed) // if game state has changed
        {
            StartCoroutine(WaitForChanges());
        }

    }
    private bool MoveTile(Tile tile, Vector2Int direction)
    {
        TileCell newCell = null;
        TileCell adjacent = grid.GetAdjacentCell(tile.cell,direction);

        while(adjacent != null) //keeps looping till out of bounds or unmergable tile
        {
            if(adjacent.occupied)
            {
                if (CanMerge(tile,adjacent.Tile))
                {
                    Merge(tile,adjacent.Tile);
                    return true;
                }
                break;
            }
            newCell = adjacent; //because it moves, the newcell is now the adjacent cell
            adjacent = grid.GetAdjacentCell(adjacent,direction);
        }
        if (newCell != null)
        {
            tile.MoveTo(newCell);
            return true; // did move
        }
        return false; //did not move
    }

    private bool CanMerge(Tile a, Tile b) //not ENTIRELY neccessary
    {
        return a.number == b.number && !b.locked; //numbers have to match a and b and B needs to be  unlocked
    
    }

    private void Merge(Tile a, Tile b) //delete one of them
    {
        tiles.Remove(a); 
        a.Merge(b.cell);

        int index = Mathf.Clamp(IndexOf(b.state) + 1,0,tileStates.Length - 1); //ensures index always stays in bounds with tilestates, if it exceeds it just uses the last state hence the clamp
        int number = b.number * 2; //infinite number possibility

        b.SetState(tileStates[index],number); // Not infinite tile states
    }

    private int IndexOf(TileState State) // checks current state of a tile
    {
        for (int i = 0; i < tileStates.Length; i++)
        {
            if(State == tileStates[i]){
                return i;
            }
        }
        return -1;
    }

    private IEnumerator WaitForChanges() //handles board state and analyzes changed after every move
    {
        waiting = true;

        yield return new WaitForSeconds(Tile.duration);

        waiting = false;

        if(tiles.Count != grid.size) // if available space
        {
        CreateTile();
        }
        //Check for game over

        foreach (var tile in tiles) //iterates through and unlocks all tiles during update step
        {
            tile.locked = false;
        }
    }
}
