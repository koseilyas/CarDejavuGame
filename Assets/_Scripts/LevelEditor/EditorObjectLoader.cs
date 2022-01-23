using UnityEngine;

namespace LevelEditor
{
    public class EditorObjectLoader
    {
        private GameObject[] _cars;
        private GameObject[] _flags;
        private GameObject[] _obstacles;

        public EditorObjectLoader(GameObject[] cars, GameObject[] flags, GameObject[] obstacles)
        {
            _cars = cars;
            _flags = flags;
            _obstacles = obstacles;
        }

        public void SetCarPosition(TransformData transformData, int index)
        {
            _cars[index].transform.position = transformData.position;
            _cars[index].transform.rotation = Quaternion.Euler(transformData.rotation);
        }
        
        public void SetFlagPosition(TransformData transformData, int index)
        {
            _flags[index].transform.position = transformData.position;
            _flags[index].transform.rotation = Quaternion.Euler(transformData.rotation);
        }
    }
    
    
}