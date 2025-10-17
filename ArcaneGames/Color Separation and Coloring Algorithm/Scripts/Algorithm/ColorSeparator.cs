#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Painter))]
public partial class ColorSeparator : MonoBehaviour
{
    [Tooltip("Maximum number of groups to be created. The number of colors that differ in the picture.")]
    public int maxColors = 11;

    [Tooltip("Threshold value of separation between colors. Color Groups are arranged and created according to this tolerance value.")]
    [Range(0.01f,0.7f)] public float rgbTolerance = 0.17f;
    
    [Tooltip("The threshold value to use when creating the Background image.")]
    [Range(0.01f,1)] public float blackTolerance = 0.17f;

    private Sprite _targetSprite;
    private SpriteRenderer _spriteRenderer;
    private Painter _painter;
    private List<ColorGroup> _colorGroups = new();

    private void OnValidate()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _painter = GetComponent<Painter>();
    }
    
    public void Create()
    {
        _targetSprite = _spriteRenderer.sprite;
        
        ImageCreator.CreateWhiteImage(_targetSprite, blackTolerance, _spriteRenderer, _painter);
        CreateColorGroups();
        _painter.colorGroups = _colorGroups;
    }
    public void Clear()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            DestroyImmediate(transform.GetChild(i).gameObject, false);
        }
        
        _painter.colorGroups.Clear();
    }
}
#endif