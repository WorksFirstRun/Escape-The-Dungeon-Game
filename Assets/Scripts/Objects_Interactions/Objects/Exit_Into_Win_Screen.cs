using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit_Into_Win_Screen : MonoBehaviour , IInteraction
{
    public void InvokeSelectedAnimation()
    {
        
    }

    public void Interact(GameObject player)
    {
        GameManager.Instance.ShowWinWindow();
        GameManager.Instance.PauseGame();
    }
}
