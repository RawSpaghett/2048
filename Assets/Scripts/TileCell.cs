using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Cannot determine its own coordinates, must be fed this information
public class TileCell : MonoBehaviour
{
    //Vector 2 means 2 axis, X and Y
    public Vector2Int coordinates { get; set;}
    public Tile Tile {get; set;}
    
    //Checks to see if tile is empty or occupied
    public bool empty => Tile == null;
    public bool occupied => Tile != null;

}
