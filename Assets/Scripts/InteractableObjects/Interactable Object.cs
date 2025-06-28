using UnityEngine;

namespace InteractableObjects
{
    public abstract class InteractableObject : MonoBehaviour
    {
        [SerializeField] public BoxCollider interactSpace;

        protected InteractableObject(BoxCollider interactSpace)
        {
            this.interactSpace = interactSpace;
        }

        protected InteractableObject()
        {
            interactSpace = null;
        }
        
        protected static void ChangeCollider(Transform root, bool f)
        {
            Collider col = root.GetComponent<Collider>();
            if (col != null)
            {
                col.enabled = f;
            }

            foreach (Transform child in root)
            {
                ChangeCollider(child, f);
            }
        }

        public abstract void Interaction();
    }
}
