using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BringerOfDeathSpell : MonoBehaviour
{
    [SerializeField] private BoxCollider2D spellBox;
    private float spellBoxEnableTime;
    private float timer;
    [SerializeField] private float spellDamage;
    private SpellStates state;
    private bool damaged;
    private void Start()
    {
        spellBox.enabled = false;
        spellBoxEnableTime = 0.32f;
    }

    private enum SpellStates
    {
        EnableTime,
        DisableTime,
        DestroyTime,
    }
    
    private void Update()
    {
        timer += Time.deltaTime;
        switch (state)
        {
            case SpellStates.EnableTime:
                if (timer > spellBoxEnableTime)
                {
                    spellBox.enabled = true;
                    state = SpellStates.DisableTime;
                }
                break;
            case SpellStates.DisableTime:
                if (timer > 0.44f)
                {
                    spellBox.enabled = false;
                    state = SpellStates.DestroyTime;
                }
                break;
            case SpellStates.DestroyTime:
                if (timer > 1f)
                {
                    Destroy(gameObject);
                }
                break;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent<BaseHealthScript>(out BaseHealthScript obj) && !damaged)
        {
            damaged = true;
            obj.TakeDamage(spellDamage);
            spellBox.enabled = false;
        }
    }

    
}
