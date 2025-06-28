using System;
using System.Collections;
using InteractableObjects;
using TMPro;
using UnityEngine;

namespace Shop
{
    public class ProductObject : MonoBehaviour
    {
        [SerializeField] public int price;
        [SerializeField] public PlacementObject prefab;
        [SerializeField] private PlacementObject box;
        [SerializeField] private Transform spawn;
        [SerializeField] public BankAccount bankAccount;
        [SerializeField] public TextMeshProUGUI priceText;
        [SerializeField] public TextMeshProUGUI balanceText;

        private void Awake()
        {
            priceText.color = new Color(255, 248, 0);
            priceText.text = price.ToString();
            prefab.gameObject.SetActive(false);
        }

        public void OnButtonClick()
        {
            if (box.GetObjectModel() == null)
            {
                if (bankAccount.Pay(price))
                {
                    box.SetObjectModel(Instantiate(prefab, spawn.position, spawn.rotation));
                    balanceText.text = bankAccount.GetBalance().ToString();
                    StartCoroutine(MessageText(Color.green, price.ToString()));
                }
                else
                {
                    StartCoroutine(MessageText(Color.red, price.ToString()));
                }
            }
            else
            {
                StartCoroutine(MessageText(Color.red, "Нет места"));
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
