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

    public static FloorManager instance;

    private Tile playerTile;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
            return;
        }

        tiles = new GameObject[numCols, numRows];

        for (int i = 0; i < numCols; i++)
        {
            for (int j = 0; j < numRows; j++)
            {
                Vector2 v = centerPosition + new Vector2(2 * i, 2 * j);
                tiles[i, j] = Instantiate(floorPiece, v, Quaternion.identity);
            }
        }

    }

    public GameObject GetTileForMove(Direction direction)
    {
        Tile result = new Tile();

        switch (direction)
        {
            case Direction.Up:
                result.col = playerTile.col;
                result.row = playerTile.row + 1;
                break;
            case Direction.Down:
                result.col = playerTile.col;
                result.row = playerTile.row - 1;
                break;
            case Direction.Left:
                result.col = playerTile.col - 1;
                result.row = playerTile.row;
                break;
            case Direction.Right:
                result.col = playerTile.col + 1;
                result.row = playerTile.row;
                break;
        }

        if (result.col < 0 || result.col >= numCols)
        {
            result = playerTile;
        }

        if (result.row < 0 || result.row >= numRows)
        {
            result = playerTile;
        }

        playerTile = result;

        return tiles[result.col, result.row];
    }

    // Update is called once per frame
    void Update()
    {

    }
}
