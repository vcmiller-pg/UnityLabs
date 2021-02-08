using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Build.Content;
using UnityEngine;

public class MaterialListerWindow : EditorWindow
{
    private Material[] _materials;
    private Texture2D[] _thumbnails;

    private Vector2 _scrollPos;
    
    private const int ButtonSize = 150;
    
    [MenuItem("Window/Material Lister")]
    public static void OpenWindow()
    {
        MaterialListerWindow window = GetWindow<MaterialListerWindow>();
        window.titleContent = new GUIContent("Materials");
        window.Refresh();
        window.Show();
    }

    private void Refresh()
    {
        string[] guids = AssetDatabase.FindAssets("t:Material");
        _materials = guids.Select(g => AssetDatabase.LoadAssetAtPath<Material>(AssetDatabase.GUIDToAssetPath(g)))
            .ToArray();
        _thumbnails = _materials.Select(m => AssetPreview.GetAssetPreview(m)).ToArray();
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Refresh"))
        {
            Refresh();
        }
        _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos);

        int i = 0;
        int materialsPerLine = Mathf.FloorToInt(position.width / ButtonSize);
        materialsPerLine = Mathf.Max(materialsPerLine, 1);
        
        while (i < _materials.Length)
        {
            EditorGUILayout.BeginHorizontal();
            for (int j = 0; j < materialsPerLine && i < _materials.Length; j++)
            {
                DrawButton(i++);
            }
            EditorGUILayout.EndHorizontal();
        }
        
        EditorGUILayout.EndScrollView();
    }

    private void DrawButton(int i)
    {
        if (GUILayout.Button(_thumbnails[i], GUILayout.Height(ButtonSize)))
        {
            Selection.activeObject = _materials[i];
        }
    }
}
