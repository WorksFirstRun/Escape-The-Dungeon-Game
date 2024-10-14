using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStateManager
{
    public BaseState GetState(string sn);
    public void SwitchState(BaseState s);
    public BaseState GetLastState();
    public void SetLastState(BaseState s);
    public void DisableStateManager();
}


public enum States
{
    Attack,
    Idle,
    Roam,
    Chase,
    Move,
    Dash,
    KnockBack,
    Cast,
}