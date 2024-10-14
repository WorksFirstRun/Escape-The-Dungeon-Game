using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private Transform interactionCenterPoint;
    [SerializeField] private float interactionRange;
    [SerializeField] private LayerMask _layerMask;
    private void Start()
    {
        InputManager.Instance.onInteractAction += Interaction;
    }

    private void Interaction(object sender, EventArgs e)
    {
        if (CheckInteractObjects(out IInteraction obj))
        {
            obj.Interact(gameObject);
        }
    }

    private bool CheckInteractObjects(out IInteraction obj)
    {
        Collider2D item = Physics2D.OverlapCircle(interactionCenterPoint.position,interactionRange,_layerMask);
        if (item != null)
        {
            if (item.TryGetComponent<IInteraction>(out IInteraction o))
            {
                obj = o;
                return true;
            }
        }
        

        obj = null;
        return false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(interactionCenterPoint.position,interactionRange);
    }
}
