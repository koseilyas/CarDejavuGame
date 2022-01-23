
using System;
using UnityEngine;

namespace GameScene
{
    public class LevelLoader
    {
        private int _level;
        private ElementReferences _elementReferences;
        private string _path = "Levels/";

        public static event Action OnLevelLoaded;
    
        public LevelLoader(int level,ElementReferences elementReferences)
        {
            _level = level;
            _elementReferences = elementReferences;

            var levelData = Resources.Load<LevelData>($"{_path}Level{level}");
            LoadLevel(levelData);
        }

        private void LoadLevel(LevelData levelData)
        {
            for (var i = 0; i < levelData.cars.Count; i++)
            {
                _elementReferences.cars[i].gameElementTransformation.playingStartTransformData = levelData.cars[i];
            }
            
            for (var i = 0; i < levelData.flags.Count; i++)
            {
                _elementReferences.flags[i].gameElementTransformation.playingStartTransformData = levelData.flags[i];
            }
            
            OnLevelLoaded?.Invoke();
        }
    }
}
