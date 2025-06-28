using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider))]

public class InteractionController : MonoBehaviour
{
    [SerializeField] BoxCollider interactionSpace;
    [SerializeField] RawImage stillAim;
    [SerializeField] RawImage activeAim;
    private bool _isActive = false;
    private InteractableObject _interactableObject;
    private void Awake()
    {
        interactionSpace = GetComponent<BoxCollider>();
        activeAim.enabled = false;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Interactable"))
        {
            _isActive = true;
            stillAim.enabled = false;
            activeAim.enabled = true;
            _interactableObject = other.GetComponent<InteractableObject>();
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Interactable"))
        {
            _isActive = true;
            stillAim.enabled = false;
            activeAim.enabled = true;
            _interactableObject = other.GetComponent<InteractableObject>();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Interactable"))
        {
            activeAim.enabled = false;
            stillAim.enabled = true;
            _isActive = false;
        }
    }

    public void InteractActivation()
    {
        if (_isActive & _interactableObject != null)
        {
            _interactableObject.Interaction();
        }
    }

    public bool isActive()
    {
        return _isActive;
    }
}
