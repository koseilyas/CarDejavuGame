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
            for (var i = 0; i < cars.Length; i++)
            {
                var car = cars[i];
                var flag = flags[i];
                flag.gameElementTransformation = new GameElementTransformation(flag.transform);
                car.gameElementTransformation = new GameElementTransformation(car.transform);
                car.SetReachPoint(flag);
                car.gameObject.SetActive(true);
                flag.gameObject.SetActive(true);
            }
        }
    }
}