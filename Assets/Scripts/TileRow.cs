using UnityEngine;

public class TileRow : MonoBehaviour
{
    //Array of cells
    public TileCell[] cells {get; private set; }

    //capitalize A, runs when function is first called
    private void Awake()
    {
        //searches for components on whatever game object this script is attatched to
        cells = GetComponent
    }       
}
