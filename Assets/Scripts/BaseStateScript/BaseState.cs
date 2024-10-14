using System;
using UnityEngine;

public abstract class BaseState : MonoBehaviour
{
    protected float startTime;
    public float time => Time.time - startTime;

    protected IStateManager stateManagerRefrence;
    [SerializeField] protected Rigidbody2D rd;
    public event EventHandler animationState; 
    public event Action<float> animationStateWithOrder; 
    
    
    
    [SerializeField] protected LayerMask opponentMask;
    
    
    public abstract void onEnter();
    public abstract void onUpdate();
    public abstract void onExit();

    protected IStateManager GetStateManager()
    {
        return stateManagerRefrence;
    }

    public void SetStateManager(IStateManager s)
    {
        stateManagerRefrence = s;
    }
    
    public void InvokeAnimationState()
    {
        animationState?.Invoke(this,EventArgs.Empty);
    }

    public void InvokeAnimationState(float f)
    {
        animationStateWithOrder?.Invoke(f);
    }

}
