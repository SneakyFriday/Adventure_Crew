using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
public class MenuTabButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    [SerializeField] private MenuTabGroupController tabGroup;
    
    public Image background;
    
    public UnityEvent onTabSelected;
    public UnityEvent onTabDeselected;
    void Start()
    {
        background = GetComponent<Image>();
        tabGroup.Subscribe(this);
    }

    public void Select()
    {
        onTabSelected?.Invoke();
    }
    
    public void Deselect()
    {
        onTabDeselected?.Invoke();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tabGroup.OnTabEnter(this);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        tabGroup.OnTabSelected(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tabGroup.OnTabExit(this);
    }
}
