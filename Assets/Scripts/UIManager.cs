using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using DG.Tweening;
public class UIManager : MonoBehaviour
{
    #region Singleton class: UIManager

    [Header("Level Progress UI")] 
    
    
    
    public static UIManager Instance;

     void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    #endregion
    
    [SerializeField] int sceneOffset;
    [SerializeField] TMP_Text nextLeveltext;
    [SerializeField] TMP_Text currentLeveltext;
    [SerializeField] Image progressFillImage;

    [Space] 
    [SerializeField] TMP_Text levelWinText;

    [Space]
    [SerializeField] Image fadePanel;

     void Start()
     {
         FadeSceneAtStart();
         progressFillImage.fillAmount = 0f;
         SetLevelProgressText();
     }

     void SetLevelProgressText()
     {
         int level = SceneManager.GetActiveScene().buildIndex + sceneOffset;
         currentLeveltext.text = level.ToString();
         nextLeveltext.text = (level + 1).ToString();
     }

     public void UpdateLevelProgress()
     {
         float value = 1f - ((float) LevelManager.Instance.objectsInScene / LevelManager.Instance.totalObjects);
         
         progressFillImage.DOFillAmount(value, .4f);
     }

     public void LevelWinText()
     {
         levelWinText.DOFade(1f, .6f).From(0f);
     }

     public void FadeSceneAtStart()
     {
         fadePanel.DOFade(0f, 1.3f).From(1f);
     }
}

