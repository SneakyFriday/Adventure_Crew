using System.Collections;
using UnityEngine;

public class CanvasGroupAlphaFade : MonoBehaviour
{
    public CanvasGroup canvasGroupFadeOut, canvasGroupFadeIn;
    public float lerpTime = 1f;
    
    public void FadeCanvasGroup()
    {
        StartCoroutine(FadeRoutine(canvasGroupFadeOut, canvasGroupFadeIn));
    }

    private IEnumerator FadeRoutine(CanvasGroup fadeOutCanvas, CanvasGroup fadeInCanvas)
    {
        yield return FadeMenuRoutine(fadeOutCanvas, false);
        yield return FadeMenuRoutine(fadeInCanvas, true);
    }

    private IEnumerator FadeMenuRoutine(CanvasGroup canvasGroupToFade, bool fadeIn)
    {
        float startAlpha = canvasGroupToFade.alpha;
        float targetAlpha = fadeIn ? 1f : 0f;

        canvasGroupToFade.interactable = fadeIn;
        canvasGroupToFade.blocksRaycasts = fadeIn;

        float startTime = Time.time;
        float elapsedTime = 0f;

        while (elapsedTime < lerpTime)
        {
            yield return null;

            elapsedTime = Time.time - startTime;
            float t = Mathf.Clamp01(elapsedTime / lerpTime);

            float alphaValue = Mathf.SmoothStep(startAlpha, targetAlpha, t);
            canvasGroupToFade.alpha = alphaValue;
        }

        canvasGroupToFade.alpha = targetAlpha;
    }
}