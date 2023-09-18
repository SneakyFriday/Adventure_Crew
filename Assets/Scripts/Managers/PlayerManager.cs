using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private int companionsCount = 0;
    [SerializeField] TextMeshProUGUI companionsCountText;
    
    public static PlayerManager Instance;
    public int CompanionsCount => companionsCount;
    
    #region Singleton
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            companionsCount = 0;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    #endregion
    
    public void AddCompanion()
    {
        companionsCount++;
        companionsCountText.text = "Gruppe: " + companionsCount+"/3";
    }
    
    public void RemoveCompanion()
    {
        companionsCount--;
        companionsCountText.text = "Gruppe: " + companionsCount+"/3";
    }
    
    public void ResetCompanions()
    {
        companionsCount = 0;
        companionsCountText.text = "Gruppe: " + companionsCount+"/3";
    }
}
