using System;
using UnityEngine;
using System.Collections;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SingleCanvasController : MonoBehaviour
{
    public bool showingCanvas;
    
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private float fadeDuration = 1f;

    private bool isFading;
    private float targetAlpha = 1f;
    private const double TOLERANCE = 0.09;

    private void Start()
    {
        canvasGroup = canvasGroup != null ? canvasGroup : GetComponent<CanvasGroup>();
        canvasGroup.alpha = targetAlpha;
        UpdateCanvasVisibility();
    }

    private void Update()
    {
        if (showingCanvas)
        {
            if (!isFading)
            {
                targetAlpha = 1f;
                StartFade();
            }
        }
        else
        {
            if (!isFading)
            {
                targetAlpha = 0f;
                StartFade();
            }
        }

        if (!isFading) return;
        var deltaAlpha = Time.deltaTime / fadeDuration;
        canvasGroup.alpha = Mathf.MoveTowards(canvasGroup.alpha, targetAlpha, deltaAlpha);

        if (Math.Abs(canvasGroup.alpha - targetAlpha) > TOLERANCE) return;
        isFading = false;
        
        if (targetAlpha == 0f)
        {
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }
        else
        {
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }
    }
    
    private void StartFade()
    {
        isFading = true;
    }
    
    private void UpdateCanvasVisibility()
    {
        canvasGroup.alpha = showingCanvas ? 1f : 0f;
        canvasGroup.interactable = showingCanvas;
        canvasGroup.blocksRaycasts = showingCanvas;
    }
}
