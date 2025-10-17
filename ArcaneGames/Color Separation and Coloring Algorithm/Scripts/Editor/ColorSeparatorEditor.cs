using System;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ColorSeparator))]
public class ColorSeparatorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        
        ColorSeparator colorSeparator = (ColorSeparator)target;

        if (GUILayout.Button(new GUIContent("Calculate And Create", "It makes calculations according to the given values, creates buttons and sprites, and assigns the necessary values to >Painter<")))
        {
            colorSeparator.Create();
        }

        if (GUILayout.Button(new GUIContent("Clear", "Deletes created Color groups and Sprites.")))
        {
            for (int i = 0; i < 5; i++)
            {
                colorSeparator.Clear();
            }
        }
    }
}
