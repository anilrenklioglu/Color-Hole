using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    #region Singleton class: LevelManager

    public static LevelManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    #endregion

    [SerializeField] ParticleSystem winFX;
    
    [Space]
    [HideInInspector] public int objectsInScene;
    [HideInInspector] public int totalObjects;

    [SerializeField] Transform objectsParent;

    [Space] 
    [Header("Level Obstacles & Objects")] 
    
    [SerializeField] Material groundMaterial;
    [SerializeField] Material objectMaterial;
    [SerializeField] Material obstacleMaterial;

    [SerializeField] SpriteRenderer groundBorderSprite;
    [SerializeField] SpriteRenderer groundSideSprite;
    [SerializeField] SpriteRenderer bgFadeSprite;

    [SerializeField] Image progressFillImage;

    [Space] 
    [Header("----Level Colors----")] 
    [Header("Ground Colors")]
    
    [SerializeField] Color groundColor;
    [SerializeField] Color bordersColor;
    [SerializeField] Color sideColor;
    
    [Header("Obstacles and Objects Color")]
    
    [SerializeField] Color objectColor;
    [SerializeField] Color obstacleColor;
    
    [Header("UI (Progress)")]
    
    [SerializeField] Color progressFillColor;
    
    [Header("Background")]
    
    [SerializeField] Color cameraColor;
    [SerializeField] Color fadeColor;
    void Start()
    {
        CountObjects();
        UpdateColors();
    }

    void CountObjects()
    {
        totalObjects = objectsParent.childCount;
        objectsInScene = totalObjects;
    }

    public void WinFXPlayer()
    {
        winFX.Play();
    }
    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
    public void Restartevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void UpdateColors()
    {
        groundMaterial.color = groundColor;
        groundSideSprite.color = sideColor;
        groundBorderSprite.color = bordersColor;

        obstacleMaterial.color = obstacleColor;
        objectMaterial.color = objectColor;

        progressFillImage.color = progressFillColor;

        Camera.main.backgroundColor = cameraColor;
        bgFadeSprite.color = fadeColor;
    }

    public void OnValidate()
    {
        UpdateColors();
    }
}
