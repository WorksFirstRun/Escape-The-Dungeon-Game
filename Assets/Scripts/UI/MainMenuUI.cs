using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button ExitButton;

    private void Start()
    {
        playButton.onClick.AddListener(() =>
        {
            Loader.LoadScene(Loader.Scenes.MainRoom);
            PlayerPickUp.Instance.ResetKeys();
        });
        ExitButton.onClick.AddListener(() => Application.Quit());
    }
}
