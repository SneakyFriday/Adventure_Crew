using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnSwitch : MonoBehaviour
{
    [SerializeField] private GameObject toggleObject;
    [SerializeField] private Button button;

    private void Start()
    {
        button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        // Setzt das Object auf den gegenteiligen Status
        toggleObject.SetActive(!toggleObject.activeSelf);
    }
}
