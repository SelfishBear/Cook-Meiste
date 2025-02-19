using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour
{
    
    [SerializeField] private Button _resumeButton;
    [SerializeField] private Button _mainMenuButton;
    
    private void Start()
    {
        _resumeButton.onClick.AddListener(() => GameManager.Instance.TogglePauseGame());
        _mainMenuButton.onClick.AddListener(() =>
        {
            GameManager.Instance.TogglePauseGame();
            Loader.Load(Loader.Scene.MainMenu);
        });
           
        GameManager.Instance.OnGamePaused += GameManager_OnGamePaused;
        GameManager.Instance.OnGameResumed += GameManager_OnGameResumed;
        Hide();
    }

    private void GameManager_OnGameResumed(object sender, EventArgs e)
    {
        Hide();
    }

    private void GameManager_OnGamePaused(object sender, EventArgs e)
    {
        Show();
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
