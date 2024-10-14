using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectLevelNpc : MonoBehaviour , IInteraction
{
    [SerializeField] private SelectLevelUi SelectLevelUI;
    
    public void InvokeSelectedAnimation()
    {
        
    }

    public void Interact(GameObject player)
    {
        SelectLevelUI.Show();
    }
}
