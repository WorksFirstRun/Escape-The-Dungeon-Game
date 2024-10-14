using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingSceneCallBack : MonoBehaviour
{
   private bool isFirstFrame = true;

   private void Update()
   {
      if (isFirstFrame)
      {
         Loader.LoaderCallBack();
      }
   }
}
