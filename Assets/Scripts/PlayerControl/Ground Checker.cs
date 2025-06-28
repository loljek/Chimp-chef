using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [SerializeField] BoxCollider groundSpace;
    public bool isGrounded = false;

    private void Awake()
    {
        groundSpace = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    public bool isOnGround()
    {
        return isGrounded;
    }
}