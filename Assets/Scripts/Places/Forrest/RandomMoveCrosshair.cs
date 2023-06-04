using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class RandomMoveCrosshair : MonoBehaviour
{
    [SerializeField] private Image _targetImage;
    [SerializeField] private float _speed = 200f;
    private RectTransform _targetSize;
    private Rect _rect;
    private Vector2 _target;

    void Start()
    {
        _targetSize = _targetImage.rectTransform;
        _rect = _targetSize.rect;
        print("Width: " + _rect.width + " Height: " + _rect.height);
        print(_targetSize);

        _target = new Vector2(Random.Range(0, _rect.width), Random.Range(0, _rect.height));
        //_rect.xMin.
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, _target, 1f * Time.deltaTime * _speed);
        if (!(Vector2.Distance(transform.position, _target) < 0.1f)) return;
        _target = new Vector2(Random.Range(0, _rect.width), Random.Range(0, _rect.height));
        _speed = Random.Range(200f, 600f);
        print("Target: " + _target);
        print("Target Size: " + _rect.width + " " + _rect.height);
    }
}