using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseUi : MonoBehaviour
{
    [SerializeField] private Button continueButton;
    [SerializeField] private Button quitMenuButton;
    private void Start()
    {
        GameManager.Instance.onShowPauseUi += Show;
        GameManager.Instance.onHidePauseUi += Hide;
        continueButton.onClick.AddListener(ContinueButton);
        quitMenuButton.onClick.AddListener(QuitButton);
        Hide();
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void ContinueButton()
    {
        GameManager.Instance.UnPauseGame();
    }

    private void QuitButton()
    {
        GameManager.Instance.UnPauseGame();
        SaveLoadOpenedDoors.ResetDoors();
        Loader.LoadScene(Loader.Scenes.GameMainMenu);
    }
}
