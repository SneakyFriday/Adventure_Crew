using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonChangeSprite : MonoBehaviour
{
    [SerializeField] private Sprite changeSprite;
    [SerializeField] private Sprite originalSprite;
    private Button _btn;
    void Start()
    {
        _btn = GetComponent<Button>();
        _btn.onClick.AddListener(ChangeSprite);
    }

    private void ChangeSprite()
    {
        _btn.image.sprite = changeSprite;
    }

}
