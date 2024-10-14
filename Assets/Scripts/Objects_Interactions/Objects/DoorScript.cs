using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour , IInteraction
{
    private bool isClose;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private DoorType doorType;
    private InteractionStates interactionStates;
    private bool beforeChanges;
    private bool afterChanges;
    public event Action<bool> interactUiEvent;
    public event Action<string> notEnoughKeysUiEvent;
    public event Action onSaveState;
    
    private enum InteractionStates
    {
        NoChanges,
        ThereIsChanges
    }
    
    private enum DoorType
    {
        GoldenDoorType,
        SilverDoorType
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
                isClose = CheckIfPlayerIsClose();
                InvokeSelectedAnimation();
                interactionStates = InteractionStates.NoChanges;
                break;
        }
    }
    
    public void InvokeSelectedAnimation()
    {
        interactUiEvent?.Invoke(isClose);
    }

    public void Interact(GameObject player)
    {
        if (player.TryGetComponent<PlayerPickUp>(out PlayerPickUp keys))
        {
            if (doorType == DoorType.GoldenDoorType)
            {
                if (keys.GetGoldenKeysNumber() > 0)
                {
                    keys.TakeGoldenKey();
                    onSaveState?.Invoke();
                    Destroy(gameObject);
                }
                else
                {
                    notEnoughKeysUiEvent?.Invoke("Golden");
                }
            }
            else if (doorType == DoorType.SilverDoorType)
            {
                if (keys.GetSilverKeysNumber() > 0)
                {
                    keys.TakeSilverKey();
                    Destroy(gameObject);
                }
                else
                {
                    notEnoughKeysUiEvent?.Invoke("Silver");
                }
            }
        }
    }
    
    private bool CheckIfPlayerIsClose()
    {
        Collider2D player = Physics2D.OverlapCircle(transform.position,1f,layerMask);

        if (player != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position,1f);
    }
}
