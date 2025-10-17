using System.Linq;
using UnityEngine;

public partial class ColorSeparator
{
    /// <summary> It creates color groups by making all the necessary calculations. </summary>
    private void CreateColorGroups()
    {
        var texture = _targetSprite.texture;
        var rect = _targetSprite.textureRect;
        var pixels = texture.GetPixels((int)rect.x, (int)rect.y, (int)rect.width, (int)rect.height);

        for (var i = 0; i < pixels.Length; i++)
        {
            if (Calculators.IsBlackPixel(pixels[i], blackTolerance)) continue;

            var position = new Vector2Int(i % (int)rect.width, i / (int)rect.width);

            var existingGroup = _colorGroups.Find(group => Calculators.CalculateColorDistance(group.colors[0], pixels[i]) <= rgbTolerance);

            if (existingGroup != null)
            {
                existingGroup.AddColor(pixels[i], position);
            }
            else
            {
                var newGroup = new ColorGroup();
                newGroup.AddColor(pixels[i], position);
                _colorGroups.Add(newGroup);
            }
        }

        // Color groups are sorted by number of positions in descending order
        _colorGroups = _colorGroups.OrderByDescending(group => group.positions.Count).ToList();
        
        if (_colorGroups.Count >= maxColors)
        {
            // Select top N groups where N is the value of maxColors
            var selectedGroups = _colorGroups.Take(maxColors).ToList();

            // Assign remaining colors to the closest group
            foreach (var group in _colorGroups.Skip(maxColors))
            {
                for (var i = 0; i < group.colors.Count; i++)
                {
                    var color = group.colors[i];
                    var position = group.positions[i];

                    var closestGroup = selectedGroups[0];
                    var closestDistance = float.MaxValue;

                    foreach (var currentGroup in selectedGroups)
                    {
                        var currentDistance = Calculators.CalculateColorDistance(color, currentGroup.colors[0]);

                        if (currentDistance < closestDistance)
                        {
                            closestGroup = currentGroup;
                            closestDistance = currentDistance;
                        }
                    }

                    closestGroup.colors.Add(color);
                    closestGroup.positions.Add(position);
                }
            }

            // Remove extra groups from the list
            _colorGroups.RemoveRange(maxColors, _colorGroups.Count - maxColors);
        }

        SpriteCreator.CreateSprites(_colorGroups, _targetSprite, transform);
    }
}