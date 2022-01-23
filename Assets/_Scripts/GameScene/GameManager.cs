using System;
using UnityEngine;

namespace GameScene
{
    public class GameManager : MonoBehaviour
    {
        private LevelLoader _levelLoader;
        public ElementReferences elementReferences;

        public static GameManager Instance;

        private void Awake()
        {
            Instance = this;
            elementReferences.InitTransforms();
            _levelLoader = new LevelLoader(1,elementReferences);
        }
    }
}