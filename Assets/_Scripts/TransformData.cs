using System;
using UnityEngine;

[Serializable]
public class TransformData
{
    public Vector3 position, rotation;

    public TransformData(Vector3 pos, Vector3 rot)
    {
        position = pos;
        rotation = rot;
    }
}