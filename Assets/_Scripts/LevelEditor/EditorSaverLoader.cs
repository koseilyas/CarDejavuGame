#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace LevelEditor
{
    public static class EditorSaverLoader
    {
        private static string levelPath = "Assets/Levels/";
        public static void Save(GameObject[] cars, GameObject[] flags, Transform cloneObstaclesParent, int levelNum)
        {
            LevelData levelData = ScriptableObject.CreateInstance<LevelData>();
            string path = $"{levelPath}Level{levelNum}.asset";

            GameObject[] obstaclesArr = new GameObject[cloneObstaclesParent.childCount];
            for (int i = 0; i < cloneObstaclesParent.childCount; i++)
            {
                obstaclesArr[i] = cloneObstaclesParent.GetChild(i).gameObject;
            }
            
            List<TransformData> carsList = GetTransformDataListFromGameObjects(cars);
            List<TransformData> flagsList = GetTransformDataListFromGameObjects(flags);
            List<TransformData> obstaclesList = GetTransformDataListFromGameObjects(obstaclesArr);
            
            levelData.Set(carsList,flagsList,obstaclesList);

            AssetDatabase.CreateAsset(levelData, path);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = levelData;
        }

        public static void Load(int levelNum,EditorManager editorManager)
        {
            var levelData = AssetDatabase.LoadAssetAtPath<LevelData>($"{levelPath}Level{levelNum}.asset");
            for (var i = 0; i < levelData.cars.Count; i++)
            {
                var car = levelData.cars[i];
                editorManager.editorObjectLoader.SetCarPosition(car,i);
            }
            
            for (var i = 0; i < levelData.flags.Count; i++)
            {
                var flag = levelData.flags[i];
                editorManager.editorObjectLoader.SetFlagPosition(flag,i);
            }
        }

        private static List<TransformData> GetTransformDataListFromGameObjects(GameObject[] gameObjects)
        {
            List<TransformData> transformList = new List<TransformData>();
            foreach (var go in gameObjects)
            {
                TransformData transformData = new TransformData(go.transform.position, go.transform.rotation.eulerAngles);
                transformList.Add(transformData);
            }

            return transformList;
        }

        public static bool IsAllCarAndFlagsSet(GameObject[] cars, GameObject[] flags)
        {
            foreach (var car in cars)
            {
                if (Mathf.Abs(car.transform.position.x) > 6 || Mathf.Abs(car.transform.position.y) > 5)
                {
                    return false;
                }
            }
            foreach (var flag in flags)
            {
                if (Mathf.Abs(flag.transform.position.x) > 6 || Mathf.Abs(flag.transform.position.y) > 5)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
#endif