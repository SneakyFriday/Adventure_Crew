using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class TypingTextScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textToType;
    [SerializeField] private string[] textsToDisplay;
    [SerializeField] private float typeSpeed = 1f;
    [SerializeField] private int delayBetweenTexts;
    private int _currentTextIndex;

    private void Start()
    {
        StartTypeText();
    }

    public void StartTypeText()
    {
        StartCoroutine(TypeText(textsToDisplay[_currentTextIndex]));
    }

    private IEnumerator TypeText(string textToDisplay)
    {
        textToType.text = "";
        foreach (char letter in textToDisplay)
        {
            textToType.text += letter;
            yield return new WaitForSeconds(typeSpeed);
        }
        yield return new WaitForSeconds(delayBetweenTexts);
        _currentTextIndex++;
        if (_currentTextIndex >= textsToDisplay.Length)
        {
            _currentTextIndex = 0;
        }
        StartCoroutine(TypeText(textsToDisplay[_currentTextIndex]));
    }

    public void StopTextCoroutine()
    {
        StopCoroutine(TypeText(textsToDisplay[_currentTextIndex]));
    }
}
