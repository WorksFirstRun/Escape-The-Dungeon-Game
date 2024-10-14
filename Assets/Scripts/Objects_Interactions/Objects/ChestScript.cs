using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestScript : MonoBehaviour, IInteraction
{
    public event EventHandler trigerOpenAnimation;
    public event Action<bool> interactionUiShowHideEvent; 
    private bool interacted;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private LootSO loot;
    private InteractionStates interactionStates;
    private bool beforeChanges;
    private bool afterChanges;
    
    
    public void InvokeSelectedAnimation()
    {
        trigerOpenAnimation?.Invoke(this,EventArgs.Empty);        
    }

    private enum InteractionStates
    {
        NoChanges,
        ThereIsChanges
    }
    
    private void Update()
    {
        afterChanges = CheckIfPlayerIsClose();
        switch (interactionStates)
        {
            case InteractionStates.NoChanges:
                if (beforeChanges != afterChanges)
                {
                    interactionStates = InteractionStates.ThereIsChanges;
                    beforeChanges = afterChanges;
                }
                break;
            
            case InteractionStates.ThereIsChanges:
                if (CheckIfPlayerIsClose())
                {
                    interactionUiShowHideEvent?.Invoke(true);
                }
                else
                {
                    interactionUiShowHideEvent?.Invoke(false);
                }
                interactionStates = InteractionStates.NoChanges;
                break;
        }
    }

    public void Interact(GameObject player)
    {
        
        InvokeSelectedAnimation();
        StartCoroutine(SpawnLootAfterAnimation());
    }

    private bool CheckIfPlayerIsClose()
    {
        Collider2D player = Physics2D.OverlapCircle(transform.position,1.5f,layerMask);

        if (player != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private IEnumerator SpawnLootAfterAnimation()
    {
        yield return new WaitForSeconds(0.5f);
        DropSystem.Instance.InvokeDropEvent(this,loot, transform.position);
        Destroy(gameObject);
    }
    
}
