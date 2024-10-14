using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthLevel2 : MonoBehaviour , Ihealable
{
    [SerializeField] private float healAmount;
    public bool flag { get; set; }
    
    public void HealEntity(BaseHealthScript health)
    {
        health.SetCurrentHealth(healAmount);
    }
}
