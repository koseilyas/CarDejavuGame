using System;
using UnityEngine;

namespace GameScene
{
    [Serializable]
    public class GameElementTransformation
    {
        private Transform _transform;
        public TransformData idleTransformData;
        public TransformData playingStartTransformData;

        public GameElementTransformation(Transform transform)
        {
            idleTransformData = new TransformData(transform.position, transform.rotation.eulerAngles);
            _transform = transform;
        }

        public void SetPositionAndRotation(TransformData transformData)
        {
            _transform.position = transformData.position;
            _transform.rotation = Quaternion.Euler(transformData.rotation);
        }

        public void SetIdlePositionAndRotation(TransformData transformData)
        {
            idleTransformData = transformData;
            SetPositionAndRotation(idleTransformData);
        }

        public void SetPlayingPositionAndRotation(TransformData transformData)
        {
            playingStartTransformData = transformData;
            SetPositionAndRotation(playingStartTransformData);
        }
    }
    
}