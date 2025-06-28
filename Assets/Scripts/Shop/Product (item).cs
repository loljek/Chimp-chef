using System;
using System.Collections;
using InteractableObjects;
using TMPro;
using UnityEngine;

namespace Shop
{
    public class ProductItem : MonoBehaviour
    {
        [SerializeField] public int price;
        [SerializeField] public PickingObject prefab;
        [SerializeField] private Transform spawn;
        [SerializeField] public BankAccount bankAccount;
        [SerializeField] public TextMeshProUGUI priceText;
        [SerializeField] public TextMeshProUGUI balanceText;

        private void Awake()
        {
            priceText.color = new Color(255, 248, 0);
            priceText.text = price.ToString();
            prefab.gameObject.SetActive(true);
        }

        public void OnButtonClick()
        {
            if (bankAccount.Pay(price))
            {
                Instantiate(prefab, spawn.position, spawn.rotation);
                balanceText.text = bankAccount.GetBalance().ToString();
                StartCoroutine(MessageText(Color.green, price.ToString()));
            }
            else
            {
                StartCoroutine(MessageText(Color.red, price.ToString()));
            }
        }
        
        private IEnumerator MessageText(Color color,String text)
        {
            priceText.color = color;
            priceText.text = text;
            yield return new WaitForSeconds(0.25f);
            priceText.color = new Color(255, 248, 0);
            priceText.text = price.ToString();
        }
    }
}
