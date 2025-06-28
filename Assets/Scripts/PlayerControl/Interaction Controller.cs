using InteractableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace PlayerControl
{
    public class InteractionController : MonoBehaviour
    {
        [SerializeField] private Transform replaceSpace;
        [SerializeField] private GameObject handModel;
        [SerializeField] private Transform handPos;
        [SerializeField] private RawImage stillAim;
        [SerializeField] private RawImage activeAim;
        private bool isActive;
        private bool isReplacing;
        public PickingObject pickingObject;
        public PickingObject hand;
        public InteractableObject interactableObject;
        public PlacementObject placementObject;

        private void Awake()
        {
            activeAim.enabled = false;
            handModel.SetActive(false);
        }

        private void Update()
        {
            if (hand != null)
            {
                hand.transform.position = handPos.position;
                hand.transform.rotation = handPos.rotation;
            }
        }

        public void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Interactable"))
            {
                ActiveOn();
                interactableObject = other.GetComponent<InteractableObject>();
            }

            if (other.CompareTag("PickingInteractable") & hand == null)
            {
                ActiveOn();
                pickingObject = other.GetComponent<PickingObject>();
            }

            if (other.CompareTag("PlacementInteractable") & !isReplacing)
            {
                ActiveOn();
                placementObject = other.GetComponent<PlacementObject>();
            }

            if (isReplacing & other.CompareTag("Place"))
            {
                placementObject.SetCurPlace(other.GetComponent<Place>());
            }
        }

        public void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Interactable"))
            {
                ActiveOn();
                interactableObject = other.GetComponent<InteractableObject>();
            }

            if (other.CompareTag("PickingInteractable") & hand == null)
            {
                ActiveOn();
                pickingObject = other.GetComponent<PickingObject>();
            }

            if (other.CompareTag("PlacementInteractable") & !isReplacing)
            {
                ActiveOn();
                placementObject = other.GetComponent<PlacementObject>();
            }

            if (isReplacing & other.CompareTag("Place"))
            {
                placementObject.SetCurPlace(other.GetComponent<Place>());
            }
        }

        public void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Interactable"))
            {
                ActiveOff();
                interactableObject = null;
            }

            if (other.CompareTag("PickingInteractable"))
            {
                ActiveOff();
                pickingObject = null;
            }

            if (other.CompareTag("PlacementInteractable") & !isReplacing)
            {
                ActiveOff();
                placementObject = null;
            }

            if (isReplacing & other.CompareTag("Place"))
            {
                placementObject?.SetCurPlace(null);
            }
        }

        public void InteractActivation()
        {
            if ((isActive) & (pickingObject != null & hand == null))
            {
                ActiveOff();
                pickingObject.InHandChange();
                pickingObject.transform.position = handPos.position;
                hand = pickingObject; 
                InHandChange();
                pickingObject = null;
            }

            else if (isActive & interactableObject != null)
            {
                interactableObject.Interaction();
            }

            else if (isActive & placementObject != null & !isReplacing)
            {
                placementObject.Interaction();
            }
        }

        public void InHandInteractActivation()
        {
            hand?.InHandInteraction();
        }

        private void InHandChange()
        {
            handModel.SetActive(hand != null);
        }
        
        public void Drop()
        {
            if (hand != null)
            {
                hand.InHandChange();
                hand = null;
                InHandChange();
            }
        }
        
        public void ReplaceActivation()
        {
            if (placementObject != null)
            {
                if (placementObject.isReady)
                {
                    placementObject.ReplaceEnd();
                    placementObject = null;
                    isReplacing = false;
                }
                else
                {
                    isReplacing = placementObject.isReplacing;
                    if (isReplacing)
                    {
                        placementObject.ReplaceCancel();
                        placementObject = null;
                        isReplacing = false;
                        ActiveOff();
                    }
                    else
                    {
                        ActiveOff();
                        placementObject.ReplaceStart(replaceSpace);
                        isReplacing = true;
                    }
                }
            }
        }
        
        public void ReplaceRotateActivation()
        {
            if (placementObject  != null)
            {
                placementObject.Rotate();
            }
        }

        private void ActiveOn()
        {
            isActive = true;
            ChangeAim("active");
        }
        
        private void ActiveOff()
        {
            isActive = false;
            ChangeAim("still");
        }
        
        private void ChangeAim(string aim)
        {
            switch (aim)
            {
                case "still":
                    activeAim.enabled = false;
                    stillAim.enabled = true;
                    break;
                case "active":
                    stillAim.enabled = false;
                    activeAim.enabled = true;
                    break;
            }
        }
    }
}
