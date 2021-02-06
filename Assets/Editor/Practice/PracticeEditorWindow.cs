using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class PracticeEditorWindow : EditorWindow
{
    private Material[] _allMaterials;
    private Texture2D[] _thumbnails;

    private Vector2 _scrollPosition;

    private const int buttonSize = 150;
    
    //[MenuItem("Window/Material Lister")]
    public static void ShowWindow()
    {
        var window = GetWindow<PracticeEditorWindow>();
        window.Refresh();
        window.Show();
    }

    private void Refresh()
    {
        titleContent = new GUIContent("Materials", AssetPreview.GetMiniTypeThumbnail(typeof(Material)));
        string[] guids = AssetDatabase.FindAssets($"t:{nameof(Material)}");
        _allMaterials = guids
            .Select(guid => AssetDatabase.LoadAssetAtPath<Material>(AssetDatabase.GUIDToAssetPath(guid))).ToArray();
        _thumbnails = _allMaterials.Select(AssetPreview.GetAssetPreview).ToArray();
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Refresh")) Refresh();
        
        _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition);

        int i = 0;
        int buttonsPerLine = Mathf.FloorToInt(position.width / buttonSize);
        buttonsPerLine = Mathf.Max(buttonsPerLine, 1);
        while (i < _allMaterials.Length)
        {
            EditorGUILayout.BeginHorizontal();
            for (int j = 0; j < buttonsPerLine && i < _allMaterials.Length; j++)
            {
                MaterialButton(i++);
            }
            EditorGUILayout.EndHorizontal();
        }
        
        EditorGUILayout.EndScrollView();
    }

    private void MaterialButton(int i)
    {
        if (GUILayout.Button(_thumbnails[i], GUILayout.Height(buttonSize)))
        {
            Selection.activeObject = _allMaterials[i];
        }
    }
}
