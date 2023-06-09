using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Button Hover Effect")]
    [Range(1f, 2f)]
    [SerializeField] private float _scaleUpFactor = 1.05f;
    private RectTransform btnRect;

    // Holt sich die RectTransform des Buttons.
    private void Start()
    {
        btnRect = GetComponent<RectTransform>();
    }

    // Vergrößert den Button, wenn der Mauszeiger darüber ist.
    public void OnPointerEnter(PointerEventData eventData)
    {
        var newSize = new Vector3(_scaleUpFactor, _scaleUpFactor, _scaleUpFactor);
        btnRect.localScale = Vector3.Lerp(btnRect.localScale, newSize, .5f);
    }

    // Verkleinert den Button, wenn der Mauszeiger nicht mehr darüber ist.
    public void OnPointerExit(PointerEventData eventData)
    {
        var newSize = new Vector3(1f, 1f, 1f);
        btnRect.localScale = Vector3.Lerp(btnRect.localScale, newSize, .5f);
    }
}