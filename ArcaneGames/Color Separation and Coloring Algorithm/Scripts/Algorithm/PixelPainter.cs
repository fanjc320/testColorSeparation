using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public partial class ColorSeparator
{
    /*
    private void PaintPixels()
    {
        if (!_selectedGroup.selected || !_selectedGroup.positions.Contains(Calculators.CalculatePixelPosition(_mainCam, targetSprite, transform))) return;
        
        var colorPositions = _selectedGroup.positions;
        var colors = _selectedGroup.colors;
        
        for (var j = 0; j < colorPositions.Count; j++)
        {
            var colorIndex = j % colors.Count;
            var color = colors[colorIndex];
            var pos = colorPositions[j];

            _generatedTexture.SetPixel(pos.x, pos.y, color);
        }
        _generatedTexture.Apply();   
        Destroy(_selectedGroup.button);
        _selectedGroup.selected = false;
    }

    private void PaintSprite()
    {
        if (!_selectedGroup.selected || !Calculators.CheckMousePosition(_mainCam, _selectedGroup)) return;
        var mouseInPixelPos = Calculators.CalculateMousePosition(_mainCam);

        var maskObj = Instantiate(maskPrefab);
        maskObj.transform.position = mouseInPixelPos;

        maskObj.transform.DOScale(new Vector3(75, 75, 0.3f), 2).OnComplete(() =>
        {
            var colorPositions = _selectedGroup.positions;
            var colors = _selectedGroup.colors;
        
            for (var j = 0; j < colorPositions.Count; j++)
            {
                var colorIndex = j % colors.Count;
                var color = colors[colorIndex];
                var pos = colorPositions[j];

                _generatedTexture.SetPixel(pos.x, pos.y, color);
            }
            _generatedTexture.Apply();
            
            _selectedGroup.selected = false;
            Destroy(_selectedGroup.spriteRenderer.gameObject);
            Destroy(maskObj);
            Destroy(_selectedGroup.button);
        });
    }
    */
}
