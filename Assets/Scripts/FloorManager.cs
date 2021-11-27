using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorManager : MonoBehaviour
{

    [SerializeField]
    private GameObject floorPiece;

    [SerializeField]
    private Vector2 centerPosition;

    [SerializeField]
    private int numRows;

    [SerializeField]
    private int numCols;


    private GameObject[,] tiles;

    // Start is called before the first frame update
    void Start()
    {
        tiles = new GameObject[numRows, numCols];

        for (int i = 0; i < numRows; i++)
        {
            for (int j = 0; j < numCols; j++)
            {
                Vector2 v = centerPosition + new Vector2(i, j);
                tiles[i, j] = Instantiate(floorPiece, v, Quaternion.identity);
            }
        }
         
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
