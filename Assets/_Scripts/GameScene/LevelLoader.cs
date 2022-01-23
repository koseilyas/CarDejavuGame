
using System;
using UnityEngine;

namespace GameScene
{
    public class LevelLoader
    {
        private ElementReferences _elementReferences;
        private string _path = "Levels/";

        public static event Action OnLevelLoaded;
    
        public LevelLoader(int level,ElementReferences elementReferences)
        {
            _elementReferences = elementReferences;

            var levelData = Resources.Load<LevelData>($"{_path}Level{level}");
            if (levelData == null)
            {
                levelData = Resources.Load<LevelData>($"{_path}Level{3}");
            }
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

            for (var i = 0; i < levelData.obstacles.Count; i++)
            {
                var obstacle = levelData.obstacles[i];
                _elementReferences.SpawnObstacleAtPoint(obstacle);
            }

            OnLevelLoaded?.Invoke();
        }
    }
}
