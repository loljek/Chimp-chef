using UnityEngine;

namespace InteractableObjects
{
    [RequireComponent(typeof(Rigidbody))]

    public abstract class PickingObject : InteractableObject
    {
        public bool isInHand;
        public Rigidbody rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        public void InHandChange()
        {
            isInHand = !isInHand;
            rb.isKinematic = isInHand;
            ChangeCollider(transform, !isInHand);
        }

        public override void Interaction()
        {
            InHandInteraction();
        } 
    
        public abstract void InHandInteraction();
    }
}