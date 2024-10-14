using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitScritp : MonoBehaviour , IInteraction
{
    public void InvokeSelectedAnimation()
    {
        
    }

    public void Interact(GameObject player)
    {
        Loader.LoadScene(Loader.Scenes.MainRoom);
    }
}
