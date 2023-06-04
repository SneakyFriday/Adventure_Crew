using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class FillAmountController : MonoBehaviour
{
    [SerializeField] private Image _fillAmountImageR;
    [SerializeField] private Image _fillAmountImageG;
    [SerializeField] private Image _fillAmountImageB;
    [SerializeField] private Slider _sliderR;
    [SerializeField] private Slider _sliderG;
    [SerializeField] private Slider _sliderB;
    [SerializeField] private Image imageCheck;
    [SerializeField] private Image toleranceImage;
    private const double Tolerance = 0.09;
    private Vector3 _rgbValue;

    void Start()
    {
        SetRandomResultColor();
        UpdateFillImages();
    }

    public void UpdateFillImages()

    {
        _fillAmountImageR.fillAmount = _sliderR.value;
        _fillAmountImageG.fillAmount = _sliderG.value;
        _fillAmountImageB.fillAmount = _sliderB.value;
        
        _fillAmountImageB.color = new Color(_sliderR.value, _sliderG.value, _sliderB.value);
        _fillAmountImageG.color = new Color(_sliderR.value, _sliderG.value, _sliderB.value);
        _fillAmountImageR.color = new Color(_sliderR.value, _sliderG.value, _sliderB.value);
        
        CheckResult();
    }
    
    private void SetRandomResultColor()
    {
        _rgbValue = new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        print("Random RGB Value: " + _rgbValue);
        imageCheck.color = new Color(_rgbValue.x, _rgbValue.y, _rgbValue.z, 1);
        print("Image Color: " + imageCheck.color);
    }

    private void CheckResult()
    {
        print("Slider Values Check:  " + _sliderR.value + " " + _sliderG.value + " " + _sliderB.value);
        print("RGB Values Check:  " + _rgbValue.x + " " + _rgbValue.y + " " + _rgbValue.z);
        print("Tolerance 0.05: " + Math.Abs(_rgbValue.x - _sliderR.value) + " " + Math.Abs(_rgbValue.y - _sliderG.value) + " " + Math.Abs(_rgbValue.z - _sliderB.value));
        var toleranceSum = Math.Abs(_rgbValue.x - _sliderR.value) + Math.Abs(_rgbValue.y - _sliderG.value) + Math.Abs(_rgbValue.z - _sliderB.value);
        if (Math.Abs(_rgbValue.x - _sliderR.value) < Tolerance && Math.Abs(_rgbValue.y - _sliderG.value) < Tolerance &&
            Math.Abs(_rgbValue.z - _sliderB.value) < Tolerance)
        {
            print("You won!");
        }
        
        toleranceImage.fillAmount = 1 + (float)Tolerance - toleranceSum;
    }
}