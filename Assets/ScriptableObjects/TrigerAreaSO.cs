using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TrigerAreaSO : ScriptableObject
{
    public float attackArea;
    public float chaseArea;
    public float castSpellArea;
    
    public bool CheckArea(Vector2 point, float areaRange, LayerMask opponentMask)
    {
        Collider2D hit = Physics2D.OverlapCircle(point, areaRange, opponentMask);
        if (hit != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
