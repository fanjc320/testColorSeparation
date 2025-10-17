using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ColorGroup
{
    public GameObject button;
    public bool selected;
    public SpriteRenderer spriteRenderer;
    public PolygonCollider2D collider;
    
    public List<Color> colors = new(); 
    public List<Vector2Int> positions = new();
    
    public void AddColor(Color color, Vector2Int position)
    {
        colors.Add(color);
        positions.Add(position);
    }
}