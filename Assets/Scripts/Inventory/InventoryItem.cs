using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(CanvasGroup))]
public class InventoryItem : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public Transform parentAfterDrag;

    [SerializeField] private TextMeshProUGUI _stackSizeText;
    [SerializeField] private Image _itemImage;
    [SerializeField] private SO_Item _item;

    private int _stackSize = 1;
    private CanvasGroup _canvasGroup;
    
    private void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }
    
    public void InitItem(SO_Item newItem)
    {
        _item = newItem;
        _itemImage.sprite = newItem.itemSprite;
        if (newItem.stackable) _stackSizeText.text = _stackSize.ToString();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        _canvasGroup.alpha = 0.6f;
        _canvasGroup.blocksRaycasts = false;
    }
    
    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parentAfterDrag);
        _canvasGroup.alpha = 1f;
        _canvasGroup.blocksRaycasts = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public SO_Item GetItem()
    {
        return _item;
    }
}