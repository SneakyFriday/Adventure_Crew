using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasGroupAlphaFade : MonoBehaviour
{
    public CanvasGroup canvasGroupFadeOut, canvasGroupFadeIn;
    public float startAlpha;
    public float endAlpha;
    public float lerpTime = 1f;
    
    public void FadeCanvasGroup()
    {
        //StartCoroutine(FadeCanvasGroupCoroutine());
        StartCoroutine(FadeRoutine(canvasGroupFadeOut, canvasGroupFadeIn));
    }
    
    // private IEnumerator FadeCanvasGroupCoroutine()
    // {
    //     var timeStartedLerping = Time.time;
    //     var timeSinceStarted = Time.time - timeStartedLerping;
    //     var percentageComplete = timeSinceStarted / lerpTime;
    //     
    //     while (true)
    //     {
    //         timeSinceStarted = Time.time - timeStartedLerping;
    //         percentageComplete = timeSinceStarted / lerpTime;
    //         
    //         var currentValue = Mathf.Lerp(startAlpha, endAlpha, percentageComplete);
    //         canvasGroup.alpha = currentValue;
    //         
    //         if (percentageComplete >= 1) break;
    //         
    //         yield return new WaitForEndOfFrame();
    //     }
    // }
    
    private IEnumerator FadeRoutine(CanvasGroup fadeOutCanvas, CanvasGroup fadeInCanvas)
    {
        StartCoroutine(FadeMenuRoutine(fadeOutCanvas));
        yield return new WaitForSeconds(lerpTime);
        StartCoroutine(FadeMenuRoutine(fadeInCanvas));
    }
    
    private IEnumerator FadeMenuRoutine(CanvasGroup canvasGroupToFade)
    {
        var fadeStart = canvasGroupToFade.alpha;
        var fadeTarget = canvasGroupToFade.alpha == 0 ? 1 : 0;
        var boolTarget = !canvasGroupToFade.interactable;

        canvasGroupToFade.interactable = boolTarget;
        canvasGroupToFade.blocksRaycasts = boolTarget;

        var startTime = Time.time;

        while (Time.time <= startTime + lerpTime)
        {
            yield return null;
            var t = (Time.time - startTime) / lerpTime;
            var alphaValue = Mathf.SmoothStep(fadeStart, fadeTarget, t);
            canvasGroupToFade.alpha = alphaValue;
        }
    }
}