using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Spikes : MonoBehaviour
{
     [SerializeField] private Vector2 spikesBoxSize;
     [SerializeField] private Transform spikesPoint;
     [SerializeField] private float damagingTime;
     [SerializeField] private float peakTime;
     [SerializeField] private float peakDamage;
     public event Action peakAnimationEvent;
     private float timer;
     private PeakState peakState;
       
       private enum PeakState
       {
          PeakOn,
          PeakOff
       }
       
       private void Start()
       {
          peakState = PeakState.PeakOff;
       }
    
       private void Update()
       {
          timer += Time.deltaTime;
          switch (peakState)
          {
             case PeakState.PeakOff:
                if (timer > peakTime)
                {
                   peakState = PeakState.PeakOn;
                   timer = 0;
                   peakAnimationEvent?.Invoke();
                }
                break;
             case PeakState.PeakOn:
                if (timer > damagingTime)
                {
                   Peak();
                   peakState = PeakState.PeakOff;
                   timer = 0;
                }
                break;
          }
       }
    
    
       private void Peak()
       {
          Collider2D[] hit = Physics2D.OverlapBoxAll(spikesPoint.position, spikesBoxSize, 0f);
          foreach (Collider2D obj in hit)
          {
             if (obj.TryGetComponent<BaseHealthScript>(out BaseHealthScript health))
             {
                health.TakeDamage(peakDamage);
             }
          }
       }
       
       void OnDrawGizmos()
       {
          Gizmos.color = Color.red;
          Gizmos.matrix = Matrix4x4.TRS(spikesPoint.position, Quaternion.Euler(0, 0, 0), Vector3.one);
          Gizmos.DrawWireCube(Vector3.zero, spikesBoxSize);
       }
}
