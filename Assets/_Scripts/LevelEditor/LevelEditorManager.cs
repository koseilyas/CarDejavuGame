using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LevelEditor
{
    public class LevelEditorManager : MonoBehaviour
    {
        [SerializeField] private GameObject[] _cars,_flags,_obstacles;
        [SerializeField] private Transform _cloneObstaclesParent;
        [SerializeField] private Button _saveButton, _loadButton;
        [SerializeField] private TMP_Text _logText;

        private void Start()
        {
            SetupCars();
            SetupFlags();
            SetupObstacles();
            _saveButton.onClick.AddListener(SaveLevel);
            _loadButton.onClick.AddListener(LoadLevel);
            new LevelEditorLogger(_logText);
        }

        private void SetupCars()
        {
            foreach (var car in _cars)
            {
                car.AddComponent<DraggableObject>();
            }
        }

        private void SetupFlags()
        {
            foreach (var flag in _flags)
            {
                flag.AddComponent<DraggableObject>();
            }
        }
    
        private void SetupObstacles()
        {
            foreach (var obstacle in _obstacles)
            {
                CloneOnClick cloneOnClick = obstacle.AddComponent<CloneOnClick>();
                cloneOnClick.Initialize(obstacle,_cloneObstaclesParent);
            }
        }
        
        private void LoadLevel()
        {
            
        }

        private void SaveLevel()
        {
            LevelSaverLoader.Save();
        }
    }
}