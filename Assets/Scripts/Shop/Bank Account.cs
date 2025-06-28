using TMPro;
using UnityEngine;

namespace Shop
{
    public class BankAccount : MonoBehaviour
    {
        [SerializeField] private int balance;
        [SerializeField, Range(0, 99999999)] private TextMeshProUGUI balanceText;

        private void Awake()
        {
            balanceText.text = balance.ToString();
        }
        
        public void AddBalance(int sum)
        {
            balance += sum;
        }
        
        public bool Pay(int sum)
        {
            if (balance >= sum)
            {
                balance -= sum;
                return true;
            }
            return false;
        }


        public int GetBalance()
        {
            return balance;
        }
    }
}
