using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class MovementSO : ScriptableObject
{
    public float speed;
    public float dashingSpeed;
    public float dashingDuration;
    public float dashingCoolDown;
}
