using UnityEngine;
using UnityEngine.UI;

namespace Shop
{
    public class ChangeCategory : MonoBehaviour
    {
        [SerializeField] ScrollRect scroll;
        [SerializeField] private GameObject activeCategory;
        [SerializeField] private GameObject[] unactiveCategory;
        
        public void OnButtonClick()
        {
            foreach (GameObject go in unactiveCategory)
            {
                go.SetActive(false);
            }    
            activeCategory.SetActive(true);
            scroll.content = activeCategory.GetComponent<RectTransform>();
        }
    }
}
