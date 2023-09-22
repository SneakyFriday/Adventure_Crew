using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkipIntroTrigger : MonoBehaviour
{

    [SerializeField] private float fillSpeed = 100f;
    [SerializeField] private Image _skipImage;

    private bool filling;
    private float fillAmount;
    private bool isSkipping;
    private float skipTimer;
    private bool sceneLoaded;

    private void Update()
    {
        CheckSkipIntro();

        UpdateFillAmount();

        if (!(fillAmount >= 1f) || sceneLoaded) return;
        isSkipping = true;
        fillAmount = 1f;
        skipTimer += Time.deltaTime;
        _skipImage.fillAmount = 1f;

        if (skipTimer >= 1f)
        {
            LoadGameScreen();
        }
    }

    private void CheckSkipIntro()
    {
        if (Input.GetKey(KeyCode.Escape) && IsInIntroScreen())
        {
            StartFilling();
        }
        else
        {
            StopFilling();
        }
    }

    private bool IsInIntroScreen()
    {
        return LevelManager.Instance.GetActiveScene().buildIndex == (int)SceneIndexes.INTROSCREEN;
    }

    private void StartFilling()
    {
        if (!filling)
        {
            filling = true;
        }
    }

    private void StopFilling()
    {
        if (!filling)
        {
            return;
        }
        filling = false;
    }

    private void UpdateFillAmount()
    {
        if (filling && fillAmount < 1f && !isSkipping)
        {
            fillAmount += fillSpeed * Time.deltaTime;
        }
        else if (!filling && fillAmount > 0f && !isSkipping)
        {
            fillAmount -= fillSpeed * Time.deltaTime;
        }

        _skipImage.fillAmount = Mathf.Clamp01(fillAmount);
    }

    private void LoadGameScreen()
    {
        if (!sceneLoaded)
        {
            LevelManager.Instance.LoadScene(SceneIndexes.GAMESCREEN.ToString());
            sceneLoaded = true;
        }
    }
}
