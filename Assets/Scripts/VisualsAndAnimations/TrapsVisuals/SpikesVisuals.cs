using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikesVisuals : MonoBehaviour
{
   private const string PEAK = "Peak";
   [SerializeField] private Animator animator;
   [SerializeField] private Spikes spikes;

   private void Start()
   {
      spikes.peakAnimationEvent += SpikesOnpeakAnimationEvent;
   }

   private void SpikesOnpeakAnimationEvent()
   {
      animator.SetTrigger(PEAK);
   }
}
