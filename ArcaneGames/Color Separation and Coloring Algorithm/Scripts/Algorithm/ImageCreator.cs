using UnityEngine;

public static class ImageCreator
{
    /// <summary> Removes other colors and creates a white image. </summary>
    public static void CreateWhiteImage(Sprite targetSprite, float blackTolerance, SpriteRenderer spriteRenderer, Painter painter)
    {
        var texture = targetSprite.texture;
        var rect = targetSprite.textureRect;
        var width = (int)rect.width;
        var height = (int)rect.height;

        var generatedTexture = new Texture2D(width, height, TextureFormat.RGBA4444, false, true);

        var colors = texture.GetPixels((int)rect.x, (int)rect.y, width, height);
        for (var i = 0; i < colors.Length; i++)
        {
            if (!Calculators.IsBlackPixel(colors[i], blackTolerance))
            {
                colors[i] = Color.white;
            }
        }

        generatedTexture.SetPixels(colors);
        generatedTexture.Apply();

        var newPivot = new Vector2(0.5f, 0.5f);

        var generatedSprite = Sprite.Create(generatedTexture, new Rect(0, 0, width, height), newPivot, targetSprite.pixelsPerUnit);

        spriteRenderer.sprite = generatedSprite;
    }
    
    /// <summary> Creates a transparent copy of the <b>targetSprite</b>. </summary>
    public static Texture2D CreateTransparentImage(Sprite targetSprite)
    {
        var texture = targetSprite.texture;
        var rect = targetSprite.textureRect;
        var width = (int)rect.width;
        var height = (int)rect.height;

        var generatedTexture = new Texture2D(width, height, TextureFormat.RGBA4444, false, true);

        var colors = texture.GetPixels((int)rect.x, (int)rect.y, width, height);
        
        for (var i = 0; i < colors.Length; i++)
        {
            colors[i] = Color.clear;
        }

        generatedTexture.SetPixels(colors);
        generatedTexture.Apply();

        return generatedTexture;
    }
}