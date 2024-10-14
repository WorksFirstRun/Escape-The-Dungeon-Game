using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NotEnoughMessageUi : MonoBehaviour
{
   [SerializeField] private TextMeshProUGUI text;

   public void SetKeyType(string obj)
   {
      text.text = "Not Enough " + obj + " Keys";
   }
   
}
