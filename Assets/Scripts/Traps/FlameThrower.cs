using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrower : MonoBehaviour
{
   [SerializeField] private Vector2 fireBoxSize;
   [SerializeField] private Transform fireBoxPoint;
   [SerializeField] private float damagingTime;
   [SerializeField] private float fireTime;
   public event Action throwFlameAnimationEvent;
   private float timer;
   private FireState fireState;
   
   private enum FireState
   {
      FireOn,
      FireOff
   }
   
   private void Start()
   {
      fireState = FireState.FireOff;
   }

   private void Update()
   {
      timer += Time.deltaTime;
      switch (fireState)
      {
         case FireState.FireOff:
            if (timer > fireTime)
            {
               fireState = FireState.FireOn;
               timer = 0;
               throwFlameAnimationEvent?.Invoke();
            }
            break;
         case FireState.FireOn:
            ThrowFlame();
            if (timer > damagingTime)
            {
               fireState = FireState.FireOff;
               timer = 0;
            }
            break;
      }
   }


   private void ThrowFlame()
   {
      Collider2D[] hit = Physics2D.OverlapBoxAll(fireBoxPoint.position, fireBoxSize, 0f);
      foreach (Collider2D obj in hit)
      {
         if (obj.TryGetComponent<BaseHealthScript>(out BaseHealthScript health))
         {
            health.TakeDamage(0.5f);
         }
      }
   }
   
   void OnDrawGizmos()
   {
      Gizmos.color = Color.red;
      Gizmos.matrix = Matrix4x4.TRS(fireBoxPoint.position, Quaternion.Euler(0, 0, 0), Vector3.one);
      Gizmos.DrawWireCube(Vector3.zero, fireBoxSize);
   }
}
