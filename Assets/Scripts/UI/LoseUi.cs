using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoseUi : MonoBehaviour
{
   [SerializeField] private Button RetryButton;

   private void Start()
   {
      RetryButton.onClick.AddListener(() =>
      {
         Loader.LoadScene(Loader.Scenes.MainRoom);
         PlayerPickUp.Instance.ResetKeys();
         SaveLoadOpenedDoors.ResetDoors();
         GameManager.Instance.UnPauseGame();
         GameManager.Instance.lose = false;
         Hide();
      });
      GameManager.Instance.onShowLoseUi += Show;
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
