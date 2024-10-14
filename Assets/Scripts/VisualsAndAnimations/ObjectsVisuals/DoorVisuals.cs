using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorVisuals : MonoBehaviour
{
    [SerializeField] private GameObject interactMessageUi;
    [SerializeField] private NotEnoughMessageUi notEnoughKeyUi;
    [SerializeField] private DoorScript door;
    private float notEnoughMessageDuration = 2f;
    
    private void Start()
    {
        door.interactUiEvent += InteractUi;
        door.notEnoughKeysUiEvent += NoKeys;
    }

    private void NoKeys(string obj)
    {
        StopAllCoroutines();
        notEnoughKeyUi.SetKeyType(obj);
        notEnoughKeyUi.gameObject.SetActive(true);
        StartCoroutine(DisableNotEnoughMessage());
    }

    private void InteractUi(bool obj)
    {
        if (obj)
        {
            ShowUi();
        }
        else
        {
            HideUi();   
        }
    }

    private void ShowUi()
    {
        interactMessageUi.SetActive(true);
    }

    private void HideUi()
    {
        interactMessageUi.SetActive(false);
    }

    private IEnumerator DisableNotEnoughMessage()
    {
        yield return new WaitForSeconds(notEnoughMessageDuration);
        notEnoughKeyUi.gameObject.SetActive(false);
    }
}
