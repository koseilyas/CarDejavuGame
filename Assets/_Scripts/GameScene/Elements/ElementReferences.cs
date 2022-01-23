using System.Collections.Generic;
using UnityEngine;

namespace GameScene
{
    public class ElementReferences : MonoBehaviour
    {
        public Car[] cars;
        public Flag[] flags;
        public List<Obstacle> obstacles;
        [SerializeField] private Obstacle barrelPrefab;
        [SerializeField] private Transform _cloneObstaclesParent;

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

        public void SpawnObstacleAtPoint(TransformData transformData)
        {
            var obstacle = Instantiate(barrelPrefab, _cloneObstaclesParent);
            obstacle.transform.position = transformData.position;
            obstacle.transform.rotation = Quaternion.Euler(transformData.rotation);
            obstacle.gameElementTransformation = new GameElementTransformation(obstacle.transform);
            obstacles.Add(obstacle);
        }
    }
}