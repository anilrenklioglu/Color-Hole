using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;
using DG.Tweening;

public class Underground : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!Game.isGameOver)
        {
            string tag = other.tag;

            if (tag.Equals("Object"))
            {
                LevelManager.Instance.objectsInScene--;
                UIManager.Instance.UpdateLevelProgress();
                
                Magnet.Instance.RemoveFromMagnetField(other.attachedRigidbody);
                
                Destroy(other.gameObject);
                
                //If Win

                if (LevelManager.Instance.objectsInScene == 0)
                {
                    UIManager.Instance.LevelWinText();
                    LevelManager.Instance.WinFXPlayer();
                    Invoke("NextLevel",(2f));
                   
                }
            }

            if (tag.Equals("Obstacle"))
            {
                Game.isGameOver = true;
                Camera.main.transform.DOShakePosition(1f, .2f, 20, 90f).OnComplete(() =>
                {LevelManager.Instance.Restartevel();
                });
                
            }
        }
    }
    void NextLevel()
    {
        LevelManager.Instance.LoadNextLevel(); 
    }
}
