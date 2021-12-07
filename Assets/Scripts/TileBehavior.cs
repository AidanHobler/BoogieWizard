using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public struct Tile : IEquatable<Tile>
{
    public int row;
    public int col;

    public Tile(int col, int row)
    {
        this.col = col;
        this.row = row;
    }

    public Boolean Equals(Tile tile)
    {
        return (row == tile.row && col == tile.col);
    }

}

public class TileBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
