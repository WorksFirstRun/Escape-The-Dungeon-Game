using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestVisuals : MonoBehaviour
{
    private const string OPEN = "Open";
    [SerializeField] private ChestScript chest;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject interactUI;
    private bool isOpened;
    public void Start()
    {
        chest.trigerOpenAnimation += ChestOnTrigerOpenAnimation;
        chest.interactionUiShowHideEvent += ChestOninteractionUiShowHideEvent;
    }

    private void ChestOninteractionUiShowHideEvent(bool obj)
    {
        if (!isOpened)
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
        
    }

    private void ChestOnTrigerOpenAnimation(object sender, EventArgs e)
    {
        animator.SetTrigger(OPEN);
        StartCoroutine(StopTheAnimation());
    }

    private IEnumerator StopTheAnimation()
    {
        yield return new WaitForSeconds(0.25f);
        isOpened = true;
        HideUi();
        animator.speed = 0f;
    }


    private void ShowUi()
    {
        interactUI.SetActive(true);
    }

    private void HideUi()
    {
        interactUI.SetActive(false);
    }
}
