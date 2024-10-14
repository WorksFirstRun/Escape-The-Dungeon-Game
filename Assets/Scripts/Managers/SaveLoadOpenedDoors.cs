using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadOpenedDoors : MonoBehaviour
{
   [SerializeField] private DoorScript[] doors;

   private void Awake()
   {
      LoadDoorStates();
   }

   public void SaveDoorState(int doorID, bool isOpen)
   {
      PlayerPrefs.SetInt("Door_" + doorID, isOpen ? 1 : 0);
      PlayerPrefs.Save();
   }

   public static void ResetDoors()
   {
      for (int i = 1; i <= 3; i++)
      {
         PlayerPrefs.SetInt("Door_" + i, 0);
      }
      PlayerPrefs.Save();
   }
   
   void LoadDoorStates()
   {
      for (int i = 1; i <= 3; i++) 
      {
         bool isOpen = PlayerPrefs.GetInt("Door_" + i, 0) == 1;
         SetDoorState(i, isOpen); 
      }
   }
   
   void SetDoorState(int doorID, bool isOpen)
   {
      if (isOpen)
      {
         Destroy(doors[doorID - 1].gameObject);
      }
   }


}
