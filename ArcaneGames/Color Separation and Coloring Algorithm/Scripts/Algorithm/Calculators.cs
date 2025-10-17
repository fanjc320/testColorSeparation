using UnityEngine;

public static class Calculators
{
    /// <summary> Finds the color difference between two colors. </summary>
    public static float CalculateColorDistance(Color color1, Color color2)
    {
        var r = color1.r - color2.r;
        var g = color1.g - color2.g;
        var b = color1.b - color2.b;

        return Mathf.Sqrt(r * r + g * g + b * b);
    }

    /// <summary> Returns false when it finds a black pixel. </summary>
    public static bool IsBlackPixel(Color pixelColor, float blackTolerance)
    {
        return pixelColor.grayscale <= blackTolerance;
    }
    
    /// <summary> Calculates the mouse position on the screen. </summary>
    public static Vector3 CalculateMousePosition(Camera camera)
    {
        Vector3 mousePosition = camera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        
        return mousePosition;
    }
    
    /// <summary> Returns true when the mouse hovers over the <b>selectedGroup</b>. </summary>
    public static bool CheckMousePosition(Camera camera, ColorGroup selectedGroup)
    {
        RaycastHit2D hit = Physics2D.Raycast(camera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        
        return hit.collider == selectedGroup.collider;
    }
}
