using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class CastSpellState : BaseState
{
    // add a serialized filed for a spell stats here {game object for the spell} 
    private Vector2 playerPosition;
    [SerializeField] private SpellsSO spell;
    [SerializeField] private float castDuration;
    [SerializeField] private float spellSpawnTime;
    private Vector2 offset = new Vector2(0, 1.9f);
    private bool casted;
    
    public override void onEnter()
    {
        startTime = Time.time;
        if (PlayerStateManager.Instance is not null)
        {
            playerPosition = PlayerStateManager.Instance.transform.position;
            InvokeAnimationState();
        }
    }

    public override void onUpdate()
    {
        if (time > castDuration)
        {
            GetStateManager().SwitchState(GetStateManager().GetState(States.Idle.ToString()));
        }
        else if (time > spellSpawnTime && !casted)
        {
            casted = true;
            Instantiate(spell.spell, playerPosition + offset, quaternion.identity);
        }
    }

    public override void onExit()
    {
        startTime = 0;
        playerPosition = Vector2.zero;
        casted = false;
    }
    
    
}
