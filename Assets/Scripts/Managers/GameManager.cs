using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool gamePause;
    public event Action onShowPauseUi;
    public event Action onHidePauseUi;
    public event Action HideAnyOtherUi;
    public event Action onShowLoseUi;
    public event Action onShowWinUi;
    public bool win, lose;
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        InputManager.Instance.onEscapeAction += TogglePauseGame;
    }

    public void TogglePauseGame(object sender, EventArgs e)
    {
        if (!lose && !win)
        {
            gamePause = !gamePause;
            if (gamePause)
            {
                Time.timeScale = 0f;
                HideAnyOtherUi?.Invoke();
                onShowPauseUi?.Invoke();
                if (PlayerStateManager.Instance !=  null)
                {
                    PlayerStateManager.Instance.DisableStateManager();
                }
            }
            else
            {
                Time.timeScale = 1f;
                onHidePauseUi?.Invoke();
                if (PlayerStateManager.Instance !=  null)
                {
                    PlayerStateManager.Instance.EnableStateManager();
                }
            }
        }
        
    }

    public void UnPauseGame()
    {
        if (gamePause)
        {
            gamePause = false;
        }
        Time.timeScale = 1f;
        onHidePauseUi?.Invoke();
        if (PlayerStateManager.Instance != null)
        {
            PlayerStateManager.Instance.EnableStateManager();
        }
    }

    public void PauseGame() // idk if i need it or not 
    {
        if (!gamePause)
        {
            gamePause = true;
        }
        Time.timeScale = 0f;
        if (PlayerStateManager.Instance != null)
        {
            PlayerStateManager.Instance.DisableStateManager();
        }
    }


    public void ShowWinWindow()
    {
        win = true;
        onShowWinUi?.Invoke();
    }

    public void ShowLoseWindow()
    {
        lose = true;
        onShowLoseUi?.Invoke();
    }
}
