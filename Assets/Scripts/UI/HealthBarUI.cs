using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private Image bar;
    [SerializeField] private BaseHealthScript entityHealth;
    private void Start()
    {
        entityHealth.onHealthUpdateBar += UpdateBarAfterTakingDamage; 
        entityHealth.onHealAnimation += UpdateBarAfterHealing;
    }

    private void UpdateBarAfterHealing(object sender, BaseHealthScript.CurrentHealthArgs e)
    {
        bar.fillAmount = e.currentHealth;
    }

    private void UpdateBarAfterTakingDamage(object sender, BaseHealthScript.CurrentHealthArgs e)
    {
        bar.fillAmount = e.currentHealth;
    }
}
