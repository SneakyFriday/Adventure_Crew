using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtonController : MonoBehaviour
{
    public void LoadMenu()
    {
        MenuController.Instance.SetMenuActive();
    }
}
