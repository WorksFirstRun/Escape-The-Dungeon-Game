using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : BaseHealthScript
{
    
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent<Ihealable>(out Ihealable potion))
        {
            if (!potion.flag) // bug fix where the player take heal twice instead of once
            {
                potion.flag = true;
                potion.HealEntity(this);
                Destroy(other.gameObject);
            }
        }
    }
   
}
