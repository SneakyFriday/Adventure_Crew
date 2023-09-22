using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class BtnSwitch : MonoBehaviour
{
    [SerializeField] private GameObject toggleObject;
    [SerializeField] private Button button;
    private CanvasGroup _menuCanvasGroup;

    private void Start()
    {
        button = button != null ? button : GetComponent<Button>();
        button.onClick.AddListener(OnClick);
        _menuCanvasGroup = toggleObject.GetComponent<CanvasGroup>();
    }

    private void OnClick()
    {
        Debug.Log("Clicked Menu Button");
        if (toggleObject == null || !toggleObject.GetComponent<CanvasGroup>()) return;
        _menuCanvasGroup.alpha = _menuCanvasGroup.alpha > 0 ? 0 : 1;
        _menuCanvasGroup.interactable = !_menuCanvasGroup.interactable;
        _menuCanvasGroup.blocksRaycasts = !_menuCanvasGroup.blocksRaycasts;
    }
    
    
}
