using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Button Hover Effect")]
    [Range(1f, 2f)]
    [SerializeField] private float _scaleUpFactor = 1.05f;
    [SerializeField] private AudioClip hoverSound;
    private TextMeshProUGUI _buttonImage;
    private Color _buttonOriginColor;
    private AudioMixerGroup _sfxMixerGroup;
    private RectTransform _btnRect;

    // Holt sich die RectTransform des Buttons.
    private void Start()
    {
        _buttonImage = GetComponent<Button>().GetComponentInChildren<TextMeshProUGUI>();
        _btnRect = GetComponent<RectTransform>();
        _sfxMixerGroup = SoundManager.sfxMixerGroup;
        _buttonOriginColor = _buttonImage.color;
    }

    // Vergrößert den Button, wenn der Mauszeiger darüber ist.
    public void OnPointerEnter(PointerEventData eventData)
    {
        var newSize = new Vector3(_scaleUpFactor, _scaleUpFactor, _scaleUpFactor);
        _btnRect.localScale = Vector3.Lerp(_btnRect.localScale, newSize, .5f);
        _buttonImage.color = new Color(0.2f, 0.5f, 0.7f, 0.8f);
        if(hoverSound != null) SoundManager.Instance.PlaySFX(hoverSound, _sfxMixerGroup);
    }

    // Verkleinert den Button, wenn der Mauszeiger nicht mehr darüber ist.
    public void OnPointerExit(PointerEventData eventData)
    {
        var newSize = new Vector3(1f, 1f, 1f);
        _btnRect.localScale = Vector3.Lerp(_btnRect.localScale, newSize, .5f);
        _buttonImage.color = _buttonOriginColor;
    }
}