using UnityEngine;

namespace PlayerControl
{
    public class GroundChecker : MonoBehaviour
    {
        public bool isGrounded;

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
}
