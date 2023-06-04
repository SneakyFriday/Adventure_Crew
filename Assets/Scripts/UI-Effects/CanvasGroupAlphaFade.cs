using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasGroupAlphaFade : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public float startAlpha;
    public float endAlpha;
    public float lerpTime = 1f;
    
    public void FadeCanvasGroup()
    {
        StartCoroutine(FadeCanvasGroupCoroutine());
    }
    
    private IEnumerator FadeCanvasGroupCoroutine()
    {
        float timeStartedLerping = Time.time;
        float timeSinceStarted = Time.time - timeStartedLerping;
        float percentageComplete = timeSinceStarted / lerpTime;
        
        while (true)
        {
            timeSinceStarted = Time.time - timeStartedLerping;
            percentageComplete = timeSinceStarted / lerpTime;
            
            float currentValue = Mathf.Lerp(startAlpha, endAlpha, percentageComplete);
            canvasGroup.alpha = currentValue;
            
            if (percentageComplete >= 1) break;
            
            yield return new WaitForEndOfFrame();
        }
    }
}