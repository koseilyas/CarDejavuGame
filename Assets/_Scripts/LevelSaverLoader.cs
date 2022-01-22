using UnityEditor;
using UnityEngine;

public static class LevelSaverLoader
{
    public static void Save()
    {
        LevelData example = ScriptableObject.CreateInstance<LevelData>();
        // path has to start at "Assets"
        string path = "Assets/Levels/filename.asset";
        AssetDatabase.CreateAsset(example, path);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = example;
    }
}