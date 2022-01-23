#if UNITY_EDITOR
using GameScene;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LevelEditor
{
    public class EditorManager : MonoBehaviour
    {
        [SerializeField] private GameObject[] _cars,_flags,_obstacles;
        [SerializeField] private Transform _cloneObstaclesParent;
        [SerializeField] private Button _saveButton, _loadButton;
        [SerializeField] private TMP_Text _logText;
        [SerializeField] private TMP_InputField _levelInputField;
        public EditorObjectLoader editorObjectLoader;

        private void Start()
        {
            SetupCars();
            SetupFlags();
            SetupObstacles();
            _saveButton.onClick.AddListener(SaveLevel);
            _loadButton.onClick.AddListener(LoadLevel);
            new EditorLogger(_logText);
            editorObjectLoader = new EditorObjectLoader(_cars,_flags);
        }

        private void SetupCars()
        {
            foreach (var car in _cars)
            {
                if (car.TryGetComponent(out Car _car))
                {
                    Destroy(_car);
                }
                car.AddComponent<DraggableObject>();
            }
        }

        private void SetupFlags()
        {
            foreach (var flag in _flags)
            {
                if (flag.TryGetComponent(out Flag _flag))
                {
                    Destroy(_flag);
                }
                flag.AddComponent<DraggableObject>();
            }
        }
    
        private void SetupObstacles()
        {
            foreach (var obstacle in _obstacles)
            {
                if (obstacle.TryGetComponent(out Obstacle _obstacle))
                {
                    Destroy(_obstacle);
                }
                CloneOnClick cloneOnClick = obstacle.AddComponent<CloneOnClick>();
                cloneOnClick.Initialize(obstacle,_cloneObstaclesParent);
            }
        }
        
        private void LoadLevel()
        {
            int levelNum  = GetLevelInput();
            if (levelNum > 0)
            {
                EditorSaverLoader.Load(levelNum,this);
            }
            else
            {
                EditorLogger.Log("Input valid levelNumber",true);
            }
            
        }

        private void SaveLevel()
        {
            int levelNum  = GetLevelInput();
            if (!EditorSaverLoader.IsAllCarAndFlagsSet(_cars, _flags))
            {
                EditorLogger.Log("Set all cars and flags",true);
                return;
            }
            if(levelNum > 0)
            {
                EditorSaverLoader.Save(_cars,_flags,_cloneObstaclesParent,levelNum);
            }
            else
            {
                EditorLogger.Log("Input valid levelNumber",true);
            }
        }

        private int GetLevelInput()
        {
            if (int.TryParse(_levelInputField.text, out int levelNum) && levelNum > 0)
            {
                return levelNum;
            }
            
            return 0;
        }
    }
}
#endif