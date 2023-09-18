using UnityEngine;

public class ForestShop : MonoBehaviour
{
    [SerializeField] private CanvasGroup shopCanvas;
    
    public void ShowShop()
    {
        shopCanvas.alpha = 1f;
        shopCanvas.interactable = true;
        shopCanvas.blocksRaycasts = true;
    }
    
    public void HideShop()
    {
        shopCanvas.alpha = 0f;
        shopCanvas.interactable = false;
        shopCanvas.blocksRaycasts = false;
    }
}
