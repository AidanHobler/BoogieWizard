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
    private Vector2 center;
    private float raiseDistance;
    // Start is called before the first frame update
    void Start()
    {
        center = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Mark()
    {
        transform.position += new Vector3(0.0f, 0.3f, 0.0f);
    }

    public void Trigger()
    {
        transform.position = center;
    }

    public IEnumerator Raise()
    {
        // float newY = transform.position.y;
        // while (
        yield return null;

    }

}
