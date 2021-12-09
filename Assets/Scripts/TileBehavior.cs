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
    private Material material;
    private bool active;

    [SerializeField]
    private Color activeColor;

    // Start is called before the first frame update
    void Start()
    {
        center = transform.position;
        material = GetComponent<SpriteRenderer>().material;
        Dim();
    }

    public void Mark()
    {
        transform.position += new Vector3(0.0f, 0.3f, 0.0f);
    }

    public void Activate()
    {
        active = true; 
        Vector4 hdr = new Vector4(activeColor.r, activeColor.g, activeColor.b, 1.0f) * 6;
        material.SetVector("_GlowColor", hdr);
    }

    public void Deactivate()
    {
        active = false;
        Trigger();
        Dim();
    }

    public void Trigger()
    {
        transform.position = center;
    }

    public void Brighten(Color color)
    {
        if (!active)
        {
            Vector4 hdr = new Vector4(color.r, color.g, color.b, 1.0f) * 6;
            material.SetVector("_GlowColor", hdr);
        }
    }

    public void Dim()
    {
        if (!active)
        {
            Vector4 hdr = new Vector4(0, 0, 0, 0);
            material.SetVector("_GlowColor", hdr);
        }
    }

}
