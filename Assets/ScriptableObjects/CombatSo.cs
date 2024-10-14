using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu()]
public class CombatSo : ScriptableObject
{
    public float damage;
    public float attack1Range;
    public float attack2Range;
    public float attack3Range;
    public float specialAttackRange;
    public float attackRate;
    public float castCoolDown;
    public float attackAnimationHitTime;
    public float attackAnimationRemainingTime;
}
