using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;

public class ButtonHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Button Hover Effect")]
    [Range(1f, 2f)]
    [SerializeField] private float _scaleUpFactor = 1.05f;
    [SerializeField] private AudioClip hoverSound;
    private AudioMixerGroup _sfxMixerGroup;
    private RectTransform _btnRect;

    // Holt sich die RectTransform des Buttons.
    private void Start()
    {
        _btnRect = GetComponent<RectTransform>();
        _sfxMixerGroup = SoundManager.sfxMixerGroup;
    }

    // Vergrößert den Button, wenn der Mauszeiger darüber ist.
    public void OnPointerEnter(PointerEventData eventData)
    {
        var newSize = new Vector3(_scaleUpFactor, _scaleUpFactor, _scaleUpFactor);
        _btnRect.localScale = Vector3.Lerp(_btnRect.localScale, newSize, .5f);
        if(hoverSound) SoundManager.Instance.PlaySFX(hoverSound, _sfxMixerGroup);
    }

    // Verkleinert den Button, wenn der Mauszeiger nicht mehr darüber ist.
    public void OnPointerExit(PointerEventData eventData)
    {
        var newSize = new Vector3(1f, 1f, 1f);
        _btnRect.localScale = Vector3.Lerp(_btnRect.localScale, newSize, .5f);
    }
}