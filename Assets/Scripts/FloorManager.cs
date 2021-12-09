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

    [SerializeField]
    private Color[] colors;

    private GameObject[,] tiles;

    // Which row in each column is currently lit
    private int[] litTiles;

    // Which row in each column is currently activated
    private int[] markedTiles;

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

        // -1 means no tile in this column is marked/lit
        for (int i = 0; i < numCols; i++)
        {
            markedTiles[i] = -1;
            litTiles[i] = -1;
        }
    }

    public void MarkTile()
    {
        if (markedTiles[playerTile.col] != -1)
        {

        }

        markedTiles[playerTile.col] = playerTile.row;
        tiles[playerTile.col, playerTile.row].GetComponent<TileBehavior>().Mark();

    }

    public void ClearColumn()
    {
        if (markedTiles[playerTile.col] != -1)
        {
            tiles[playerTile.col, markedTiles[playerTile.col]].GetComponent<TileBehavior>().Unmark();
        }
    }

    public void MarchColumn(int col)
    {
        if (litTiles[col] == -1)
        {
            litTiles[col] = markedTiles[col];
        }
        else
        {
            int row = litTiles[col]++;
            if (row >= numRows) row = 0;
            litTiles[col] = row;

        }

        tiles[col, litTiles[col]].GetComponent<TileBehavior>().Brighten(colors[col]);
    }

    public void TriggerTiles()
    {
        foreach (Tile t in markedTiles)
        {
            OSCManager.instance.SendTrigger(t);
            tiles[t.col, t.row].GetComponent<TileBehavior>().Trigger();

        }

        markedTiles.Clear();
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
