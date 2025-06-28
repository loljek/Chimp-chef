using UnityEngine;

namespace InteractableObjects
{
    public class Grid : MonoBehaviour
    {
        [SerializeField] private int columns;
        [SerializeField] private int rows;
        [SerializeField] private float columnsDistance;
        [SerializeField] private float rowsDistance;
        [SerializeField] private Place prefab;
        private Vector3 position;

        private void Awake()
        {
            if (columnsDistance == 0f & rowsDistance == 0f)
            {
                columnsDistance = prefab.GetComponent<BoxCollider>().size.z;
                rowsDistance = prefab.GetComponent<BoxCollider>().size.x;
            }

            position = transform.position;
            for (int i = 0; i < columns; i++)
            {
                position.z += columnsDistance;
                for (int j = 0; j < rows; j++)
                {
                    Place place = Instantiate(prefab, position, new Quaternion(0, 0, 0, 0));
                    place.transform.parent = transform;
                    position.x += rowsDistance;
                }
                position.x += -rowsDistance * rows;
            }
        }
    }
}
