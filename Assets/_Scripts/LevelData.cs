using System.Collections.Generic;
using UnityEngine;

public class LevelData : ScriptableObject
{
    public List<TransformData> cars, flags, obstacles;

    public void Set(List<TransformData> carList,List<TransformData> flagList,List<TransformData> obstacleList)
    {
        cars = carList;
        flags = flagList;
        obstacles = obstacleList;
    }
}