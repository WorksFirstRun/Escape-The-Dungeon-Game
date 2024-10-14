using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectLevelUi : MonoBehaviour
{
    [SerializeField] private Button level1Button;
    [SerializeField] private Button level2Button;
    [SerializeField] private Button level3Button;
    [SerializeField] private Button exitMenuButton;

    private void Start()
    {
        GameManager.Instance.HideAnyOtherUi += Hide;
        level1Button.onClick.AddListener(() => { Loader.LoadScene(Loader.Scenes.Level1);});
        level2Button.onClick.AddListener(() => { Loader.LoadScene(Loader.Scenes.Level2);});
        level3Button.onClick.AddListener(() => { Loader.LoadScene(Loader.Scenes.Level3);});
        exitMenuButton.onClick.AddListener(() => { Hide();});
        Hide();
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
