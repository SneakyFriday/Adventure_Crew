using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtonController : MonoBehaviour
{
    public void LoadMenu(bool loadMenu)
    {
        MenuController.Instance.SetMenuActive(loadMenu);
    }
}
