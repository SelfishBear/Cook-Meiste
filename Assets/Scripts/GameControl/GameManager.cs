using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public event EventHandler OnStateChanged;

    private enum GameState
    {
        WaitingToStart,
        CountdownToStart,
        GamePlaying,
        GameOver,
    }

    private GameState _gameState;

    private float _waitingToStartTimer = 1f;
    private float _countdownToStartTimer = 3f;
    private float _gamePlayingTimer;
    private float _gamePlayingTimerMax = 10f;


    private void Awake()
    {
        Instance = this;
        _gameState = GameState.WaitingToStart;
    }

    private void Update()
    {
        switch (_gameState)
        {
            case GameState.WaitingToStart:
                _waitingToStartTimer -= Time.deltaTime;
                if (_waitingToStartTimer < 0f)
                {
                    _gameState = GameState.CountdownToStart;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }

                break;
            case GameState.CountdownToStart:
                _countdownToStartTimer -= Time.deltaTime;
                if (_countdownToStartTimer < 0f)
                {
                    _gameState = GameState.GamePlaying;
                    _gamePlayingTimer = _gamePlayingTimerMax;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }

                break;
            case GameState.GamePlaying:
                _gamePlayingTimer -= Time.deltaTime;
                if (_gamePlayingTimer < 0f)
                {
                    _gameState = GameState.GameOver;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }

                break;
            case GameState.GameOver:
                break;
        }

        Debug.Log(_gameState);
    }

    public bool IsGamePlaying()
    {
        return _gameState == GameState.GamePlaying;
    }

    public bool IsCountdownToStartActive()
    {
        return _gameState == GameState.CountdownToStart;
    }

    public float GetCoundownToStartTimer()
    {
        return _countdownToStartTimer;
    }

    public bool IsGameOverActive()
    {
        return _gameState == GameState.GameOver;
    }

    public float GetPlayingTimerNormalized()
    {
        return 1 - (_gamePlayingTimer / _gamePlayingTimerMax);
    }
}