using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader
{
    public static Action onLoaderCallBack;
    
    public enum Scenes
    {
        Level1,
        Level2,
        Level3,
        MainRoom,
        GameMainMenu,
        Loading
    }

    public static void LoadScene(Scenes scene)
    {
        onLoaderCallBack += () =>
        {
            SceneManager.LoadScene(scene.ToString());
        };

        SceneManager.LoadScene(Scenes.Loading.ToString());
    }

    public static void LoaderCallBack()
    {
        onLoaderCallBack?.Invoke();
        onLoaderCallBack = null;
    }
    
}
