using System.Collections.Generic;
using System.Linq;

namespace GameScene
{
    public class CarRecordData
    {
        private List<TransformData> _transformSet = new List<TransformData>();
        private int _index = -1;

        public void AddRecord(TransformData transformData)
        {
            _transformSet.Add(transformData);
        }

        public void Reset()
        {
            _index = 0;
        }

        public void Restart()
        {
            _transformSet.Clear();
            _index = 0;
        }

        public TransformData GetNextTransform()
        {
            _index++;
            if(_index < _transformSet.Count)
                return _transformSet[_index];
            return null;
        }
    }
}