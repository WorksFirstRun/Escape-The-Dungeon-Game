using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinUi : MonoBehaviour
{
    [SerializeField] private Button playAgainButton;
    [SerializeField] private Button quitButton;

    private void Start()
    {
        playAgainButton.onClick.AddListener(() =>
        {
            Loader.LoadScene(Loader.Scenes.MainRoom);
            PlayerPickUp.Instance.ResetKeys();
            SaveLoadOpenedDoors.ResetDoors();
            GameManager.Instance.UnPauseGame();
            GameManager.Instance.win = false;
            Hide();
        });
        quitButton.onClick.AddListener(() =>
        {
            Loader.LoadScene(Loader.Scenes.GameMainMenu);
            PlayerPickUp.Instance.ResetKeys();
            SaveLoadOpenedDoors.ResetDoors();
            GameManager.Instance.UnPauseGame();
            GameManager.Instance.win = false;
            Hide();
        });
        GameManager.Instance.onShowWinUi += Show;
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
}
