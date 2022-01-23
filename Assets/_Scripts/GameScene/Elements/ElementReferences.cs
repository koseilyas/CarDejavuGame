using System;
using UnityEngine;

namespace GameScene
{
    public class ElementReferences : MonoBehaviour
    {
        public Car[] cars;
        public Flag[] flags;
        public Obstacle[] obstacles;
        [SerializeField] private GameObject treePrefab, barrelPrefab;

        public void InitTransforms()
        {
            foreach (var car in cars)
            {
                car.gameElementTransformation = new GameElementTransformation(car.transform);
            }
            foreach (var flag in flags)
            {
                flag.gameElementTransformation = new GameElementTransformation(flag.transform);
            }
        }
    }
}