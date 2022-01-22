using System.Collections.Generic;
using UnityEngine;

public class LevelData : ScriptableObject
{
    [SerializeField] private List<TransformData> _cars, _flags, _obstacles;
}