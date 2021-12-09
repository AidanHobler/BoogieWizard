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
    public int numRows;

    [SerializeField]
    public int numCols;

    [SerializeField]
    private Color[] colors;

    private GameObject[,] tiles;

    // Which row in each column is currently lit
    private int[] litTiles;

    // Which row in each column is currently marked 
    private int[] markedTiles;

    // Which row in each column is currently playing 
    private int[] activeTiles;

    public static FloorManager instance;

    public Tile playerTile;

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
                tiles[i, j].GetComponent<SpriteRenderer>().sortingOrder = -1 * j;
            }
        }

        markedTiles = new int[numCols];
        litTiles = new int[numCols];
        activeTiles = new int[numCols];

        // -1 means no tile in this column is marked/lit
        for (int i = 0; i < numCols; i++)
        {
            markedTiles[i] = -1;
            litTiles[i] = 0;
            activeTiles[i] = -1;
        }
    }

    public void MarkTile()
    {
        if (markedTiles[playerTile.col] != -1)
        {
            tiles[playerTile.col, markedTiles[playerTile.col]].GetComponent<TileBehavior>().Trigger();
        }

        markedTiles[playerTile.col] = playerTile.row;
        tiles[playerTile.col, playerTile.row].GetComponent<TileBehavior>().Mark();

    }

    public void ClearColumn()
    {
        if (markedTiles[playerTile.col] != -1)
        {
            tiles[playerTile.col, markedTiles[playerTile.col]].GetComponent<TileBehavior>().Trigger();
        }
        if (litTiles[playerTile.col] != -1)
        {
            tiles[playerTile.col, litTiles[playerTile.col]].GetComponent<TileBehavior>().Dim();
        }
        if (activeTiles[playerTile.col] != -1)
        {
            OSCManager.instance.SendClear(playerTile.col);
            tiles[playerTile.col, activeTiles[playerTile.col]].GetComponent<TileBehavior>().Deactivate();
        }

        markedTiles[playerTile.col] = -1;
        litTiles[playerTile.col] = -1;
        activeTiles[playerTile.col] = -1;
    }

    public void MarchColumn(int col)
    {
        // litTiles[col] += 1;
        // if (litTiles[col] == numRows)
        if (activeTiles[col] != -1)
        {
            if (litTiles[col] == -1)
            {
                litTiles[col] = activeTiles[col];
                // tiles[col, activeTiles[col]].GetComponent<TileBehavior>().Activate();
            }
            int row = litTiles[col];
            row += 1;
            if (row >= numRows) row = 0;
            if (row == activeTiles[col]) row += 1;
            if (row >= numRows) row = 0;
            

            tiles[col, litTiles[col]].GetComponent<TileBehavior>().Dim();
            litTiles[col] = row;



            tiles[col, litTiles[col]].GetComponent<TileBehavior>().Brighten(colors[col]);
        }
    }

    public void TriggerTiles()
    {
        for (int i = 0; i < numCols; i++)
        {
            if (markedTiles[i] != -1)
            {
                OSCManager.instance.SendTrigger(new Tile(i, markedTiles[i]));
                tiles[i, markedTiles[i]].GetComponent<TileBehavior>().Trigger();

                if (activeTiles[i] != -1)
                {
                    tiles[i, activeTiles[i]].GetComponent<TileBehavior>().Deactivate();
                }

                if (litTiles[i] != -1)
                {
                    tiles[i, litTiles[i]].GetComponent<TileBehavior>().Deactivate();
                }

                activeTiles[i] = markedTiles[i];
                litTiles[i] = -1;
                tiles[i, activeTiles[i]].GetComponent<TileBehavior>().Activate();

            }

            // Reset this tile
            markedTiles[i] = -1;

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

        // Wrap around
        if (result.col < 0)
        {
            result.col = numCols - 1;
        }
        if (result.col >= numCols)
        {
            result.col = 0;
        }
        if (result.row < 0)
        {
            result.row = numRows - 1;
        }
        if (result.row >= numRows)
        {
            result.row = 0;
        }

        playerTile = result;

        return tiles[result.col, result.row];
    }

    // Update is called once per frame
    void Update()
    {

    }
}
