using System.Collections.Generic;
using UnityEngine;

public class SpriteCreator: MonoBehaviour
{
    /// <summary> Creates sprites based on the number of color groups. </summary>
    public static void CreateSprites(List<ColorGroup> colorGroups, Sprite targetSprite,  Transform parent)
    {
        for (int i = 0; i < colorGroups.Count; i++)
        {
            var generatedSprite = CreateSprite(colorGroups[i], targetSprite);
            var newGameObject = new GameObject("Group_" + i);

            var spriteRenderer = newGameObject.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = generatedSprite;
            spriteRenderer.sortingOrder = i + 1;
            spriteRenderer.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
            
            var polygonCol = newGameObject.AddComponent<PolygonCollider2D>();

            newGameObject.transform.SetParent(parent);
            newGameObject.transform.localPosition = Vector3.zero;
            newGameObject.transform.localScale = Vector3.one;
            newGameObject.SetActive(false);
            
            colorGroups[i].spriteRenderer = spriteRenderer;
            colorGroups[i].collider = polygonCol;
        }
    }  
    
    /// <summary> Creates sprite by taking colors from the color group. </summary>
    private static Sprite CreateSprite(ColorGroup colorGroups, Sprite targetSprite)
    {
        var rect = targetSprite.textureRect;
        var width = (int)rect.width;
        var height = (int)rect.height;

        var colors = colorGroups.colors.ToArray();
        var positions = colorGroups.positions.ToArray();

        var generatedTexture = ImageCreator.CreateTransparentImage(targetSprite);

        for (var i = 0; i < colors.Length; i++)
        {
            generatedTexture.SetPixel(positions[i].x, positions[i].y, colors[i]);
        }

        generatedTexture.Apply();

        var newPivot = new Vector2(0.5f, 0.5f);

        return Sprite.Create(generatedTexture, new Rect(0, 0, width, height), newPivot, targetSprite.pixelsPerUnit);
    }
}
