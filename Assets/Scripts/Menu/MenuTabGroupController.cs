using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuTabGroupController : MonoBehaviour
{
    public List<MenuTabButton> tabButtons;
    [SerializeField] private MenuTabButton selectedTab;
    public List<GameObject> objectsToSwap;
    public Sprite tabIdle;
    public Sprite tabHover;
    public Sprite tabActive;
    
    /**
     * This method is called when the script is loaded
     */
    public void Subscribe(MenuTabButton button)
    {
        if (tabButtons == null)
        {
            tabButtons = new List<MenuTabButton>();
        }
        tabButtons.Add(button);
    }
    
    /**
     * This method is called when the mouse enters the tab button
     * It sets the tab to the hover state sprite and resets the other tabs
     */
    public void OnTabEnter(MenuTabButton button)
    {
        ResetTabs();
        if (selectedTab == null || button != selectedTab)
        {
            button.background.sprite = tabHover;
        }
    }
    
    /**
     * This method is called when the mouse exits the tab button
     * It resets the tabs to the default state sprite
     */
    public void OnTabExit(MenuTabButton button)
    {
        ResetTabs();
    }
    
    /**
     * This method is called when the tab button is clicked
     * It sets the tab to the active state sprite and resets the other tabs
     */
    public void OnTabSelected(MenuTabButton button)
    {
        if(selectedTab != null) selectedTab.Deselect();
        
        selectedTab = button;
        
        selectedTab.Select();
        
        ResetTabs();
        button.background.sprite = tabActive;
        int index = button.transform.GetSiblingIndex();
        for (int i = 0; i < objectsToSwap.Count; i++)
        {
            if (i == index)
            {
                objectsToSwap[i].SetActive(true);
            }
            else
            {
                objectsToSwap[i].SetActive(false);
            }
        }
    }
    
    /**
     * This method resets the tab buttons to their default state
     * It is called when the mouse enters or exits a tab button
     */
    public void ResetTabs()
    {
        foreach (MenuTabButton button in tabButtons)
        {
            if (selectedTab != null && button == selectedTab) { continue; }
            button.background.sprite = tabIdle;
        }
    }
}
