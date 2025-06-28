using UnityEngine;

namespace InteractableObjects
{
    public class Place : MonoBehaviour
    {
        public bool isOccupied;
        public Vector3 placePos;
        
        private void Awake()
        {
            placePos = transform.position;
        }

        public void ChangeIsOccupied()
        {
            isOccupied = !isOccupied;
        }
    
        public Vector3 GetPos()
        {
            return placePos;
        }
    }
}
