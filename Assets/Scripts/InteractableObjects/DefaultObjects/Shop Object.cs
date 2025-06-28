using System.Collections;
using PlayerControl;
using UnityEngine;

namespace InteractableObjects.DefaultObjects
{
    public class ShopObject : InteractableObject
    {
        [SerializeField] private GameObject shopUI;
        [SerializeField] private GameObject playerUI;
        [SerializeField] private PlayerController player;
        private bool isInShop;
        
        public override void Interaction()
        {
            if (isInShop)
            {
                StartCoroutine(Exit());
            }
            else
            {
                playerUI.SetActive(false);
                shopUI.SetActive(true);
                player.GetComponent<Rigidbody>().isKinematic = true;
                player.isActive = false;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                isInShop = true;
            }
        }

        private IEnumerator Exit()
        {
            yield return new WaitForSeconds(0.4f);
            shopUI.SetActive(false);
            playerUI.SetActive(true);
            player.GetComponent<Rigidbody>().isKinematic = false;
            player.isActive = true;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            isInShop = false;
        }
    }
}
