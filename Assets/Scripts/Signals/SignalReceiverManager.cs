using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignalReceiverManager : MonoBehaviour
{
    public void TriggerIntroMusic()
    {
        SoundManager.Instance.StopMusic();
        SoundManager.Instance.PlayIntro();
    }

    public void TriggerGameLevel()
    {
        LevelManager.Instance.LoadScene(SceneIndexes.GAMESCREEN.ToString());
    }
}
